using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class KennyStressTest {
    public GameObject bossPrefab;

    private List<GameObject> bossPool = new List<GameObject>();
    private bool isStressing = false;
    public float current;

    bool sceneLoaded = false;
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return new WaitUntil(() => sceneLoaded);  // Wait until scene is fully loaded

        bossPrefab = GameObject.Find("Boss");
        yield return null;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneLoaded = true;
    }

    [UnityTest]
    public IEnumerator StressTest() {

        bool startup = false;
        // Pre-create and pool a large number of buttons
        int poolSize = 10000;
        for (int i = 0; i < poolSize; i++)
        {
            // Instead of using Instantiate during the test, we can set up the pool
            GameObject newBoss = new GameObject("Boss " + i);
            Boss bossComponent = newBoss.AddComponent<Boss>();
            bossPool.Add(newBoss);
            current = (int)(Time.frameCount / Time.time);
            yield return null;
            yield return null;
            yield return null;
            if(current > 200)
            {
                startup = true;
            }
            if (startup == true && current < 200)
            {
                Debug.Log(current + " current fps");
                Assert.Pass(current + " fps is lower than expected");
            }
        }

            Assert.Fail(current + " fps is higher than expected");

        // Yield to let Unity handle the frame and avoid locking the engine
        yield return null;
    }
}