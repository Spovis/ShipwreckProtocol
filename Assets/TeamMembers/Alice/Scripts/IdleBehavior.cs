using UnityEngine;
public class IdleBehavior : EnemyBaseBehavior
{
    [SerializeField] private bool is_hunter=true;
    private Vector2 boundaryMin, boundaryMax;
    private GameObject pointA, pointB;
    
    //just calls the base constructor
    public IdleBehavior(enemy enemy, Vector2 boundaryMin, Vector2 boundaryMax) : base(enemy)
    {}
    public override void OnEnterBehavior(){
        Debug.Log("Entering the idle state");
        enemy.GetComponent<Animator>().SetBool("is_idle", true);
        if (is_hunter){
            enemy.GetComponent<Animator>().SetBool("is_idle", false);
            enemy.SetBehavior(new PatrolBehavior(enemy)); //if it's a patrolling enemy, set behavior automatically to patrol
            enemy.GetComponent<Animator>().SetBool("is_hunter", true); //Transition to PatrolBehavior
        }
    }

    public override void OnBehaviorUpdate(){
        float dist_to_player = Vector3.Distance(enemy.transform.position, enemy.player.position); 
        
        //Check if the player in bounds and within detection range
        if (dist_to_player <= enemy.detectRange){
            Debug.Log("Player detected within idle range, moving to attack ");
            enemy.GetComponent<Animator>().SetBool("is_idle", false);
            enemy.SetBehavior(new AttackBehavior(enemy)); //Transition to AttackBehavior
        }
        else{
            Debug.Log($"Player  out of detection range. dist ={dist_to_player} range is {enemy.detectRange} ");
        }
        if(IsPlayerInBounds()){
            Debug.Log("Plaayer is in bounds");
        }
        else{
            Debug.Log("Not in bounds");
        }
       
    }

    public override void OnExitBehavior(){
        Debug.Log("Now leaving the idle state");
        enemy.GetComponent<Animator>().SetBool("is_idle", false);
    }

    private bool IsPlayerInBounds(){
        Vector3 playerPos = enemy.player.position; 
        Debug.Log($"Player Position: {playerPos}");
        Debug.Log($"Min Boundary: {enemy.minBoundary}, Max Boundary: {enemy.maxBoundary}");

        return playerPos.x >= enemy.minBoundary.x && playerPos.x <= enemy.maxBoundary.x &&
        playerPos.y >= enemy.minBoundary.y && playerPos.y <= enemy.maxBoundary.y;
    }
}
