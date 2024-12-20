using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goggles : Items
{
    public static event Action OnGogglesTouched;

    public override void Collect()
    {
        AddToPlayerInventory();
        //AudioManager.Instance.PlayFX("HealthPack");

        PopupText.Show("Goggles Collected");
        OnGogglesTouched?.Invoke();
     
        Destroy(gameObject);
    }
}