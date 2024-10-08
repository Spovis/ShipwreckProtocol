using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MoveLeftTest
{
    GameObject testObj;

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
    public IEnumerator TestObjectCreation() {

        float startX, endX;

        startX = playerObj.transform.position.x;

        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);

        yield return new WaitForSeconds(0.5f);

        endX = playerObj.transform.position.x;

        Assert.IsTrue(endX < startX, "Player should move left when input is set to -1.");
    }
}
