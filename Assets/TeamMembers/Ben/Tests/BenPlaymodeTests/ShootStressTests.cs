using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ShootStressTests
{
    private bool hasSceneLoaded = false;

    GameObject playerObj;
    private int bulletCount;

    private float averageFPS;
    private float fpsSum = 0.0f;
    private int fpsSampleCount = 0;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        hasSceneLoaded = true;
        GetReferences();
    }

    private void GetReferences()
    {
        playerObj = GameObject.Find("Player");
    }

    [UnityTest]
    public IEnumerator Test_Shooting_Exponentially()
    {
        bool hasFailed = false;
        bulletCount = 1;

        for (int i = 0; i < 500; i++)
        {
            SceneManager.LoadScene("Scenes/SampleScene");
            yield return new WaitUntil(() => hasSceneLoaded);
            GetReferences();

            Debug.Log("Shooting " + bulletCount + " bullets");

            PlayerInput.Instance.CanInput = false;

            for (int j = 0; j < bulletCount; j++)
            {
                PlayerLogic.Instance.Shoot();
            }

            yield return MeasureFPSOverTime();

            hasSceneLoaded = false;
            bulletCount *= 2;

            if (averageFPS < 20)
            {
                hasFailed = true;
                break;
            }
        }

        if (hasFailed)
        {
            Assert.Pass("FPS dropped below 20 with " + bulletCount + " bullets.");
        }
        else
        {
            Assert.Fail("FPS not affected with " + bulletCount + " bullets.");
        }
    }

    private IEnumerator MeasureFPSOverTime()
    {
        fpsSum = 0.0f;
        fpsSampleCount = 0;

        float duration = 1f;  // Measure FPS over 2 seconds
        float timer = 0f;

        while (timer < duration)
        {
            float currentFPS = 1.0f / Time.deltaTime;
            fpsSum += currentFPS;
            fpsSampleCount++;

            timer += Time.deltaTime;
            yield return null;
        }

        averageFPS = fpsSum / fpsSampleCount;
        Debug.Log("Average FPS: " + averageFPS);
    }
}
