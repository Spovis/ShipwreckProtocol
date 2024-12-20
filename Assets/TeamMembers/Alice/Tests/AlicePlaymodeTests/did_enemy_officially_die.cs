using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test makes sure my enemy is taken offscreen and fully removed*/
public class Enemy_Fully_Dead
{
    GameObject enemyObj;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        enemyObj = new GameObject("Enemy");
        GameObject playerObj = new GameObject("Player");
        var enemyScript = enemyObj.AddComponent<enemy>();
        enemyObj.AddComponent<Animator>();
        playerObj.AddComponent<Animator>();
        enemyScript.health = 1;  // Initial health is set to 1, will be killed by 1 damage
        enemyScript.player = playerObj.transform;
        yield return null;
    }

    // Test if the collider is disabled when the enemy dies
    [UnityTest]
    public IEnumerator ColliderDisabled()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();
        var collider = enemyObj.AddComponent<BoxCollider2D>();

        enemyScript.TakeDamage(1); // Kill the enemy
        yield return null;

        Assert.IsFalse(collider.enabled, "Collider should be disabled now enemy is dead");
        yield return null;
    }

    // Test if the enemy's GameObject is deactivated or destroyed after death
    [UnityTest]
    public IEnumerator GameObjDeactivate()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();
        var collider = enemyObj.AddComponent<BoxCollider2D>();

        enemyScript.TakeDamage(1); // Kill the enemy
        yield return null;

        Assert.IsFalse(enemyObj.activeSelf, "Enemy gameObject should be deactivated/destroyed now");
        yield return null;
    }

    // Test if the enemy takes damage and dies correctly
    [UnityTest]
    public IEnumerator TakeDamageAndDie()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();
        
        Assert.AreEqual(1, enemyScript.health, "Initial health should be 1");
        
        enemyScript.TakeDamage(1); // Kill the enemy
        yield return null;

        Assert.AreEqual(0, enemyScript.health, "Enemy health should be 0 after damage");
        yield return null;
    }
}
