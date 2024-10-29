using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPack : MonoBehaviour, ICollectables
{
    public static event Action OnHealthPackTouched;
    public void Collect()
    {
        //TBD Player Health ++, if Player Health == Full, exit, else do
        OnHealthPackTouched?.Invoke();
        Debug.Log("HealthPack Touched");
        AudioManager.Instance.PlayFX("HealthPack");
        Destroy(gameObject);
    }


}
