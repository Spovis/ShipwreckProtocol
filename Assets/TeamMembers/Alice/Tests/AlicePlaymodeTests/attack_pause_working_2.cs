/*using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
my enemy has a brief pause in between shooting(very brief), so the player isn't bombarded constantly. 
This test checks that the enemy is in fact pausing correctly.
public class attack_pause_working
{
    GameObject enemyObj;
    float attackPause = .5f;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        enemyObj = new GameObject("Enemy");
        var enemyScript = enemyObj.AddComponent<enemy>();  
        enemyScript.attackPause = attackPause;
        enemyObj.AddComponent<Animator>();
        yield return null;
    }

    //Test that the attack pause starts after the first attack it does
    [UnityTest]
    public IEnumerator AttackPauseStarts()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();
        var attackBehavior = enemyScript.GetComponent<AttackBehavior>();
        attackBehavior.OnBehaviorUpdate();

        // Verify that the enemy cannot attack after the first attack (should be on cooldown)
        Assert.IsFalse(enemyScript.ShootProjectile, "Enemy stops attacking to wait");

        yield return null;
    }

    //attack pause ends after the time, enemy can now re-attack
    [UnityTest]
    public IEnumerator AttackPauseAllDone()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();
        var attackBehavior = enemyScript.SetBehavior(new AttackBehavior(enemyScript));
        attackBehavior.OnBehaviorUpdate(); 
        yield return null;

        // Wait for the attack cooldown to finish
        yield return new WaitForSeconds(attackPause);
        attackBehavior.OnBehaviorUpdate(); 
        //enemy can now attack
        Assert.IsTrue(enemyScript.ShootProjectile, "Enemy attacks again after cooldown");

        yield return null;
    }
}
*/

