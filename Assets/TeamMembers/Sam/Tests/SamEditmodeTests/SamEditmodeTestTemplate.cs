using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SamEditmodeTestTemplate {

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
    public IEnumerator TestRunRoom() {

        // Assert that the test object is now active
        Assert.IsTrue(testObj.activeSelf, "Test object should be active when the test runs.");

        yield return null;
    }
}