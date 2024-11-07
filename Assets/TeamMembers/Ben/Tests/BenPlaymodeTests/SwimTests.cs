using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SwimTests
{
    bool hasSceneLoaded = false;

    GameObject playerObj;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.LoadScene("Scenes/SampleScene");

        yield return null; 
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1) {
        hasSceneLoaded = true;

        playerObj = GameObject.Find("Player");
    }

    [UnityTest]
    public IEnumerator Test_Swim_Physics()
    {
        float startDrag = playerObj.GetComponent<Rigidbody2D>().drag;

        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject water = GameObject.Find("Water");
        if (water == null)
        {
            Assert.Fail("No water found in scene.");
        }

        playerObj.transform.position = water.transform.position;

        yield return null;
        if (!PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Swim))
        {
            Assert.Fail("Player should be in Swim state when in water.");
        }
        float endDrag = playerObj.GetComponent<Rigidbody2D>().drag;

        Assert.IsTrue(endDrag > startDrag, "Player's drag should increase when in water.");
    }

    [UnityTest]
    public IEnumerator Simulate_Player_Jump_In_Water() {

        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject water = GameObject.Find("Water");
        if (water == null)
        {
            Assert.Fail("No water found in scene.");
        }

        playerObj.transform.position = water.transform.position;

        yield return null;
        if(!PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Swim))
        {
            Assert.Fail("Player should be in Swim state when in water.");
        }

        float startY, endY;

        startY = playerObj.transform.position.y;

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerInput.Instance.IsJumpPressed = true;
        yield return null;
        PlayerInput.Instance.IsJumpPressed = false;

        yield return new WaitForSecondsRealtime(0.3f);

        endY = playerObj.transform.position.y;

        Assert.IsTrue(endY > startY, "Player should move up when input is pressed.");
    }

    [UnityTest]
    public IEnumerator Test_Swim_Particles()
    {
        yield return new WaitUntil(() => hasSceneLoaded);
        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        WaterSplashParticlePooling[] firstSplashes = GameObject.FindObjectsOfType<WaterSplashParticlePooling>(true);

        GameObject water = GameObject.Find("Water");
        if (water == null)
        {
            Assert.Fail("No water found in scene.");
        }

        playerObj.transform.position = water.transform.position;

        yield return new WaitForSeconds(0.1f);

        WaterSplashParticlePooling[] secondSplashes = GameObject.FindObjectsOfType<WaterSplashParticlePooling>(true);

        Assert.IsTrue(secondSplashes.Length > firstSplashes.Length, "Particles should play when player enters water.");
    }
}
