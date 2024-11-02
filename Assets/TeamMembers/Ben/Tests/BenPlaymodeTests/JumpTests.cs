using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class JumpTests
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
    public IEnumerator Simulate_Player_Jump() {
        
        yield return new WaitUntil(() => hasSceneLoaded);

        float startY, endY;

        startY = playerObj.transform.position.y;

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        PlayerInput.Instance.IsJumpPressed = true;

        yield return new WaitForSecondsRealtime(0.5f);

        endY = playerObj.transform.position.y;

        Assert.IsTrue(endY > startY, "Player should move up when input is pressed.");
    }

    [UnityTest]
    public IEnumerator Check_Multi_Jump_Limit()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerLogic.Instance.MaxJumpCount = 3;

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        
        for(int i = 0; i < PlayerLogic.Instance.MaxJumpCount * 2; i++)
        {
            PlayerInput.Instance.IsJumpPressed = true;
            yield return null;
            PlayerInput.Instance.IsJumpPressed = false;

            yield return new WaitForSecondsRealtime(0.3f);

            Debug.Log("Jumps made: " + PlayerLogic.Instance.JumpCount + " / " + PlayerLogic.Instance.MaxJumpCount);

            if(PlayerLogic.Instance.JumpCount > PlayerLogic.Instance.MaxJumpCount)
            {
                Assert.Fail("Player should not be able to jump more than the max jump count.");
            }

            yield return null;
        }

        Assert.Pass("Player cannot jump more than MaxJumpCount");
    }

    [UnityTest]
    public IEnumerator Simulate_Player_Multi_Jump()
    {
        yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

        PlayerLogic.Instance.MaxJumpCount = 2;

        float startY, midY, endY;

        startY = playerObj.transform.position.y;

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerInput.Instance.IsJumpPressed = true;
        yield return null;
        PlayerInput.Instance.IsJumpPressed = false;

        yield return new WaitUntil(() => playerObj.GetComponent<Rigidbody2D>().velocity.y < 0); // Wait until we start moving downwards

        midY = playerObj.transform.position.y;

        PlayerInput.Instance.IsJumpPressed = true;
        yield return null;
        PlayerInput.Instance.IsJumpPressed = false;

        yield return new WaitUntil(() => playerObj.GetComponent<Rigidbody2D>().velocity.y < 0);

        endY = playerObj.transform.position.y;

        Assert.IsTrue(endY > midY && midY > startY, "Player should move up for each jump.");
    }
}
