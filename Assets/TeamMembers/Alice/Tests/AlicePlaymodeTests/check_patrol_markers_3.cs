using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test checks my patrolling function is watching the boundaries correctly.*/
public class patrol_bounds{
    GameObject enemyObj;
    Vector2 minBoundary = new Vector2(-10f, 0f);
    Vector2 maxBoundary = new Vector2(10f, 0f);

    [UnitySetUp]
    public IEnumerator Setup()
    {
        enemyObj = new GameObject("Enemy");
        var enemyScript = enemyObj.AddComponent<enemy>();
        enemyObj.AddComponent<Animator>();
        enemyScript.minBoundary = minBoundary;
        enemyScript.maxBoundary = maxBoundary;
        enemyScript.SetBehavior(new PatrolBehavior(enemyScript)); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_PatrolMaxBoundary()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        enemyObj.transform.position = maxBoundary;
        yield return new WaitForSeconds(1f);

        //Check if the enemy turns back (x<min)
        Assert.Less(enemyObj.transform.position.x, maxBoundary.x, "Enemy should turn back after reaching max boundary");

        yield return null;
    }

    //Test if the enemy turns back after reaching the min boundary
    [UnityTest]
    public IEnumerator Test_PatrolMinBoundary()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        //Move the enemy to the min boundary
        enemyObj.transform.position = minBoundary;
        yield return new WaitForSeconds(1f);

        //Check if the enemy turns back (x > min)
        Assert.Greater(enemyObj.transform.position.x, minBoundary.x, "Enemy should turn back after reaching min boundary");

        yield return null;
    }

    //enemy position changed when it reaches the max boundary
    [UnityTest]
    public IEnumerator Test_EnemyPositionAfterTurningAtMaxBoundary()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        //Move the enemy to the max boundary
        enemyObj.transform.position = maxBoundary;
        yield return new WaitForSeconds(1f);

        //Check if enemy has turned and isn't any longer at the max boundary
        Assert.Less(enemyObj.transform.position.x, maxBoundary.x, "Enemy position should be adjusted after reaching max boundary");

        yield return null;
    }

    //Test if the enemy position is changed when it reaches the min boundary
    [UnityTest]
    public IEnumerator Test_EnemyPositionAfterTurningAtMinBoundary()
    {
        var enemyScript = enemyObj.GetComponent<enemy>();

        enemyObj.transform.position = minBoundary;
        yield return new WaitForSeconds(1f);

        //Check if the enemy turned and isn't at the min boundary
        Assert.Greater(enemyObj.transform.position.x, minBoundary.x, "Enemy position should be adjusted after reaching min boundary");

        yield return null;
    }
}
