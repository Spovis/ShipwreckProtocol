using UnityEngine;

public class PatrolBehavior : EnemyBaseBehavior 
{
    private Vector3 currentTarget;
    private bool isObstacleHit = false;
    private bool is_hunter;
    private float speed = 5.0f;

    public PatrolBehavior(enemy enemy) : base(enemy)
    {
    }

    public override void OnEnterBehavior()
    {
        Debug.Log("Entering patrol state");
        enemy.GetComponent<Animator>().SetBool("is_patrolling", true);
        enemy.GetComponent<Animator>().SetBool("is_hunter", true);
        currentTarget = enemy.minBoundary;
    }

    public override void OnBehaviorUpdate()
    {
        float distToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        
        if (distToPlayer <= enemy.detectRange)
        {
            Debug.Log("Player detected within patrol range, transitioning to attack");
            enemy.GetComponent<Animator>().SetBool("is_patrolling", false);
            enemy.SetBehavior(new AttackBehavior(enemy));
        }
        else
        {
            Patrol();
        }

        if (IsPlayerInBounds())
        {
            Debug.Log("Player in bounds.");
        }
        else
        {
            Debug.Log("Player not in bounds.");
        }
    }

    private void Patrol() 
    {
        Debug.Log("patrolling");
        if (isObstacleHit)
        {
            //delay
            isObstacleHit = false;  //reset
            return;  //exit
        }

        if (Vector3.Distance(enemy.transform.position, currentTarget) < 0.1f)
        {
            if (Vector3.Distance(currentTarget, enemy.minBoundary) < 0.1f)
            {
                currentTarget = enemy.maxBoundary;
            }
            else if (Vector3.Distance(currentTarget, enemy.maxBoundary) < 0.1f)
            {
                currentTarget = enemy.minBoundary;
            }
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
        
        //Change direction when obstacle
        if (Vector3.Distance(currentTarget, enemy.minBoundary) < 0.1f)
        {
            currentTarget = enemy.maxBoundary;
        }
        else if (Vector3.Distance(currentTarget, enemy.maxBoundary) < 0.1f)
        {
            currentTarget = enemy.minBoundary;
        }
        
        FlipDirection();
    }

    private void FlipDirection() 
    {
        Vector3 scale = enemy.transform.localScale;
        if (currentTarget.x > enemy.transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
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