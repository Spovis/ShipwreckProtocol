using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PauseTest {
    GameObject pauseMenu;
    GameObject uiManagerObject;
    GameObject playerInputObject;

    PlayerInput playerInput;

    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        // Create a new GameObject and attach the UIManager script dynamically
        uiManagerObject = new GameObject("UIManagerObject");

        playerInputObject = new GameObject("PlayerInput");

        // Add the UIManager component by referencing the type directly
        uiManagerObject.AddComponent<UIManager>();

        playerInput = playerInputObject.AddComponent<PlayerInput>();

        // Create a pause menu GameObject and ensure it's inactive
        pauseMenu = new GameObject("PauseMenu");
        pauseMenu.SetActive(false);

        // Use reflection to set the pause menu on the UIManager
        var uiManager = uiManagerObject.GetComponent<UIManager>();  // Get the actual component
        var pauseMenuField = uiManager.GetType().GetField("pauseMenu", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        pauseMenuField.SetValue(uiManager, pauseMenu);

        // Yield to allow for initialization
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestPauseMenuOpensWhenEscapePressed() {
        // Simulate pressing the Escape key
        PlayerInput.Instance.IsPausePressed = true;

        // Wait one frame for the Update method to run
        yield return null;

        // Assert that the pause menu is now active
        Assert.IsTrue(pauseMenu.activeSelf, "Pause menu should be active when escape is pressed.");
        Assert.AreEqual(Time.timeScale, 0f, "Time scale should be 0 when game is paused.");
    }
}