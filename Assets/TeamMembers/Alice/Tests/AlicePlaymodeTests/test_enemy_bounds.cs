using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test looks at where exactly my enemy goes out of bounds if it doesy*/
public class EnemyBounds
{
    GameObject enemyGameObject;
    enemy enemy;
    Vector2 minBoundary = new Vector2(-0.03f, 9.940499f);
    Vector2 maxBoundary = new Vector2(116.05f, 9.940499f);

    [UnitySetUp]
    public IEnumerator Setup()
    {
        enemyGameObject = new GameObject("Enemy");
        enemy = enemyGameObject.AddComponent<enemy>();
        enemy.minBoundary = minBoundary;
        enemy.maxBoundary = maxBoundary;
        enemy.transform.position = new Vector3(50f, 0f, 0f);
        enemy.SetBehavior(new IdleBehavior(enemy, enemy.minBoundary, enemy.maxBoundary));

        yield return null;
    }

    [UnityTest]
    public IEnumerator XOutOfBounds_Lower()
    {
        enemy.transform.position = new Vector3(minBoundary.x - 1f, 0f, 0f); //push past min x
        yield return new WaitForSeconds(0.1f);

        Vector3 position = enemy.transform.position;
        Assert.IsFalse(position.x >= enemy.minBoundary.x && position.x <= enemy.maxBoundary.x,
            $"Enemy X {position.x} is out of bounds (lower than min boundary).");
    }

    [UnityTest]
    public IEnumerator XOutOfBounds_Upper()
    {
        enemy.transform.position = new Vector3(maxBoundary.x + 1f, 0f, 0f); //push past max x
        yield return new WaitForSeconds(0.1f);

        Vector3 position = enemy.transform.position;
        Assert.IsFalse(position.x >= enemy.minBoundary.x && position.x <= enemy.maxBoundary.x,
            $"Enemy X {position.x} is out of bounds (greater than max boundary).");
    }
    [UnityTest]
public IEnumerator Test_EnemyYOutOfBounds_Lower()
{
    enemy.transform.position = new Vector3(50f, enemy.minBoundary.y - 1); //push below lower y bound
    yield return new WaitForSeconds(0.1f);

    Vector3 position = enemy.transform.position;
    Assert.IsTrue(position.y >= enemy.minBoundary.y, $"Enemy Y {position.y} out of lower bound [{enemy.minBoundary.y}]");
}

[UnityTest]
public IEnumerator Test_EnemyYOutOfBounds_Upper()
{
    enemy.transform.position = new Vector3(50f, enemy.maxBoundary.y + 1); //push above upper y bound
    yield return new WaitForSeconds(0.1f);

    Vector3 position = enemy.transform.position;
    Assert.IsTrue(position.y <= enemy.maxBoundary.y, $"Enemy Y {position.y} out of upper bound [{enemy.maxBoundary.y}]");
}

}
