using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test tests if the enemy is correctly id'ng if the player is
in the detection range or not.*/
public class Alice_Test_PlayerOut_Prox {

    GameObject testObj;
    GameObject playerObj;
    float detects_range = 5f;  
    [UnitySetUp]
    public IEnumerator Setup() {
       
        testObj = new GameObject("TestObject");
        playerObj = new GameObject("Player");
        testObj.AddComponent<Animator>();
        playerObj.AddComponent<Animator>();

        // Initial positions
        testObj.transform.position = Vector3.zero; 
        playerObj.transform.position = new Vector3(10f, 0f, 0f); //Player starts pretty far away

        yield return null; 
    }
    [UnityTest]
    public IEnumerator Test_Player_Out_Of_Proximity() {
        playerObj.transform.position = new Vector3(10f, 0f, 0f); // Move beyond range (detects_range is 5, so 10 is out of range)

        float dist_to_player = Vector3.Distance(testObj.transform.position, playerObj.transform.position);
        
        // Check if player's out of range
        Assert.IsTrue(dist_to_player > detects_range, $"Player is out of range. Distance: {dist_to_player}");

        yield return null;
    }
}

    