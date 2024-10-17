using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SamPlaymodeTestTemplate {

    /* 
     * This is basically an empty test script. This test simply creates an empty GameObject and asserts that it is active.
     * The purpose of this existing is to ensure that tests are working for everyone. You can edit this test to be one you need,
     * or delete it and create your own test script. It also works as a basic template for a boundary test script.
    */

    GameObject testObj;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        yield return null;
    }



    [UnityTest]
    public IEnumerator StressTest() {

        for(int i = 0; i > 100; i++)
        {
        testObj = new GameObject("TestObject");
        }
        yield return null;
    }
}