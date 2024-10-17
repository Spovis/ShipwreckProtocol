using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PauseMenuTest
{
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PauseMenuTestActive()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(PauseMenu.activeSelf, "Pause menu should be active when escape is pressed.");
        Assert.AreEqual(Time.timeScale, 0f, "Time scale should be 0 when game is paused.");
    }
}
