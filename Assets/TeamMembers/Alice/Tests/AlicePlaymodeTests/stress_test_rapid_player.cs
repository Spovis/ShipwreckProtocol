using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*this test looks at if the enemy can keep up with the player moving in
and out rapidly.*/
public class test_boundary_detection {


    GameObject testObj;
    GameObject playerObj;
    float detects_range = 5f;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        // Creates a new GameObject and assigns it to the testObj variable
        testObj = new GameObject("TestObject");
        playerObj = new GameObject("Player");
        testObj.transform.position = Vector3.zero;  //place enemy 
        playerObj.transform.position = new Vector3(10f, 0f, 0f);//place player out of range  
        // Yield to allow for initialization
        yield return null;
    }



    [UnityTest]
    public IEnumerator stress_test_player_movement() {
        playerObj.transform.position = new Vector3(10f, 0f, 0f);
        for(int i = 0; i < 500; i++){ //could be changed to a while loop too if want guarantteeed crash
            playerObj.transform.position = new Vector3(4f, 0f, 0f); // Move within range (detects_range is 5 so 4 will work)
            float dist_to_player = Vector3.Distance(testObj.transform.position, playerObj.transform.position);
            //true: player's in range
            Assert.IsTrue(dist_to_player <= detects_range, "Player is in range");
            //move player out
            playerObj.transform.position = new Vector3(10f, 0f, 0f); //Move player away
            dist_to_player = Vector3.Distance(testObj.transform.position, playerObj.transform.position);
            //false: player's not in range
            Assert.IsFalse(dist_to_player <= detects_range, "Player is not in range. ");
            yield return null;
        }
        
        yield return null;
    }
}