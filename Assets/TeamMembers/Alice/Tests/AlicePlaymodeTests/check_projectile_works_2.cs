/*using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test checks out the projectile for my enemy and makes sure it's functioning by spawning and moving 
public class ProjectileDirec{
    GameObject enemyObj;
    GameObject playerObj;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        enemyObj = new GameObject("Enemy");
        playerObj = new GameObject("Player");
        enemyObj.AddComponent<Animator>();
        playerObj.AddComponent<Animator>();

        enemyObj.transform.position = Vector3.zero;
        playerObj.transform.position = new Vector3(5f, 0f, 0f);

        yield return null;
    }

    //does it spawn correctly?
    [UnityTest]
    public IEnumerator Test_ProjectileSpawn()
    {
        var enemyScript = enemyObj.AddComponent<enemy>();
        var projectilePrefab = new GameObject("Projectile");
        projectilePrefab.AddComponent<Projectile>(); 
        var projectileRigidbody = projectilePrefab.AddComponent<Rigidbody2D>();
        enemyScript.projectileTemplate = projectilePrefab;
        enemyScript.player = playerObj.transform;

        enemyScript.ShootProjectile();
        yield return new WaitForSeconds(0.1f);

        var projectile = GameObject.Find("Projectile(Clone)");
        Assert.IsNotNull(projectile, "Projectile spawned");

        yield return null;
    }

    //is the projectile is moving in the right direction(player)
    [UnityTest]
    public IEnumerator Test_ProjectileDirection()
    {
        var enemyScript = enemyObj.AddComponent<enemy>();
        var projectilePrefab = new GameObject("Projectile");
        projectilePrefab.AddComponent<Projectile>(); 
        var projectileRigidbody = projectilePrefab.AddComponent<Rigidbody2D>();
        enemyScript.projectileTemplate = projectilePrefab;
        enemyScript.player = playerObj.transform;

        enemyScript.ShootProjectile();
        yield return new WaitForSeconds(0.1f);

        var projectile = GameObject.Find("Projectile(Clone)");
        Assert.IsNotNull(projectile, "Projectile should be spawned");

        //Check if the projectile headed in the correct direction
        Vector2 expectedDirection = (playerObj.transform.position - enemyObj.transform.position).normalized;
        Vector2 actualDirection = projectile.GetComponent<Rigidbody2D>().velocity.normalized;

        Assert.AreEqual(expectedDirection.x, actualDirection.x, 0.1f, "Projectile moving to player on X");
        Assert.AreEqual(expectedDirection.y, actualDirection.y, 0.1f, "Projectile moving to player on Y");

        yield return null;
    }

    //Test if the projectile has a Rigidbody2D 
    [UnityTest]
    public IEnumerator Test_ProjectileRigidbodyAssigned()
    {
        var enemyScript = enemyObj.AddComponent<enemy>();
        var projectilePrefab = new GameObject("Projectile");
        projectilePrefab.AddComponent<Projectile>(); 
        var projectileRigidbody = projectilePrefab.AddComponent<Rigidbody2D>();
        enemyScript.projectileTemplate = projectilePrefab;
        enemyScript.player = playerObj.transform;
        enemyScript.SetBehavior(new AttackBehavior(enemyScript));

        enemyScript.ShootProjectile();
        yield return new WaitForSeconds(0.1f);

        var projectile = GameObject.Find("Projectile(Clone)");
        Assert.IsNotNull(projectile, "Projectile should be spawned");

        // Check if the Rigidbody2D is on the projectile
        var rb = projectile.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb, "Projectile should have a Rigidbody2D component");

        yield return null;
    }
}*/
