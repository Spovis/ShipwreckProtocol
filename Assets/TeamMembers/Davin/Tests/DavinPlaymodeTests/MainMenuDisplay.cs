using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MainMenuDisplay
{
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        //loads primary scene for game
        SceneManager.LoadScene("Scenes/MainMenu");
        yield return null;
    }


    [UnityTest]
    public IEnumerator TitleActive()
    {
        yield return new WaitForSecondsRealtime(1);
        //Gets the UIManager and Health UI from within UIManager
        GameObject title = GameObject.Find("TitleArea");
        yield return new WaitForSecondsRealtime(2);

        // Assert that the Health UI is active
        Assert.IsTrue(title.activeSelf, "Title should be on screen");
    }

    [UnityTest]
    public IEnumerator MenuActive()
    {
        //Gets the UIManager and Health UI from within UIManager
        GameObject menu = GameObject.Find("MainMenu");
        yield return new WaitForSecondsRealtime(2);

        // Assert that the Health UI is active
        Assert.IsTrue(menu.activeSelf, "Menu should be on screen");
    }
}
