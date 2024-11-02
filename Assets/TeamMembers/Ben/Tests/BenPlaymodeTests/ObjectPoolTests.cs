using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ObjectPoolTests
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
    public IEnumerator Test_Bullet_Object_Pool()
    {
        int activeBullets = 0;
        yield return new WaitUntil(() => hasSceneLoaded);

        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.

        for (int i = 0; i < 5; i++)
        {
            PlayerLogic.Instance.Shoot();
            yield return null;
        }

        Bullet[] firstWaveBullets = GetBulletsInScene(out activeBullets);
        Debug.Log(firstWaveBullets.Length + " bullets found.");
        Debug.Log(activeBullets + " bullets are active.");

        yield return new WaitForSecondsRealtime(Bullet.bulletLifetime); // Wait for all bullets to despawn

        Bullet[] secondWaveBullets = GetBulletsInScene(out activeBullets);
        Debug.Log(secondWaveBullets.Length + " bullets found.");
        Debug.Log(activeBullets + " bullets are active.");

        for (int i = 0; i < 5; i++)
        {
            PlayerLogic.Instance.Shoot();
            yield return null;
        }

        Bullet[] thirdWaveBullets = GetBulletsInScene(out activeBullets);
        Debug.Log(thirdWaveBullets.Length + " bullets found.");
        Debug.Log(activeBullets + " bullets are active.");

        Assert.IsTrue(firstWaveBullets.Length == secondWaveBullets.Length && secondWaveBullets.Length == thirdWaveBullets.Length, "The object pool should remain the same size as the same amount of bullets are spawned and subsequently despawned");
        Assert.IsTrue(activeBullets == thirdWaveBullets.Length, "There should be just as many bullets spawned as there are active in the scene after the second shooting.");
    }

    private Bullet[] GetBulletsInScene(out int activeCount)
    {
        Bullet[] bullets = GameObject.FindObjectsOfType<Bullet>(true);
        // get count of all the bullets that are active in the bullets array
        activeCount = 0;
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].gameObject.activeSelf)
            {
                activeCount++;
            }
        }

        return bullets;
    }
}
