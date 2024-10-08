using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class HealthUpdate
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
    public IEnumerator HealthUpdateTest()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject HealthUI = UIManager.transform.Find("HealthUI").gameObject;
        Image health = HealthUI.GetComponentInChildren<Image>();
        PlayerInput.Instance.IsPausePressed = true;
        GameObject player = GameObject.Find("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.NotifyObserver(PlayerActions.Hurt);
        // Wait one frame for the Update method to run
        yield return null;

        // Assert that the pause menu is now active
        Assert.Less(health.fillAmount, 100, "Pause menu should be active when escape is pressed.");
    }
}
