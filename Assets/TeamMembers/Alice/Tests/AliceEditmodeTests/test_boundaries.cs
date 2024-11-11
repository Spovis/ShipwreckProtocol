using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*similar to my other tests but a boudnary test in edit mode*/
public class EnemyNotPastMax{
    [UnityTest]
    public IEnumerator EnemyNot_Max()
    {
        var enemy = new GameObject("Enemy");
        var enemyComponent = enemy.AddComponent<enemy>();
        enemy.AddComponent<Animator>();
        enemyComponent.maxBoundary = new Vector2(10f, 0f);
        enemyComponent.minBoundary = new Vector2(0f, 0f);

        //set patrolling
        enemyComponent.SetBehavior(new PatrolBehavior(enemyComponent));
        enemyComponent.transform.position = new Vector3(15f, 0f, 0f); //Attempt to move past max

        // Ensure enemy is still within bounds
        Assert.IsTrue(enemyComponent.transform.position.x <= 10f, "Enemy shouldn't go past max boundary.");
        yield return null;
    }
}
