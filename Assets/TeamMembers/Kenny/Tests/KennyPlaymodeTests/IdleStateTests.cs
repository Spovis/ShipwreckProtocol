using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class IdleStateTests {
  GameObject bossObj;
  GameObject playerObj;
  bool hasSceneLoaded = false;
  private List<string> loggedMessages = new List<string>();

  [UnitySetUp]
  public IEnumerator Setup()
  {
    SceneManager.sceneLoaded += OnSceneLoad;
    SceneManager.LoadScene("Scenes/SampleScene");
    // Set up a custom log handler to capture all logs
    Application.logMessageReceived += CaptureLog;

    yield return null;
  }

  private void OnSceneLoad(Scene arg0, LoadSceneMode arg1) {
    bossObj = GameObject.Find("Boss");
    playerObj = GameObject.Find("Player");
    hasSceneLoaded = true;
  }

  private void CaptureLog(string condition, string stackTrace, LogType type)
  {
    // Add each logged message to the list
    loggedMessages.Add(condition);
  }

  [UnityTest]
  public IEnumerator IdleStateOnEnterState() {
    yield return new WaitUntil(() => hasSceneLoaded);
    BossState idleState = new IdleState();
    idleState.OnEnterState(bossObj.GetComponent<Animator>());

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnEnterState called"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator IdleStateOnTriggerEnter2D() {
    BossState idleState = new IdleState();
    idleState.OnTriggerEnter2D(playerObj.GetComponent<Collider2D>(), bossObj.GetComponent<Boss>());

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnTriggerEnter2D called"));

    // shouldn't take damage from bumping into the player
    Assert.AreEqual(100, bossObj.GetComponent<Boss>().GetHealth());

    yield return null;
  }

  [UnityTest]
  public IEnumerator IdleStateOnStateUpdate() {
    BossState idleState = new IdleState();
    idleState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnStateUpdate called"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator IdleStateOnExitState() {
    BossState idleState = new IdleState();
    idleState.OnExitState(bossObj.GetComponent<Animator>());

    // IdleState doesnt have its own definition of OnExitState
    Assert.IsTrue(loggedMessages.Contains("abstract class definition of OnExitState called"));

    yield return null;
  }
}