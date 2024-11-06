using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class EnemyBounds{

[UnityTest]
public IEnumerator EnemyStays_InBounds()
{
    
    var enemyGameObject = new GameObject("Enemy");
    var enemy = enemyGameObject.AddComponent<enemy>();
    enemy.minBoundary = new Vector2(-0.03f, 9.940499f); 
    enemy.maxBoundary = new Vector2(116.05f, 9.940499f);

    enemy.transform.position = new Vector3(50f, 0f, 0f);

    enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));

    for (int i = 0; i < 50; i++)
    {
        yield return new WaitForSeconds(0.1f);
        yield return null;

        //is enemy in bounds
        Vector3 position = enemy.transform.position;
        Assert.IsTrue(position.x >= enemy.minBoundary.x && position.x <= enemy.maxBoundary.x,
            $"Enemy X {position.x} out of bounds [{enemy.minBoundary.x}, {enemy.maxBoundary.x}]");
        Assert.IsTrue(position.y >= enemy.minBoundary.y && position.y <= enemy.maxBoundary.y,
            $"Enemy Y{position.y} out of bounds [{enemy.minBoundary.y}, {enemy.maxBoundary.y}]");
    }
}
}
