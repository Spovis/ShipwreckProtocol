using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class SpawnLots{
    [UnityTest]
public IEnumerator SpawnEnemies()
{
    int enemyCount = 10;//could go higher
    List<enemy> enemies = new List<enemy>();
    for (int i = 0; i < enemyCount; i++)
    {
        var enemyGameObject = new GameObject($"Enemy_{i}");
        var enemy = enemyGameObject.AddComponent<enemy>();
        enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));
        enemies.Add(enemy);
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
