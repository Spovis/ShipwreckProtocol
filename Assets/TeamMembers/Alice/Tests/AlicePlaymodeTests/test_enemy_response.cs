
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*test seeing how fast is the enemy responding when the player gets in range*/
public class TestEnemyResponse
{
    [UnityTest]
    public IEnumerator EnemyResponseTime()
    {
       
        var enemyGameObject = new GameObject("Enemy");
        var enemy = enemyGameObject.AddComponent<enemy>();
    
        enemy.player = new GameObject("Player").transform; 
        enemy.player.position = new Vector3(0, 0, 0); 
        enemy.detectRange = 5f; 

        enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));

        enemy.player.position = new Vector3(3, 0, 0); 

        float startTime = Time.time;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => enemy.currentBehavior is AttackBehavior);
        
        float responseTime = Time.time - startTime;

        Assert.Less(responseTime, 2f, "Enemy responds in less than 2 seconds.");

        yield return null; 

        Object.Destroy(enemyGameObject);
    }
}
