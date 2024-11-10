using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DeathStateTests {
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
  public IEnumerator DeathStateOnEnterState() {
    yield return new WaitUntil(() => hasSceneLoaded);
    BossState deathState = new DeathState();
    deathState.OnEnterState(bossObj.GetComponent<Animator>());

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnEnterState called"));

    yield return new WaitUntil(() => (bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death")));

    // Set him back to idle
    bossObj.GetComponent<Animator>().Play("Idle");

    yield return null;
  }

  [UnityTest]
  public IEnumerator DeathStateOnTriggerEnter2D() {
    BossState deathState = new DeathState();
    deathState.OnTriggerEnter2D(playerObj.GetComponent<Collider2D>(), bossObj.GetComponent<Boss>());

    yield return new WaitUntil(() => loggedMessages.Contains("abstract class definition of OnTriggerEnter2D called"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator DeathStateOnExitState() {
    BossState deathState = new DeathState();
    deathState.OnExitState(bossObj.GetComponent<Animator>());

    // DeathState doesnt have its own definition of OnExitState
    Assert.IsTrue(loggedMessages.Contains("abstract class definition of OnExitState called"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator DeathStateOnStateUpdate() {
    BossState deathState = new DeathState();
    deathState.OnEnterState(bossObj.GetComponent<Animator>());
    deathState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnStateUpdate called"));

    yield return new WaitForSeconds(2);

    deathState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    yield return new WaitUntil(() => (bossObj == null));

    yield return null;
  }
}