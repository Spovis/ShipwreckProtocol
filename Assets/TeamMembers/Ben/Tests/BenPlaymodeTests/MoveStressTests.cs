using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MoveStressTests
{
    private bool hasSceneLoaded = false;

    GameObject playerObj, rWall, lWall;
    PlayerLogic playerLogic;

    private float playerSpeed;

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
        lWall = GameObject.Find("LWall");
    }

    [UnityTest]
    public IEnumerator Test_Player_Move_Left_Exponential() {

        bool hasFailed = false;

        playerSpeed = playerLogic.MoveSpeed;

        for (int i = 0; i < 500; i++) {
            Debug.Log("Speed Modifier - " + playerSpeed); // Only doing it as a warning so I can hide other Debug.Logs
            if (playerSpeed == Mathf.Infinity) {
                Assert.Pass("Player speed reached infinity");
            }

            SceneManager.LoadScene("Scenes/SampleScene");
            yield return new WaitUntil(() => hasSceneLoaded);
            GetReferences();

            PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
            PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);

            // Sets player's speed
            playerLogic.MoveSpeed = playerSpeed;

            yield return new WaitForSeconds(0.5f);
            Debug.Log("Player Velocity - " + playerObj.GetComponent<Rigidbody2D>().velocity); // Only doing it as an error so I can hide other Debug.Logs

            if (playerObj.transform.position.x <= lWall.transform.position.x) {
                hasFailed = true;
                break;
            }

            hasSceneLoaded = false;
            // Quadruple move speed if haven't failed
            playerSpeed *= 4;
        }

        if(hasFailed) {
            Assert.Pass("Player moved past right wall at speed of " + playerSpeed);
        }
        else {
            Assert.Fail("Player did not move past right wall. Reached total speed of " + playerSpeed);
        }
    }

    [UnityTest]
    public IEnumerator Test_Player_Move_Right_Exponential()
    {

        bool hasFailed = false;

        playerSpeed = playerLogic.MoveSpeed;

        for (int i = 0; i < 500; i++)
        {
            Debug.Log("Speed Modifier - " + playerSpeed); // Only doing it as a warning so I can hide other Debug.Logs
            if (playerSpeed == Mathf.Infinity)
            {
                Assert.Pass("Player speed reached infinity");
            }

            SceneManager.LoadScene("Scenes/SampleScene");
            yield return new WaitUntil(() => hasSceneLoaded);
            GetReferences();

            PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
            PlayerInput.Instance.CurrentMovementInput = new Vector2(1, 0);

            // Sets player's speed
            playerLogic.MoveSpeed = playerSpeed;

            yield return new WaitForSeconds(0.5f);
            Debug.Log("Player Velocity - " + playerObj.GetComponent<Rigidbody2D>().velocity); // Only doing it as an error so I can hide other Debug.Logs

            if (playerObj.transform.position.x >= rWall.transform.position.x)
            {
                hasFailed = true;
                break;
            }

            hasSceneLoaded = false;
            // Quadruple move speed if haven't failed
            playerSpeed *= 4;
        }

        if (hasFailed)
        {
            Assert.Pass("Player moved past right wall at speed of " + playerSpeed);
        }
        else
        {
            Assert.Fail("Player did not move past right wall. Reached total speed of " + playerSpeed);
        }
    }
}
