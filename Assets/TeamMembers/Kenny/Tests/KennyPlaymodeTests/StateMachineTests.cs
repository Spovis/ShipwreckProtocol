using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class StateMachineTests {
  Animator testAnimator;
  GameObject bossObj;
  GameObject playerObj;
  bool hasSceneLoaded = false;

  [UnitySetUp]
  public IEnumerator Setup()
  {
    SceneManager.sceneLoaded += OnSceneLoad;
    SceneManager.LoadScene("Scenes/SampleScene");

    yield return null;
  }

  private void OnSceneLoad(Scene arg0, LoadSceneMode arg1) {
    bossObj = GameObject.Find("Boss");
    playerObj = GameObject.Find("Player");
    testAnimator = bossObj.GetComponent<Animator>();
    hasSceneLoaded = true;
  }

  [UnityTest]
  public IEnumerator InitializesToIdleState() {
    BossStateMachine stateMachine = new BossStateMachine();
    BossState state = stateMachine.GetState();

    Debug.Log("typeof(IdleState):"  + typeof(IdleState));
    Debug.Log("state.GetType(): " + state.GetType());
    Assert.AreEqual(typeof(IdleState), state.GetType());

    yield return null;
  }

  [UnityTest]
  public IEnumerator TransitionsToDeathState() {
    BossStateMachine stateMachine = new BossStateMachine();
    stateMachine.UpdateState(1, testAnimator);
    BossState state = stateMachine.GetState();

    Debug.Log("typeof(DeathState):"  + typeof(DeathState));
    Debug.Log("state.GetType(): " + state.GetType());
    Assert.AreEqual(typeof(DeathState), state.GetType());

    yield return null;
  }

  [UnityTest]
  public IEnumerator TransitionsToDamageState() {
    BossStateMachine stateMachine = new BossStateMachine();
    stateMachine.UpdateState(2, testAnimator);
    BossState state = stateMachine.GetState();

    Debug.Log("typeof(DamageState):"  + typeof(DamageState));
    Debug.Log("state.GetType(): " + state.GetType());
    Assert.AreEqual(typeof(DamageState), state.GetType());

    yield return null;
  }

  [UnityTest]
  public IEnumerator TransitionsToAttackState() {
    BossStateMachine stateMachine = new BossStateMachine();
    stateMachine.UpdateState(3, testAnimator);
    BossState state = stateMachine.GetState();

    Debug.Log("typeof(AttackState):"  + typeof(AttackState));
    Debug.Log("state.GetType(): " + state.GetType());
    Assert.AreEqual(typeof(AttackState), state.GetType());

    yield return null;
  }

  [UnityTest]
  public IEnumerator PlayerTriggersAttackState() {
    // Should start in IdleState
    Assert.AreEqual(typeof(IdleState), bossObj.GetComponent<Boss>().GetState().GetType());

    Vector2 playerPos = playerObj.transform.position;
    // Move player to be next to the boss
    playerObj.transform.position = new Vector2(bossObj.transform.position.x + 2, bossObj.transform.position.y);

    // Boss should transition to AttackState
    // wait for .1 s
    yield return new WaitForSeconds(0.1f);
    Assert.AreEqual(typeof(AttackState), bossObj.GetComponent<Boss>().GetState().GetType());
    yield return null;
  }

  [UnityTest]
  public IEnumerator PlayerLeavingTriggersIdleState() {
    // Start the same as the previous test
    Assert.AreEqual(typeof(IdleState), bossObj.GetComponent<Boss>().GetState().GetType());

    Vector2 playerPos = playerObj.transform.position;
    playerObj.transform.position = new Vector2(bossObj.transform.position.x + 2, bossObj.transform.position.y);
    yield return new WaitForSeconds(0.1f);
    Assert.AreEqual(typeof(AttackState), bossObj.GetComponent<Boss>().GetState().GetType());

    // Move the player back away from the boss
    playerObj.transform.position = playerPos;

    // Boss should transition to IdleState
    // wait for .1 s
    yield return new WaitForSeconds(0.1f);
    Assert.AreEqual(typeof(IdleState), bossObj.GetComponent<Boss>().GetState().GetType());

    yield return null;
  }

  [UnityTest]
  public IEnumerator PlayerShootingTriggersDamageState() {
    // Should start in IdleState
    Assert.AreEqual(typeof(IdleState), bossObj.GetComponent<Boss>().GetState().GetType());

    Vector2 playerPos = playerObj.transform.position;
    playerObj.transform.position = new Vector2(bossObj.transform.position.x + 12, bossObj.transform.position.y);
    yield return new WaitForSeconds(0.1f);
    // Make sure the player is facing the boss, by running to the left
    PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
    PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);
    yield return new WaitForSecondsRealtime(0.1f);
    PlayerInput.Instance.CurrentMovementInput = new Vector2(0, 0);

    // Now shoot the boss
    PlayerInput.Instance.IsAttackPressed = true;
    yield return new WaitForSeconds(0.1f);
    PlayerInput.Instance.IsAttackPressed = false;

    yield return new WaitUntil(() => bossObj.GetComponent<Boss>().GetState().GetType() == typeof(DamageState));
    yield return new WaitUntil(() => bossObj.GetComponent<Boss>().GetState().GetType() == typeof(IdleState));
  }
}