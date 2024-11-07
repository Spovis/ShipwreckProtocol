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
        BCMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
        BCModeToggle.isOn = BCMode;
    }
    public void setBCMode()
    {
        BCMode = !BCMode;
        PlayerPrefs.SetInt("BCMode", BCMode ? 1 : 0);
    }
}
