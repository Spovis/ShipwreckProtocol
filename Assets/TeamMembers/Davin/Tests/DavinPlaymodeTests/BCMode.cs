using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class BCMode
{

    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }

    [UnityTest]
    public IEnumerator BCModeOff()
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
        Toggle bcmode = SettingsMenu.transform.Find("BCMode").GetComponent<Toggle>();
        if (bcmode.isOn)
        {
            bcmode.isOn = false;
        }
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        GameObject HealthUI = UIManager.transform.Find("HealthUI").gameObject;
        //Gets Health from Health UI
        GameObject Health = HealthUI.transform.Find("Health").gameObject;
        //Gets HealthFill from Health
        GameObject HealthFill = Health.transform.Find("HealthFill").gameObject;
        //Gets image from Healthfill
        HealthUI health = Health.GetComponentInChildren<HealthUI>();
        float start = health.getHealth();
        GameObject enemy = GameObject.Find("Hunter");
        enemy.SetActive(false);
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(10);
        float end = health.getHealth();

        Assert.Less(end, start, "Health should lower");
        yield return new WaitForSecondsRealtime(1);
    }

    [UnityTest]
    public IEnumerator BCModeOn()
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
        Toggle bcmode = SettingsMenu.transform.Find("BCMode").GetComponent<Toggle>();
        if (!bcmode.isOn)
        {
            bcmode.isOn = true;
        }
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        PlayerInput.Instance.IsPausePressed = true;
        yield return null;
        PlayerInput.Instance.IsPausePressed = false;
        GameObject HealthUI = UIManager.transform.Find("HealthUI").gameObject;
        //Gets Health from Health UI
        GameObject Health = HealthUI.transform.Find("Health").gameObject;
        //Gets HealthFill from Health
        GameObject HealthFill = Health.transform.Find("HealthFill").gameObject;
        //Gets image from Healthfill
        HealthUI health = Health.GetComponentInChildren<HealthUI>();
        float start = health.getHealth();
        GameObject enemy = GameObject.Find("Hunter");
        enemy.SetActive(false);
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);
        // Wait one frame for the Update method to run
        yield return new WaitForSecondsRealtime(10);
        float end = health.getHealth();

        Assert.AreEqual(end, start, "Health should be the same");
        yield return new WaitForSecondsRealtime(1);
    }
}
