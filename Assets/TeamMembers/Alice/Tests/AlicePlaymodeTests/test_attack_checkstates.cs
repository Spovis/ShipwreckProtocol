using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test checks if all my animations are playing as they should*/
public class Check_States
{
    private GameObject enemyGameObject;
    private enemy enemy;
    private Animator animator;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        enemyGameObject = new GameObject("Enemy");
        enemy = enemyGameObject.AddComponent<enemy>();
        animator = enemyGameObject.AddComponent<Animator>();
        enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_IdleState()
    {
        yield return new WaitForSeconds(0.1f); // Wait for animation to play
        Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("idle"), "Idle animation should be playing.");
    }

    [UnityTest]
    public IEnumerator Test_AttackState()
    {
        enemy.SetBehavior(new AttackBehavior(enemy));
        animator.SetBool("is_attacking", true);

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("attack"), "Attack animation should be playing.");
    }

    [UnityTest]
    public IEnumerator Test_PatrolState()
    {
        
        enemy.SetBehavior(new PatrolBehavior(enemy));
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("patrol"), "Patrol animation should be playing.(same as idle)");
    }

    [TearDown]
    public void TearDown()
    {
        //Clean up test environment
        Object.Destroy(enemyGameObject);
    }
}
