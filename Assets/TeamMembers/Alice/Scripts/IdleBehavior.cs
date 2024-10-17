using UnityEngine;

public class IdleBehavior : EnemyBaseBehavior
{
    public Vector2 boundaryMin; 
    public Vector2 boundaryMax;

    public IdleBehavior(enemy enemy, Vector2 boundaryMin, Vector2 boundaryMax) : base(enemy)
    {
        this.boundaryMin = boundaryMin;
        this.boundaryMax = boundaryMax;
    }

    public override void OnEnterBehavior()
    {
        Debug.Log("Entering the idle state");
        enemy.GetComponent<Animator>().SetBool("is_idle", true);
    }

    public override void OnBehaviorUpdate()
    {
        float dist_to_player = Vector3.Distance(enemy.transform.position, enemy.player.position);
        
        // Check if the player in bounds and within detection range
        if (IsPlayerInBounds() && dist_to_player <= enemy.detectRange)
        {
            Debug.Log("Player detected within idle range and bounds!");
            enemy.SetBehavior(new AttackBehavior(enemy)); // Transition to AttackBehavior
        }
        else
        {
            Debug.Log("Player out of bounds or out of detection range.");
        }
    }

    public override void OnExitBehavior()
    {
        Debug.Log("Now leaving the idle state");
        enemy.GetComponent<Animator>().SetBool("is_idle", false);
    }

    private bool IsPlayerInBounds()
    {
        Vector3 playerPos = enemy.player.position; // Get the player's current position
        // Check if the player's position is within the defined boundaries
        return playerPos.x >= boundaryMin.x && playerPos.x <= boundaryMax.x &&
               playerPos.y >= boundaryMin.y && playerPos.y <= boundaryMax.y;
    }
}
