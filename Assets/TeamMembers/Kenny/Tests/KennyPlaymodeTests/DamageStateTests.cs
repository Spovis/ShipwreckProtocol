using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DamageStateTests {
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
  public IEnumerator DamageStateOnEnterState() {
    yield return new WaitUntil(() => hasSceneLoaded);
    BossState damageState = new DamageState();
    damageState.OnEnterState(bossObj.GetComponent<Animator>());

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnEnterState called"));

    yield return new WaitUntil(() => bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Damage"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator DamageStateOnTriggerEnter2D() {
    BossState damageState = new DamageState();
    damageState.OnTriggerEnter2D(playerObj.GetComponent<Collider2D>(), bossObj.GetComponent<Boss>());

    Assert.IsTrue(loggedMessages.Contains("abstract class definition of OnTriggerEnter2D called"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator DamageStateOnStateUpdate() {
    BossState damageState = new DamageState();
    damageState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnStateUpdate called"));

    yield return new WaitForSeconds(2);

    damageState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    yield return new WaitUntil(() => bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator DamageStateOnExitState() {
    BossState damageState = new DamageState();
    damageState.OnExitState(bossObj.GetComponent<Animator>());

    // DamageState doesnt have its own definition of OnExitState
    Assert.IsTrue(loggedMessages.Contains("abstract class definition of OnExitState called"));

    yield return null;
  }
}