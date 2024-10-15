using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Alice_Test_Player_Prox {

    GameObject testObj;
    GameObject playerObj;
    float detects_range = 5f;

    [UnitySetUp]
    public IEnumerator Setup() {
        //create new game objs for both player and the enemy
        testObj = new GameObject("TestObject");
        playerObj = new GameObject("Player");

        // initial positions
        testObj.transform.position = Vector3.zero;  // (0,0,0)
        playerObj.transform.position = new Vector3(10f, 0f, 0f); // Player starts pretty far away

        yield return null; 
    }

    // Test: is the player inside the detection range?
    [UnityTest]
    public IEnumerator Test_Player_In_Proximity() { //move the player in range and see if message is output
        playerObj.transform.position = new Vector3(4f, 0f, 0f); // Move within range (detects_range = 5)

        float dist_to_player = Vector3.Distance(testObj.transform.position, playerObj.transform.position);
        // Assert true: player's in range
        Assert.IsTrue(dist_to_player <= detects_range, $"Player is in range. Distance: {dist_to_player}");

        yield return null;
    }
}