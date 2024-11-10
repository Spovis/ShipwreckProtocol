using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BCModeSaver : MonoBehaviour
{
    public bool BCMode = false;
    [SerializeField] Toggle BCModeToggle;

    public void loadBCMode()
    {
        //loads player pref and sets box to match
        BCMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
        BCModeToggle.isOn = BCMode;
    }
    public void setBCMode()
    {
        //saves to player prefs and switches box
        BCMode = !BCMode;
        PlayerPrefs.SetInt("BCMode", BCMode ? 1 : 0);
    }
}
