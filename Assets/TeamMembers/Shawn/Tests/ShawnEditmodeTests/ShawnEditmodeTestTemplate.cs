using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShawnEditmodeTestTemplate {

    /* 
     * This is basically an empty test script. This test simply creates an empty GameObject and asserts that it is active.
     * The purpose of this existing is to ensure that tests are working for everyone. You can edit this test to be one you need,
     * or delete it and create your own test script. It also works as a basic template for a boundary test script.
    */

    GameObject testObj;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        // Creates a new GameObject and assigns it to the testObj variable
        testObj = new GameObject("TestObject");

        // Yield to allow for initialization
        yield return null;
    }



    [UnityTest]
    public IEnumerator TestObjectCreation() {

        // Assert that the test object is now active
        Assert.IsTrue(testObj.activeSelf, "Test object should be active when the test runs.");

        yield return null;
    }
}