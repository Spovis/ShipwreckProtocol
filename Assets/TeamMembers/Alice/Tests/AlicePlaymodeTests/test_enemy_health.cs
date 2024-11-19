using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test is just to check damage. Unlike the other damage test, which is more general, 
this is a specicfic damage scenario with damage hardcoded in*/
public class test_enemy_health {


public IEnumerator EnemyHealthTest()
{
    var enemy = new GameObject("Enemy");
    var enemyComponent = enemy.AddComponent<enemy>();
    
    enemyComponent.health = 100;
    int damage = 25;
    enemy.AddComponent<Animator>();
    enemyComponent.TakeDamage(damage); 

    // Wait one frame
    yield return null;

    Assert.AreEqual(80, enemyComponent.health, "enemy health has decreased.");

    yield return null;
}

}
