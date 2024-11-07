using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuNavigationTests
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
    public IEnumerator PauseMenuOpen()
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

    [UnityTest]
    public IEnumerator PauseMenuClose()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.UI.Button resume = PauseMenu.transform.Find("ResumeButton").GetComponent<UnityEngine.UI.Button>();
        resume.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);
        // Assert that the pause menu is now active
        Assert.IsTrue(!PauseMenu.activeSelf, "Pause menu should be inactive");
        Assert.AreEqual(Time.timeScale, 1f, "Time scale should be 1");
    }

    [UnityTest]
    public IEnumerator PauseMenuCloseWithEsc()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
        // Assert that the pause menu is now active
        Assert.IsTrue(!PauseMenu.activeSelf, "Pause menu should be inactive");
        Assert.AreEqual(Time.timeScale, 1f, "Time scale should be 1");
    }

    [UnityTest]
    public IEnumerator SettingsMenuOpen()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        UnityEngine.UI.Button options = PauseMenu.transform.Find("SettingsButton").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
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
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        UnityEngine.UI.Button options = PauseMenu.transform.Find("SettingsButton").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
        options.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);
        GameObject SettingsMenu = GameObject.Find("SettingsMenu");
        UnityEngine.UI.Button back = SettingsMenu.transform.Find("Back").GetComponent<UnityEngine.UI.Button>();
        back.onClick.Invoke();
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(PauseMenu.activeSelf, "Pause menu should be active");
    }

    [UnityTest]
    public IEnumerator SettingsMenuCloseWithEsc()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        UnityEngine.UI.Button options = PauseMenu.transform.Find("SettingsButton").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
        options.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(PauseMenu.activeSelf, "Pause menu should be active");
    }

    [UnityTest]
    public IEnumerator ConfirmationMenuOpen()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        UnityEngine.UI.Button exit = PauseMenu.transform.Find("Exit").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
        exit.onClick.Invoke();
        GameObject ConfirmationMenu = GameObject.Find("ConfirmationMenu");
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(ConfirmationMenu.activeSelf, "Confirmation menu should be active");
    }

    [UnityTest]
    public IEnumerator ConfirmationMenuClose()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        UnityEngine.UI.Button exit = PauseMenu.transform.Find("Exit").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
        exit.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);
        GameObject ConfirmationMenu = GameObject.Find("ConfirmationMenu");
        UnityEngine.UI.Button back = ConfirmationMenu.transform.Find("No").GetComponent<UnityEngine.UI.Button>();
        back.onClick.Invoke();
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(PauseMenu.activeSelf, "Pause menu should be active");
    }

    [UnityTest]
    public IEnumerator GoToMainMenu()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject PauseMenu = UIManager.transform.Find("PauseMenu").gameObject;
        UnityEngine.UI.Button exit = PauseMenu.transform.Find("Exit").GetComponent<UnityEngine.UI.Button>();
        PlayerInput.Instance.CanInput = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        yield return new WaitForSecondsRealtime(2);
        exit.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);
        GameObject ConfirmationMenu = GameObject.Find("ConfirmationMenu");
        UnityEngine.UI.Button back = ConfirmationMenu.transform.Find("Yes").GetComponent<UnityEngine.UI.Button>();
        back.onClick.Invoke();
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(2);
        GameObject MainMenu = GameObject.Find("MainMenu");
        // Assert that the pause menu is now active
        Assert.IsTrue(MainMenu.activeSelf, "Pause menu should be active");
    }
}
