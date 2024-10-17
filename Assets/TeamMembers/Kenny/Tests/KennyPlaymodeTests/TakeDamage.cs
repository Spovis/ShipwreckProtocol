using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class KennyTestTakeDamage {
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

    [UnityTest]
    public IEnumerator TestTakeALotOfDamage() {
        Assert.IsTrue(bossObj.activeSelf, "Boss should be active when the test runs.");
        bossObj.GetComponent<Boss>().TakeDamage(1000);
        Assert.AreEqual(0, bossObj.GetComponent<Boss>().GetHealth(), "Boss should have 0 health after taking more than 100 damage.");

        yield return null;
    }
}