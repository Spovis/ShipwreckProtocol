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

    public float currentFPS;
    private Queue<float> fpsHistory = new Queue<float>(); // Queue to store FPS history
    private const int fpsSampleSize = 10; // Number of frames to average over

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.LoadScene("Scenes/SampleScene");

        yield return null; 
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1) {
        hasSceneLoaded = true;
        GetReferences();
    }

    private void GetReferences() {
        playerObj = GameObject.Find("Player");
        playerLogic = playerObj.GetComponent<PlayerLogic>();

        rWall = GameObject.Find("RWall");
    }

    [UnityTest]
    public IEnumerator Simulate_Player_Move_Right() {

        bool hasFailed = false;

        bool startup = false;

        // ** Warm-up phase to let Unity stabilize FPS **
        int warmupFrames = 200;
        for (int w = 0; w < warmupFrames; w++)
        {
            yield return null;  // Wait for several frames to allow FPS to stabilize
        }

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
            currentFPS = 1.0f / Time.deltaTime;

            // Add current FPS to history
            fpsHistory.Enqueue(currentFPS);
            if (fpsHistory.Count > fpsSampleSize)
            {
                fpsHistory.Dequeue(); // Remove the oldest frame to keep the queue size constant
            }

            // Calculate the average FPS from the fpsHistory queue
            float averageFPS = 0f;
            foreach (float fps in fpsHistory)
            {
                averageFPS += fps;
            }
            averageFPS /= fpsHistory.Count;

            // Break the loop if average FPS drops below 60 after the startup phase
            if (currentFPS < 5)
            {
                Debug.Log(averageFPS + " average fps");
                Assert.Pass(i + " buttons drop average fps lower than 60");
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
            Assert.Pass("Game did not crash with " + bulletCount + " bullets.");
        }
    }
}
