using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Overheat
{
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }


    [UnityTest]
    public IEnumerator ShootOverheat()
    {
        //Gets UIManager from hierarchy
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        //Gets HealthUI from UIManager
        GameObject AmmoUI = UIManager.transform.Find("AmmoUI").gameObject;
        //Gets Health from Health UI
        GameObject Ammo = AmmoUI.transform.Find("Ammo").gameObject;
        //Gets HealthFill from Health
        GameObject GunSpace = Ammo.transform.Find("GunSpace").gameObject;
        //Gets image from Healthfill
        Image fill = GunSpace.GetComponentInChildren<Image>();
        PlayerInput.Instance.CanInput = false;
        GameObject player = GameObject.Find("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        yield return new WaitForSecondsRealtime(1);
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        yield return null;
        Assert.AreNotEqual(fill.fillAmount, 0, "Overheat gauge should fill up");
        yield return new WaitForSecondsRealtime(2);
    }

    [UnityTest]
    public IEnumerator FullOverheat()
    {
        //Gets UIManager from hierarchy
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        //Gets HealthUI from UIManager
        GameObject AmmoUI = UIManager.transform.Find("AmmoUI").gameObject;
        //Gets Health from Health UI
        GameObject Ammo = AmmoUI.transform.Find("Ammo").gameObject;
        //Gets HealthFill from Health
        GameObject GunSpace = Ammo.transform.Find("GunSpace").gameObject;
        //Gets image from Healthfill
        Animator animator = GunSpace.GetComponent<Animator>();
        PlayerInput.Instance.CanInput = false;
        GameObject player = GameObject.Find("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        yield return new WaitForSecondsRealtime(1);
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        yield return null;
        Assert.IsTrue(animator.GetBool("Overheated"), "Overheat gauge should fill up");
        yield return new WaitForSecondsRealtime(2);
    }

    [UnityTest]
    public IEnumerator CoolDown()
    {
        //Gets UIManager from hierarchy
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        //Gets HealthUI from UIManager
        GameObject AmmoUI = UIManager.transform.Find("AmmoUI").gameObject;
        //Gets Health from Health UI
        GameObject Ammo = AmmoUI.transform.Find("Ammo").gameObject;
        //Gets HealthFill from Health
        GameObject GunSpace = Ammo.transform.Find("GunSpace").gameObject;
        //Gets image from Healthfill
        Animator animator = GunSpace.GetComponent<Animator>();
        PlayerInput.Instance.CanInput = false;
        GameObject player = GameObject.Find("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        yield return new WaitForSecondsRealtime(1);
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        playerHealth.NotifyObserver(PlayerActions.Fire);
        PlayerLogic.Instance.Shoot();
        yield return new WaitForSecondsRealtime(5);
        Assert.IsFalse(animator.GetBool("Overheated"), "Overheat gauge should fill up");
        yield return new WaitForSecondsRealtime(2);
    }
}
