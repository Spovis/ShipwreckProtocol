using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test tests if the enemy is correctly id'ng if the player is
in the detection range or not.*/
public class Alice_Test_Player_Prox {

    GameObject testObj;
    GameObject playerObj;
    float detects_range = 15f;

    [UnitySetUp]
    public IEnumerator Setup() {
        //create new game objs for both player and the enemy
        testObj = new GameObject("TestObject");
        playerObj = new GameObject("Player");

        testObj.AddComponent<Animator>();
        playerObj.AddComponent<Animator>();

        testObj.transform.position = Vector3.zero;  // (0,0,0)
        playerObj.transform.position = new Vector3(10f, 0f, 0f); // Player starts pretty far away

        yield return null; 
    }

    //is player in range?
    [UnityTest]
    public IEnumerator Test_Player_In_Proximity() { //move the player in range and see if message is output
        playerObj.transform.position = new Vector3(4f, 0f, 0f); // Move within range (detects_range is 5 so 4 will work)

        float dist_to_player = Vector3.Distance(testObj.transform.position, playerObj.transform.position);
        //yes player is in range
        Assert.IsTrue(dist_to_player <= detects_range, $"Player is in range. Distance: {dist_to_player}");

        yield return null;
    }
}