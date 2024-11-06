using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyObject{
[UnityTest]
public IEnumerator EnemyStopsIfObject()
{
    var enemyObject = new GameObject("Enemy");
    var enemy = enemyObject.AddComponent<enemy>();
    var playerObject = new GameObject("Player");
    playerObject.transform.position = new Vector3(5f, 0f, 0f); 
    enemy.player = playerObject.transform;
    enemy.detectRange = 10f;

    //put an object
    var obstacleObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    obstacleObject.transform.position = new Vector3(2.5f, 0f, 0f); 

    enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));
    
    yield return new WaitForSeconds(1f);

    
    Assert.IsFalse(enemyObject.GetComponent<Animator>().GetBool("is_attacking"), "Enemy doesn't attack with an obstacle present.");

    yield return null;
}
}