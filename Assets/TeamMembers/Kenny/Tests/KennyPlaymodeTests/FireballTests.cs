using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FireballTests {
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
  public IEnumerator FireballsMoveUpAndLeft() {
    Rigidbody2D fireballPrefab = bossObj.GetComponent<Boss>().fireballPrefab;
    Rigidbody2D fireball = UnityEngine.Object.Instantiate(fireballPrefab, bossObj.transform.position, bossObj.transform.rotation);
    fireball.GetComponent<Fireball>().SetDirection(new Vector3(-4.0f, 1.0f, 0.0f));
    Vector2 startPos = fireball.transform.position;
    yield return new WaitForSeconds(0.1f);
    Vector2 endPos = fireball.transform.position;

    Assert.IsTrue(startPos.x > endPos.x);
    Assert.IsTrue(startPos.y < endPos.y);

    UnityEngine.Object.Destroy(fireball);
    yield return null;
  }

  [UnityTest]
  public IEnumerator FireballsMoveDownAndRight() {
    Rigidbody2D fireballPrefab = bossObj.GetComponent<Boss>().fireballPrefab;
    Rigidbody2D fireball = UnityEngine.Object.Instantiate(fireballPrefab, bossObj.transform.position, bossObj.transform.rotation);
    fireball.GetComponent<Fireball>().SetDirection(new Vector3(4.0f, -1.0f, 0.0f));
    Vector2 startPos = fireball.transform.position;
    yield return new WaitForSeconds(0.1f);
    Vector2 endPos = fireball.transform.position;

    Assert.IsTrue(startPos.x < endPos.x);
    Assert.IsTrue(startPos.y > endPos.y);

    UnityEngine.Object.Destroy(fireball);
    yield return null;
  }

  [UnityTest]
  public IEnumerator FireballHitsPlayer() {
    Rigidbody2D fireballPrefab = bossObj.GetComponent<Boss>().fireballPrefab;
    Vector2 playerPos = playerObj.transform.position;
    // spawn the fireball to the left of the player
    Vector2 fireballPos = new Vector2(playerPos.x - 2, playerPos.y);
    Rigidbody2D fireball = UnityEngine.Object.Instantiate(fireballPrefab, fireballPos, bossObj.transform.rotation);
    fireball.GetComponent<Fireball>().SetDirection(new Vector3(1.0f, 0.0f, 0.0f));
    // wait for the fireball to hit the player and Destroy() itself
    yield return new WaitUntil(() => fireball == null);
    yield return null;
  }
}