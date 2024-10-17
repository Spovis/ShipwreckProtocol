using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PauseMenuTest
{
    bool sceneLoaded = false;
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneLoaded = true;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PauseMenuTestActive()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        PlayerInput.Instance.IsPausePressed = true;
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(PauseMenu.activeSelf, "Pause menu should be active when escape is pressed.");
        Assert.AreEqual(Time.timeScale, 0f, "Time scale should be 0 when game is paused.");
    }
}