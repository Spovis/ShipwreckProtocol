using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class KennyTestTakeDamage {

    /*
     * This is basically an empty test script. This test simply creates an empty GameObject and asserts that it is active.
     * The purpose of this existing is to ensure that tests are working for everyone. You can edit this test to be one you need,
     * or delete it and create your own test script. It also works as a basic template for a boundary test script.
    */

    GameObject bossObj;
    bool hasSceneLoaded = false;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.LoadScene("Scenes/SampleScene");

        yield return null;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1) {
        bossObj = GameObject.Find("Boss");
        hasSceneLoaded = true;
    }


    [UnityTest]
    public IEnumerator TestTakeDamage() {
        Assert.IsTrue(bossObj.activeSelf, "Boss should be active when the test runs.");
        bossObj.GetComponent<Boss>().TakeDamage(1);
        Assert.AreEqual(99, bossObj.GetComponent<Boss>().GetHealth(), "Boss should have 99 health after taking 1 damage.");

        yield return null;
    }
}