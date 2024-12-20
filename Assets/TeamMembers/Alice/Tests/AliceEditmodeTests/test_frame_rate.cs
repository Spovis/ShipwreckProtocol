using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/*this test is just to check frame rate for my enemy and make sure it's appearing in the game correctly*/
public class Check_Frame_Rates{
[UnityTest]
    public IEnumerator FrameEnemyBehavior()
    {
        var enemyGameObject = new GameObject("Enemy");
        var enemy = enemyGameObject.AddComponent<enemy>();
        enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));

        float startTime = Time.time;
        int frameCount = 0;

        float duration = 5f; 
        while (Time.time - startTime < duration){
            yield return new WaitForSeconds(0.1f);
            frameCount++;
            yield return null;
        }

        float averageFrameRate = frameCount/duration;
        Assert.IsTrue(averageFrameRate > 30f, "Frame rate too low");
        Object.Destroy(enemyGameObject);
    }
}
