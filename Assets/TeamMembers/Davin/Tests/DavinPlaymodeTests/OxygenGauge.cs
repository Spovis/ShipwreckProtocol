using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class OxygenGauge
{
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        //loads primary scene for game
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }

    [UnityTest]
    public IEnumerator OxygenGaugeAppears()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject OxygenUI = UIManager.transform.Find("Oxygen").gameObject;
        //Gets Health from Health UI
        GameObject Slider = OxygenUI.transform.Find("Slider").gameObject;
        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);
        yield return new WaitForSecondsRealtime(5);

        Assert.IsTrue(Slider.activeSelf, "Oxygen gauge should be on");
        yield return new WaitForSecondsRealtime(1);
    }

    [UnityTest]
    public IEnumerator OxygenGaugeLowers()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        GameObject enemy = GameObject.Find("Hunter");
        enemy.SetActive(false);
        GameObject OxygenUI = UIManager.transform.Find("Oxygen").gameObject;
        //Gets Health from Health UI
        Slider Slider = OxygenUI.transform.Find("Slider").gameObject.GetComponent<Slider>();
        PlayerInput.Instance.CanInput = false; // Stops the player from being able to change input. We will set inputs manually.
        PlayerInput.Instance.CurrentMovementInput = new Vector2(-1, 0);
        yield return new WaitForSecondsRealtime(5);
        float start = Slider.value;
        yield return new WaitForSecondsRealtime(3);
        float end = Slider.value;

        Assert.Less(end, start, "Oxygen gauge should be on");
        yield return new WaitForSecondsRealtime(1);
    }
}
