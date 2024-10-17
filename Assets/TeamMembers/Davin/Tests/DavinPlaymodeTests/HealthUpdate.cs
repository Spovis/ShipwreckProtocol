using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class HealthUpdate
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
