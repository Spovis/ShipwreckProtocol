using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DeathTest {
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
    hasSceneLoaded = true;
  }

  [UnityTest]
  public IEnumerator PlayerCanKillBoss() {
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

    yield return new WaitUntil(() => bossObj.GetComponent<Boss>().GetState().GetType() == typeof(DamageState));
    yield return new WaitUntil(() => bossObj.GetComponent<Boss>().GetState().GetType() == typeof(DeathState));
  }
}