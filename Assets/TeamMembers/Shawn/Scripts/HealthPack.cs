using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPack : Items
{
    public static event Action OnHealthPackTouched;

    public override void Collect()
    {
        AddToPlayerInventory();
        AudioManager.Instance.PlayFX("HealthPack");

        OnHealthPackTouched?.Invoke();
     
        Destroy(gameObject);
    }
}