using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crowbar : Items
{
    public static event Action OnCrowbarTouched;

    public override void Collect()
    {
        AddToPlayerInventory();
        //AudioManager.Instance.PlayFX("HealthPack");

        OnCrowbarTouched?.Invoke();
     
        Destroy(gameObject);
    }
}