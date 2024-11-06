
using UnityEngine;

public class PatrolBehavior : EnemyBaseBehavior
{
    public PatrolBehavior(enemy enemy, Vector2 boundaryMin, Vector2 boundaryMax) : base(enemy)
    {}

    private Vector3 currentTarget;
    bool isObstacleHit = false;
    private bool is_hunter;
    private float speed = 5.0f;
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

    public override void OnExitBehavior()
    {
        Debug.Log("Exiting patrol state");
        enemy.GetComponent<Animator>().SetBool("is_patrolling", false);
    }

   private void Patrol()
{
    Debug.Log("patrolling");
    if (isObstacleHit)
    {
        // Stop moving or apply a slight delay to allow flip
        isObstacleHit = false;  // Reset flag after handling collision
        return;  // Exit patrol for this frame to avoid overriding changes
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
    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentTarget, speed * Time.deltaTime);
}

private void FlipDirection()
{
    
    Vector3 scale = enemy.transform.localScale;

    if (currentTarget.x > enemy.transform.position.x)
    {
        scale.x = Mathf.Abs(scale.x);} 
    else
    {
        scale.x = -Mathf.Abs(scale.x); 

    enemy.transform.localScale = scale;
}
}

 void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Obstacle"))
    {
        isObstacleHit = true;
        Debug.Log($"Collided with: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Obstacle")){
            
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
    }
}





    private bool IsPlayerInBounds()
    {
        Vector3 playerPos = enemy.player.position;
        Debug.Log($"Player Position: {playerPos}");
        Debug.Log($"Min Boundary: {enemy.minBoundary}, Max Boundary: {enemy.maxBoundary}");

        return playerPos.x >= enemy.minBoundary.x && playerPos.x <= enemy.maxBoundary.x &&
        playerPos.y >= enemy.minBoundary.y && playerPos.y <= enemy.maxBoundary.y;
    }
}

