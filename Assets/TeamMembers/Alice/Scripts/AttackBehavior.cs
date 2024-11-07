
using UnityEngine;

public class AttackBehavior : EnemyBaseBehavior 
{
    private float attackCooldown = 0.5f;
    private float lastAttackTime;

    public AttackBehavior(enemy enemy) : base(enemy)
    {
        lastAttackTime = -attackCooldown;
    }
    
    public override void OnEnterBehavior()
    {
        Debug.Log("Now about to start attacking");
        enemy.GetComponent<Animator>().SetBool("is_attacking", true);
    }

    public override void OnBehaviorUpdate()
    {
        // First check if player reference exists
        if (enemy.player == null) return;

        // Check distance to player
        float distToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distToPlayer <= enemy.detectRange)
        {
            // Player is in range, try to shoot if cooldown is ready
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                ShootProjectile();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Player has left range, switch back to patrol
            enemy.SetBehavior(new PatrolBehavior(enemy));
        }
    }
    
    private void ShootProjectile()
    {
        if (enemy.player != null && enemy.projectileTemplate != null)
        {
            Vector3 direction = (enemy.player.position - enemy.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
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
            Debug.LogWarning($"Missing references - Player: {enemy.player}, Template: {enemy.projectileTemplate}");
        }
    }

    public override void OnExitBehavior()
    {
        Debug.Log("Leaving the attack state");
        enemy.GetComponent<Animator>().SetBool("is_attacking", false);
    }
}