using UnityEngine;
public class PatrolBehavior : EnemyBaseBehavior 
{
    private Vector3 currentTarget;
    private bool isObstacleHit = false;
    private float speed = 5.0f;

    public PatrolBehavior(enemy enemy) : base(enemy) { }

    public override void OnEnterBehavior()
    {
        Debug.Log("Entering patrol state");
        enemy.GetComponent<Animator>().SetBool("is_patrolling", true);
        enemy.GetComponent<Animator>().SetBool("is_idle",false);
        //enemy.GetComponent<Animator>().SetBool("is_hunter", true);
        currentTarget = enemy.pointA.position;  // Use pointA as the initial target
    }
    public override void OnBehaviorUpdate()
    {
        float dist_to_player = Vector3.Distance(enemy.transform.position, enemy.player.position); 
        if (dist_to_player <= enemy.detectRange){
            enemy.GetComponent<Animator>().SetBool("is_patrolling", false);
            enemy.SetBehavior(new AttackBehavior(enemy)); //Transition to AttackBehavior
        }
        Patrol();

    }


    private void Patrol() 
    {
        Debug.Log("patrolling");
        if (isObstacleHit)
        {
            isObstacleHit = false;  // Reset flag
            return;
        }

        if (Vector3.Distance(enemy.transform.position, currentTarget) < 0.1f)
        {
            // Switch target between pointA and pointB
            currentTarget = currentTarget == enemy.pointA.position ? enemy.pointB.position : enemy.pointA.position;
            FlipDirection();
        }

        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position, 
            currentTarget, 
            speed * Time.deltaTime
        );
    }

    public void HandleObstacleCollision()
    {
        isObstacleHit = true;
        Debug.Log("Obstacle collision handled in patrol behavior");

        // Flip to the opposite patrol point
        currentTarget = currentTarget == enemy.pointA.position ? enemy.pointB.position : enemy.pointA.position;
        FlipDirection();
    }

    private void FlipDirection() 
    {
        Vector3 scale = enemy.transform.localScale;
        scale.x = currentTarget.x > enemy.transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        enemy.transform.localScale = scale;
    }



    private bool IsPlayerInBounds()
    {
        Vector3 playerPos = enemy.player.position;
        Debug.Log($"Player Position: {playerPos}");
        Debug.Log($"Min Boundary: {enemy.minBoundary}, Max Boundary: {enemy.maxBoundary}");
        
        return playerPos.x >= enemy.minBoundary.x && 
               playerPos.x <= enemy.maxBoundary.x &&
               playerPos.y >= enemy.minBoundary.y && 
               playerPos.y <= enemy.maxBoundary.y;
    }

    public override void OnExitBehavior()
    {
        Debug.Log("Exiting patrol state");
        enemy.GetComponent<Animator>().SetBool("is_patrolling", false);
    }
}