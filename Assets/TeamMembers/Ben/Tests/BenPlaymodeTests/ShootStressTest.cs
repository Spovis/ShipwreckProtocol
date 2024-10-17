using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ShootStressTest : MonoBehaviour
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
