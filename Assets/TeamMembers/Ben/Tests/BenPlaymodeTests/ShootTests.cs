using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ShootTests
{
    private bool hasSceneLoaded = false;

    GameObject playerObj;

    public float currentFPS;

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
    }

    [UnityTest]
    public IEnumerator Simulate_Player_Shoot() 
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerLogic.Instance.Shoot();

        yield return null;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerAttack");

        Assert.IsTrue(bullets.Length > 0, "Bullet(s) should be instantiated when player shoots.");
    }

    [UnityTest]
    public IEnumerator Test_Bullet_Despawning()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        for (int i = 0; i < 5; i++)
        {
            PlayerLogic.Instance.Shoot();
            yield return null;
        }

        yield return new WaitForSecondsRealtime(Bullet.bulletLifetime);

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerAttack");

        Assert.IsTrue(bullets.Length <= 0, "Bullet(s) should despawn no later than bullet lifetime.");
    }

    [UnityTest]
    public IEnumerator Test_Player_Shoot_Particles()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        PlayerLogic.Instance.Shoot();

        yield return null;

        Assert.IsTrue(PlayerLogic.Instance.isShootParticlePlaying, "Particles should play when player shoots.");
    }
}
