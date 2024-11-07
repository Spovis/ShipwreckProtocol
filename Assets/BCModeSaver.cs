using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BCModeSaver : MonoBehaviour
{
    public bool BCMode = false;
    [SerializeField] Toggle BCModeToggle;
    GameObject settingsMenu;

    public void Awake()
    {
        settingsMenu = GameObject.Find("SettingsMenu");
        settingsMenu.SetActive(true);
        BCMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
        BCModeToggle.isOn = BCMode;
        settingsMenu.SetActive(false);
    }

    public void setBCMode()
    {
        BCMode = !BCMode;
        PlayerPrefs.SetInt("BCMode", BCMode ? 1 : 0);
        PlayerPrefs.Save();
        BCModeToggle.isOn = BCMode;
    }
}
