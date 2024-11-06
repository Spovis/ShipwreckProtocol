using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class Check_States{
[UnityTest]
public IEnumerator Animation_StateCheck()
{
    var enemyGameObject = new GameObject("Enemy");
    var enemy = enemyGameObject.AddComponent<enemy>();
    enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));
    
    
    Animator animator = enemyGameObject.AddComponent<Animator>();
    
    yield return new WaitForSeconds(0.1f);
    yield return null;

    enemy.SetBehavior(new AttackBehavior(enemy));
    animator.SetBool("is_attacking", true);

    
    yield return null;

    Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("attack"), "Attack animation should be playing.");
    
    // Clean up
    Object.Destroy(enemyGameObject);
}
}
