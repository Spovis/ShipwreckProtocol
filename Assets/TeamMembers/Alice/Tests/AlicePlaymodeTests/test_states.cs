using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test sees if enemy resets when player dies*/
public class ResetDeath{
[UnityTest]
public IEnumerator EnemyResetsPlayerDeath()
{
    var enemyGameObject = new GameObject("Enemy");
    var enemy = enemyGameObject.AddComponent<enemy>();
    enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));
    enemyGameObject.AddComponent<Animator>();
    enemy.player.gameObject.SetActive(false); //kill player
    yield return new WaitForSeconds(0.1f);
    yield return null;

    Assert.IsInstanceOf(typeof(IdleBehavior), enemy.currentBehavior, "Enemy should idle when player dies");
   
    Object.Destroy(enemyGameObject);
}
}