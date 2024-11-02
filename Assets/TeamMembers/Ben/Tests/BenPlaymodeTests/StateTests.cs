using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class StateTests
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
    public IEnumerator Test_Idle_State()
    {

        yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Idle), "Player should be in Idle state immediately.");
    }

    [UnityTest]
    public IEnumerator Test_Walk_State() {
        
        yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);

        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Walk), "Player should be in Walk state.");
    }

    [UnityTest]
    public IEnumerator Test_Jump_State()
    {
        yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerInput.Instance.IsJumpPressed = true;
        yield return null;
        PlayerInput.Instance.IsJumpPressed = false;

        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Jump), "Player should enter Jump state when jump is pressed and they are grounded.");
    }

    [UnityTest]
    public IEnumerator Test_Fall_State()
    {
        yield return new WaitUntil(() => hasSceneLoaded && PlayerStateMachine.Instance.IsGrounded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerInput.Instance.IsJumpPressed = true;
        yield return null;
        PlayerInput.Instance.IsJumpPressed = false;

        yield return new WaitUntil(() => playerObj.GetComponent<Rigidbody2D>().velocity.y < 0);
        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Fall), "Player should enter Fall state when falling.");
    }

    [UnityTest]
    public IEnumerator Test_Swim_State()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject water = GameObject.Find("Water");
        if (water == null)
        {
            Assert.Fail("No water found in scene.");
        }

        playerObj.transform.position = water.transform.position;

        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Swim), "Player should enter Swim state in water.");
    }

    [UnityTest]
    public IEnumerator Test_Tread_State()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject water = GameObject.Find("ShallowWater");
        if(water == null)
        {
            Assert.Fail("No shallow water found in scene.");
        }

        playerObj.transform.position = water.transform.position;

        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Tread), "Player should enter Tread state in shallow water.");
    }

    [UnityTest]
    public IEnumerator Test_Attack_State()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerInput.Instance.IsAttackPressed = true;
        yield return null;
        PlayerInput.Instance.IsAttackPressed = false;

        yield return null;

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Attack), "Player should enter Attack state when attack is pressed.");
    }

    [UnityTest]
    public IEnumerator Test_Fall_State_No_Jump()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        GameObject platform = GameObject.Find("Platform2");
        if (platform == null)
        {
            Assert.Fail("No platform found in scene.");
        }

        playerObj.transform.position = platform.transform.position + (Vector3.up * 2);

        yield return new WaitUntil(() => PlayerStateMachine.Instance.IsGrounded);
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);

        yield return new WaitUntil(() => !PlayerStateMachine.Instance.IsGrounded);

        Assert.IsTrue(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Fall), "Player should enter Fall state when walking off a platform.");
    }
}
