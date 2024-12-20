using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test watches the damage to my enemy and makes sure it's being handled right*/
public class Enemy_Damage_Reduction
{
    GameObject enemyObj;
    int damageAmount = 10;
    int initialHealth = 100;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        enemyObj = new GameObject("Enemy");
        var enemyScript = enemyObj.AddComponent<enemy>();
        enemyObj.AddComponent<Animator>();
        enemyScript.health = initialHealth;
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnemyDamage()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        int healthBeforeDamage = enemyScript.health;
        enemyScript.TakeDamage(damageAmount);
        yield return null;

        int healthAfterDamage = enemyScript.health;
        Assert.AreEqual(healthBeforeDamage - damageAmount, healthAfterDamage, "Enemy health should reduce correctly by the damage amount");
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoNegDamage()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        //go until health goes below zero
        while (enemyScript.health > 0)
        {
            enemyScript.TakeDamage(damageAmount);
            yield return null;
        }

        Assert.AreEqual(0, enemyScript.health, "Enemy health should not go below zero");
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator ZeroAtMax()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        //go until the enemy's health reaches zero
        while (enemyScript.health > 0)
        {
            enemyScript.TakeDamage(damageAmount);
            yield return null;
        }

        Assert.AreEqual(0, enemyScript.health, "Enemy health should reach zero after enough damage is taken");
        
        yield return null;
    }
}
