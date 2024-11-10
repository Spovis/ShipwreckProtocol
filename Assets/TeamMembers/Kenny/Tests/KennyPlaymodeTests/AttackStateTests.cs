using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AttackStateTests {
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
  public IEnumerator AttackStateOnEnterState() {
    yield return new WaitUntil(() => hasSceneLoaded);
    BossState attackState = new AttackState();
    attackState.OnEnterState(bossObj.GetComponent<Animator>());

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnEnterState called"));

    yield return new WaitUntil(() => bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator AttackStateOnTriggerEnter2D() {
    BossState attackState = new AttackState();
    attackState.OnTriggerEnter2D(playerObj.GetComponent<Collider2D>(), bossObj.GetComponent<Boss>());

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnTriggerEnter2D called"));

    // shouldn't take damage from bumping into the player
    Assert.AreEqual(100, bossObj.GetComponent<Boss>().GetHealth());

    yield return null;
  }

  [UnityTest]
  public IEnumerator AttackStateOnStateUpdate() {
    BossState attackState = new AttackState();

    // move the player close to the boss so it remains in the attack state
    Vector2 playerPos = playerObj.transform.position;
    playerObj.transform.position = new Vector3(bossObj.transform.position.x - 2, bossObj.transform.position.y);

    attackState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    Assert.IsFalse(loggedMessages.Contains("abstract class definition of OnStateUpdate called"));

    yield return new WaitUntil(() => bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f);

    attackState.OnStateUpdate(bossObj, bossObj.GetComponent<Animator>(), new BossStateMachine(), bossObj.GetComponent<Boss>().fireballPrefab, playerObj.transform);

    yield return new WaitUntil(() => bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f);

    // move the player away from the boss so it leaves the attack state
    playerObj.transform.position = playerPos;

    yield return new WaitUntil(() => bossObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"));

    yield return null;
  }

  [UnityTest]
  public IEnumerator AttackStateOnExitState() {
    BossState attackState = new AttackState();
    attackState.OnExitState(bossObj.GetComponent<Animator>());

    // AttackState doesnt have its own definition of OnExitState
    Assert.IsTrue(loggedMessages.Contains("abstract class definition of OnExitState called"));

    yield return null;
  }
}
