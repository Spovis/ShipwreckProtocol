
using UnityEngine;


/*this is my attack behavior function. Here I code the enemy's attacks.*/
public class AttackBehavior : EnemyBaseBehavior 
{
    private float attackCooldown = 1f;
    private float lastAttackTime;

    /*my constructor, I pass an enemy object into here, and then use the base feature of C# to give the enemy object to 
    EnemyBaseBehavior class constructor*/
    public AttackBehavior(enemy enemy) : base(enemy)
    {
        lastAttackTime = -attackCooldown; 
        //keeps track of how long it's been. If cool down is .5, then lastAttackTime= -.5. See later stuff
    }
    
    /*as we enter the attack behavior*/
    public override void OnEnterBehavior()
    {
        Debug.Log("Now about to start attacking"); 
        enemy.GetComponent<Animator>().SetBool("is_attacking", true); 
        //this sets up the animator by setting my bool to true
    }

/*each frame, it updates and checks if the player is there. (see enemy script with Transform player*/
    public override void OnBehaviorUpdate()
    {
        
        if (enemy.player == null) return;

        //dist to player
        float distToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distToPlayer <= enemy.detectRange)
        {
            /*lastAttackTime and attackCoolDown is 0 or greater (.5 = -.5 or greater. 
            As it progresses, it'll be 0.7+0.5, 1.0+0.5) etc etc*/
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Debug.Log("shooting");
                ShootProjectile();
                lastAttackTime = Time.time; //resets lastAttackTime to a new time-whebever the shooting occurred.
            }
        }
        else
        {
            //switch back to patrol
            Debug.Log("Going to patrol");
            enemy.SetBehavior(new PatrolBehavior(enemy));
        }
    }
    

    /*if there's a player and there's a projectile available, */
   private void ShootProjectile()
{
    Debug.Log("Preparing projectile");
    if (enemy.player != null && enemy.projectileTemplate != null)
    {
        Debug.Log("Shooting projectile");

        // Calculate the direction from the enemy to the player
        Vector3 direction = (enemy.player.position - enemy.transform.position);
        if (direction.magnitude == 0)
        {
            Debug.LogError("Direction is zero! Cannot normalize.");
            return;
        }

        Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;

        // Instantiate the projectile at the enemy's position
        GameObject projectileInstance = GameObject.Instantiate(
            enemy.projectileTemplate,
            enemy.transform.position,
            Quaternion.identity // Default rotation; angle is handled by Rigidbody2D
        );

        if (projectileInstance == null)
        {
            Debug.LogError("Failed to instantiate projectile.");
            return;
        }

        // Ensure the projectile is active
        projectileInstance.SetActive(true);

        // Get the projectile script
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        if (projectileScript == null)
        {
            Debug.LogError("Projectile script is missing on the instantiated projectile.");
            return;
        }

        // Initialize the projectile
        projectileScript.Initialize(direction2D);
    }
    else
    {
        Debug.LogWarning($"Missing references - Player: {enemy.player}, Template: {enemy.projectileTemplate}");
    }
}
    public override void OnExitBehavior()
    {
        Debug.Log("Leaving the attack state");
        enemy.GetComponent<Animator>().SetBool("is_attacking", false);
    }
}