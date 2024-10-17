using UnityEngine;

public class IdleBehavior : EnemyBaseBehavior
{
    public IdleBehavior(enemy enemy) : base(enemy) { }

    public override void OnEnterBehavior()
    {
        Debug.Log("entering the idle state");
        enemy.GetComponent<Animator>().SetBool("is_idle", true);
    }

    public override void OnBehaviorUpdate()
    {
        float dist_to_player = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (dist_to_player <= enemy.detectRange)
        {
            enemy.SetBehavior(new AttackBehavior(enemy));
        }
    }

    public override void OnExitBehavior()
    {
        Debug.Log("now leaving the idle state");
        enemy.GetComponent<Animator>().SetBool("is_idle", false);
    }
}