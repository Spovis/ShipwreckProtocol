
using UnityEngine;


/*this is my attack behavior function. Here I code the enemy's attacks.*/
public class AttackBehavior : EnemyBaseBehavior 
{
    private float attackCooldown = 0.5f;
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
                ShootProjectile();
                lastAttackTime = Time.time; //resets lastAttackTime to a new time-whebever the shooting occurred.
            }
        }
        else
        {
            //switch back to patrol
            enemy.SetBehavior(new PatrolBehavior(enemy));
        }
    }
    

    /*if there's a player and there's a projectile available, */
    private void ShootProjectile()
    {
        if (enemy.player != null && enemy.projectileTemplate != null) 
        {
            /*used normalized here to focus on the direction of the vector from the enemy to player*/
            Vector3 direction = (enemy.player.position - enemy.transform.position).normalized;
            //Atan gets the angle of vector from x axis and convert it to degrees from rads <
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            /*create a new instance of my projectile using a unity method. sets my pos at enemy, Quaternion.Euler
            gives the angle I want my projectile rotated at.(so it kind of looks like it's aiming right at the player)
            */
            GameObject projectile = GameObject.Instantiate(
                enemy.projectileTemplate,
                enemy.transform.position,
                Quaternion.Euler(0, 0, angle)
            );
            
            projectile.SetActive(true);
            Debug.Log("Shooting projectile at player");
        }
        else
        {
            //warning me in case I lose references. I had it happen a couple times where my player got
            //disconnected off of my enemy for some reason
            Debug.LogWarning($"Missing references - Player: {enemy.player}, Template: {enemy.projectileTemplate}");
        }
    }

    public override void OnExitBehavior()
    {
        Debug.Log("Leaving the attack state");
        enemy.GetComponent<Animator>().SetBool("is_attacking", false);
    }
}