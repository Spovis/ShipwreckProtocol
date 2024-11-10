using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*test if an obstacle in the way causes the enemy to stop attacking(can't see the player).*/
public class EnemyObject
{
    [UnityTest]
    public IEnumerator EnemyStopsIfObject()
    {
        var enemyObject = new GameObject("Enemy");
        var enemy = enemyObject.AddComponent<enemy>();
        var playerObject = new GameObject("Player");
        playerObject.transform.position = new Vector3(5f, 0f, 0f);
        enemy.player = playerObject.transform;
        enemy.detectRange = 10f;
        var obstacleObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacleObject.transform.position = new Vector3(2.5f, 0f, 0f);

        enemy.SetBehavior(new AttackBehavior(enemy));

        yield return new WaitForSeconds(1f);

        bool isAttacking = enemyObject.GetComponent<Animator>().GetBool("is_attacking");
        Assert.IsFalse(isAttacking, "Enemy should not attack with something in the way.");

        Object.Destroy(enemyObject);
        Object.Destroy(obstacleObject);
        Object.Destroy(playerObject);
    }
}
