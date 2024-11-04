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
    public IEnumerator HurtTest()
    {
        //Gets UIManager from hierarchy
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        //Gets HealthUI from UIManager
        GameObject HealthUI = UIManager.transform.Find("HealthUI").gameObject;
        //Gets Health from Health UI
        GameObject Health = HealthUI.transform.Find("Health").gameObject;
        //Gets HealthFill from Health
        GameObject HealthFill = Health.transform.Find("HealthFill").gameObject;
        //Gets image from Healthfill
        Image health = HealthFill.GetComponentInChildren<Image>();
        GameObject player = GameObject.Find("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        float starting = health.fillAmount;
        yield return new WaitForSecondsRealtime(2);
        playerHealth.NotifyObserver(PlayerActions.Hurt);
        // Wait one frame for the Update method to run
        yield return null;

        // Assert that the pause menu is now active
        Assert.Less(health.fillAmount, starting, "Player health should lower");
    }

    [UnityTest]
    public IEnumerator HealTest()
    {
        //Gets UIManager from hierarchy
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        //Gets HealthUI from UIManager
        GameObject HealthUI = UIManager.transform.Find("HealthUI").gameObject;
        //Gets Health from Health UI
        GameObject Health = HealthUI.transform.Find("Health").gameObject;
        //Gets HealthFill from Health
        GameObject HealthFill = Health.transform.Find("HealthFill").gameObject;
        //Gets image from Healthfill
        Image health = HealthFill.GetComponentInChildren<Image>();
        GameObject player = GameObject.Find("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        float starting = health.fillAmount;
        playerHealth.NotifyObserver(PlayerActions.Hurt);
        yield return new WaitForSecondsRealtime(2);
        playerHealth.NotifyObserver(PlayerActions.Heal);
        yield return new WaitForSecondsRealtime(2);
        // Wait one frame for the Update method to run
        yield return null;

        // Assert that the pause menu is now active
        Assert.AreEqual(health.fillAmount, starting, "Player health should raise");
    }
}
