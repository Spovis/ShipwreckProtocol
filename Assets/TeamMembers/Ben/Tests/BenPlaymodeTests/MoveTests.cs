using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MoveTests
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
    public IEnumerator Simulate_Player_Move_Left() {
        
        yield return new WaitUntil(() => hasSceneLoaded);

        float startX, endX;

        startX = playerObj.transform.position.x;

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);

        yield return new WaitForSecondsRealtime(0.5f);

        endX = playerObj.transform.position.x;

        Assert.IsTrue(endX < startX, "Player should move left when input is set to -1.");
    }

    [UnityTest]
    public IEnumerator Simulate_Player_Move_Right()
    {

        yield return new WaitUntil(() => hasSceneLoaded);

        float startX, endX;

        startX = playerObj.transform.position.x;

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        PlayerInput.Instance.CurrentMovementInput = new Vector2(1, 0);

        yield return new WaitForSecondsRealtime(0.5f);

        endX = playerObj.transform.position.x;

        Assert.IsTrue(endX > startX, "Player should move left when input is set to -1.");
    }

    [UnityTest]
    public IEnumerator Test_Player_Facing_Correct_Direction()
    {

        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerInput.Instance.CurrentMovementInput = new Vector2(1, 0);
        yield return new WaitForSecondsRealtime(0.25f);
        if(!PlayerLogic.Instance.isFacingRight)
        {
            Assert.Fail("Player should be facing right when moving right.");
        }

        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);
        yield return new WaitForSecondsRealtime(0.25f);
        if (PlayerLogic.Instance.isFacingRight)
        {
            Assert.Fail("Player should be facing right when moving right.");
        }

        Assert.Pass("Player faces the correct directions when moving.");
    }
}
