using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class MainMenuNavigation
{
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
        yield return null;
    }

    [UnityTest]
    public IEnumerator SettingsMenuOpen()
    {
        GameObject MainMenu = GameObject.Find("MainMenu");
        UnityEngine.UI.Button options = MainMenu.transform.Find("Options").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        options.onClick.Invoke();
        GameObject SettingsMenu = GameObject.Find("SettingsMenu");
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);
        // Assert that the pause menu is now active
        Assert.IsTrue(SettingsMenu.activeSelf, "Settings menu should be active");
    }

    [UnityTest]
    public IEnumerator SettingsMenuClose()
    {
        GameObject MainMenu = GameObject.Find("MainMenu");
        UnityEngine.UI.Button options = MainMenu.transform.Find("Options").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        options.onClick.Invoke();
        GameObject SettingsMenu = GameObject.Find("SettingsMenu");
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.UI.Button back = SettingsMenu.transform.Find("Back").GetComponent<UnityEngine.UI.Button>();
        back.onClick.Invoke();
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);
        // Assert that the pause menu is now active
        Assert.IsTrue(!SettingsMenu.activeSelf, "Settings menu should be active");
    }

    [UnityTest]
    public IEnumerator Play()
    {
        GameObject MainMenu = GameObject.Find("MainMenu");
        UnityEngine.UI.Button play = MainMenu.transform.Find("Play").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        play.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        GameObject SettingsMenu = GameObject.Find("SettingsMenu");
        // Wait one frame for the Update method to run
        
        // Assert that the pause menu is now active
        Assert.AreEqual(sceneName, "SampleScene", "Settings menu should be active");
    }
}
