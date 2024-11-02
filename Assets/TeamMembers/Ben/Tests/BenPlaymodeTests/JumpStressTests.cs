using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class JumpStressTests
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
    public IEnumerator Player_Jump_Expoenential_Stress()
    {

        yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

        bool hasFailed = false;

        float jumpForce = PlayerLogic.Instance.JumpForce;

        for (int i = 0; i < 500; i++)
        {
            Debug.Log("Jump Force - " + jumpForce); // Only doing it as a warning so I can hide other Debug.Logs
            if (jumpForce == Mathf.Infinity)
            {
                Assert.Pass("Jump force reached infinity");
            }

            SceneManager.LoadScene("Scenes/SampleScene");
            yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

            PlayerLogic.Instance.JumpForce = jumpForce;

            PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

            PlayerInput.Instance.IsJumpPressed = true;
            yield return null;
            PlayerInput.Instance.IsJumpPressed = false;

            yield return new WaitForSecondsRealtime(0.3f);
            Debug.Log("Player Y Pos - " + playerObj.transform.position.y); // Only doing it as an error so I can hide other Debug.Logs

            if (playerObj.transform.position.y > 100)
            {
                hasFailed = true;
                break;
            }

            hasSceneLoaded = false;
            // Triple jump force if haven't failed
            jumpForce *= 3;
        }

        if (hasFailed)
        {
            Assert.Pass("Player moved through ceiling at jump force of " + jumpForce);
        }
        else
        {
            Assert.Fail("Player did not get past ceiling. Reached total jump force of " + jumpForce);
        }
    }
}
