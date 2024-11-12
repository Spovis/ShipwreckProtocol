using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test should spawn tons of enemies to test and then make sure the game can handle it if needed*/
public class SpawnLots{
    [UnityTest]
    public IEnumerator SpawnEnemies()
    {
        int enemyCount = 10;//could go higher
        Vector2 minBoundary = new Vector2(-10f, 0f);
        Vector2 maxBoundary = new Vector2(10f, 0f);
        List<enemy> enemies = new List<enemy>();
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<Rigidbody2D>();
        playerObj.AddComponent<Animator>();
        playerObj.transform.position = new Vector3(10f, 0f, 0f);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyObj = new GameObject("Enemy" + i);
            var enemyScript = enemyObj.AddComponent<enemy>();
            enemyObj.AddComponent<Animator>();

            enemyScript.minBoundary = minBoundary;
            enemyScript.maxBoundary = maxBoundary;
            enemyScript.SetBehavior(new IdleBehavior(enemyScript, enemyScript.minBoundary, enemyScript.maxBoundary));
            enemyScript.player = playerObj.transform;
            playerObj.transform.position = new Vector3(10f, 0f, 0f);
            enemies.Add(enemyScript);
        }
        for (int i = 0; i < 50; i++)//could also make a while loop
        {
            foreach (var enemy in enemies)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }

        foreach (var enemy in enemies){
            Object.Destroy(enemy.gameObject);
        }
    }
}
