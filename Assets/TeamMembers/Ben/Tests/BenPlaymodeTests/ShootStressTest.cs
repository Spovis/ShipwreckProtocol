using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ShootStressTest
{
    private bool hasSceneLoaded = false;

    GameObject playerObj, rWall;
    PlayerLogic playerLogic;

    private int bulletCount;

    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.LoadScene("Scenes/SampleScene");

        yield return null; 
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1) {
        hasSceneLoaded = true;
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        GetReferences();
    }

    private void GetReferences() {
        playerObj = GameObject.Find("Player");
        playerLogic = playerObj.GetComponent<PlayerLogic>();

        rWall = GameObject.Find("RWall");
    }

    [UnityTest]
    public IEnumerator SimulateShootingExponentially() {

        bool hasFailed = false;

        bulletCount = 1;

        for (int i = 0; i < 500; i++) {
            SceneManager.LoadScene("Scenes/SampleScene");
            yield return new WaitUntil(() => hasSceneLoaded);
            GetReferences();

            Debug.Log("Shooting " + bulletCount + " bullets");

            PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

            for(int j = 0; j < bulletCount; j++)
            {
                PlayerLogic.Instance.Shoot();
            }

            // Calculate the current FPS using Time.deltaTime
            currentFPS = 1.0f / Time.unscaledDeltaTime;

            // Add current FPS to history
            fpsHistory.Enqueue(currentFPS);
            if (fpsHistory.Count > fpsSampleSize)
            {
                fpsHistory.Dequeue(); // Remove the oldest frame to keep the queue size constant
            }

            // Break the loop if average FPS drops below 60 after the startup phase
            if (currentFPS < 5)
            {
                Assert.Pass(bulletCount + " bullets drop average fps lower than 5");
                yield break;  // Exit the coroutine
            }

            hasSceneLoaded = false;
            // Double move speed if haven't failed
            bulletCount *= 2;
        }

        if(hasFailed) {
            //Assert.Fail("Player moved past right wall at speed of " + bulletCount);
        }
        else {
            Assert.Fail("Game did not crash with " + bulletCount + " bullets.");
        }
    }
}
