using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class UIDisplay
{
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        //loads primary scene for game
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }


    [UnityTest]
    public IEnumerator HealthDisplayActive()
    {
        //Gets the UIManager and Health UI from within UIManager
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject health = UIManager.transform.Find("HealthUI").gameObject;
        yield return new WaitForSecondsRealtime(2);

        // Assert that the Health UI is active
        Assert.IsTrue(health.activeSelf, "HealthUI should be on screen");
    }

    [UnityTest]
    public IEnumerator AmmoDisplayActive()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject ammo = UIManager.transform.Find("AmmoUI").gameObject;
        yield return new WaitForSecondsRealtime(2);

        // Assert that the pause menu is now active
        Assert.IsTrue(ammo.activeSelf, "AmmoUI should be on screen");
    }

    [UnityTest]
    public IEnumerator OverheatDisplayActive()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject AmmoUI = UIManager.transform.Find("AmmoUI").gameObject;
        //Gets Health from Health UI
        GameObject Ammo = AmmoUI.transform.Find("Ammo").gameObject;
        //Gets HealthFill from Health
        GameObject GunSpace = Ammo.transform.Find("GunSpace").gameObject;
        //Gets image from Healthfill
        Image fill = GunSpace.GetComponentInChildren<Image>();
        GameObject player = GameObject.Find("Player");
        PlayerInteractions playerHealth = player.GetComponent<PlayerInteractions>();
        yield return new WaitForSecondsRealtime(1);
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        yield return new WaitForSecondsRealtime(1);

        Assert.AreNotEqual(fill.fillAmount, 0, "Overheat gauge should fill up");
        yield return new WaitForSecondsRealtime(1);
    }

    [UnityTest]
    public IEnumerator OxygenNotActive()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject OxygenUI = UIManager.transform.Find("Oxygen").gameObject;
        //Gets Health from Health UI
        GameObject Slider = OxygenUI.transform.Find("Slider").gameObject;
        yield return new WaitForSecondsRealtime(1);

        Assert.IsFalse(Slider.activeSelf,"Oxygen gauge should be off");
        yield return new WaitForSecondsRealtime(1);
    }
}
