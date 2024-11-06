using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this boundary test looks at if the enemy can correctly identify if the player
is out of bounds, even if the player is in detection range. */
public class BoundaryPlaymodeTest {

    GameObject player;
    GameObject enemy;
    Vector2 boundaryMin = new Vector2(-0.03f, 9.940499f);  
    Vector2 boundaryMax = new Vector2(116.05f, 9.940499f);

    //setting up the test
    [UnitySetUp]
    public IEnumerator Setup()  
    {
        //add a player object
        player = new GameObject("Player");
        player.AddComponent<Rigidbody2D>();

        //enemy object
        enemy = new GameObject("Enemy");
        enemy.AddComponent<Rigidbody2D>();
        enemy.AddComponent<enemy>();
        
        //put in player and enemy
        player.transform.position = new Vector3(0, 0, 0); 
        enemy.transform.position = new Vector3(-5, 0, 0);  //Enemy at random loc

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerWithinBoundaryTest()
    {
        //Move the player inside the boundary
        player.transform.position = new Vector3(5, 5, 0);
        yield return null;  

        // Check if the player's position is within the boundary
        Assert.IsTrue(PlayerInBounds(), "Player is within the boundaries.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerOutsideBoundaryTest()
    {
        
        player.transform.position = new Vector3(-5, 15, 0);//out of bounds
        yield return null;  

        Assert.IsFalse(PlayerInBounds(), "Player outside the boundaries.");

        yield return null;
    }

    private bool PlayerInBounds()
    {
        Vector3 playerPos = player.transform.position;
        return playerPos.x >= boundaryMin.x && playerPos.x <= boundaryMax.x &&
               playerPos.y >= boundaryMin.y && playerPos.y <= boundaryMax.y;
    }
}
