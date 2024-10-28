using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SamPlaymodeTestTemplate {



    GameObject testObj;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        yield return null;
    }



    [UnityTest]
    public IEnumerator StressTest() {

        for(int i = 0; i > 10000; i++)
        {
        testObj = new GameObject("TestObject");
        }
        yield return null;
    }
}