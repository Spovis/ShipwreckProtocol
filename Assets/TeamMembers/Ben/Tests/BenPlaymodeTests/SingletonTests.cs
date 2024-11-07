using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SingletonTests
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
    public IEnumerator Test_StateMachine_Singleton() 
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject newMachine = new GameObject("New State Machine");
        newMachine.AddComponent<PlayerStateMachine>();

        yield return null;

        PlayerStateMachine[] machines = GameObject.FindObjectsOfType<PlayerStateMachine>(true);

        Assert.IsTrue(machines.Length <= 1, "There should only ever be one instance of the state machine.");
    }

    [UnityTest]
    public IEnumerator Test_PlayerLogic_Singleton()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject newLogic = new GameObject("New Player Logic");
        newLogic.AddComponent<PlayerLogic>();

        yield return null;

        PlayerLogic[] logics = GameObject.FindObjectsOfType<PlayerLogic>(true);

        Assert.IsTrue(logics.Length <= 1, "There should only ever be one instance of player logic.");
    }

    [UnityTest]
    public IEnumerator Test_PlayerInput_Singleton()
    {
        yield return new WaitUntil(() => hasSceneLoaded);

        GameObject newInput = new GameObject("New Player Input");
        newInput.AddComponent<PlayerInput>();

        yield return null;

        PlayerInput[] inputs = GameObject.FindObjectsOfType<PlayerInput>(true);

        Assert.IsTrue(inputs.Length <= 1, "There should only ever be one instance of player input.");
    }
}
