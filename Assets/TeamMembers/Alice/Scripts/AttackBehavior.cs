using UnityEngine;

public class AttackBehavior : EnemyBaseBehavior
{
    public AttackBehavior(enemy enemy) : base(enemy) { }

    public override void OnEnterBehavior()
    {
        Debug.Log("Now about to start attacking");
        //enemy.GetComponent<Animator>().SetBool("is_attacking", true);
    }

    public override void OnBehaviorUpdate()
    {
        Debug.Log("Attacking the player");
        // Implement attack logic here
    }

    public override void OnExitBehavior()
    {
        Debug.Log("leaving the attack state");
        //enemy.GetComponent<Animator>().SetBool("is_attacking", false);
    }
}
