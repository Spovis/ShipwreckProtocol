using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tuna : Items
{
    public static event Action OnTunaTouched;

    public override void Collect()
    {
        AddToPlayerInventory();
        PlayerLogic.Instance.IncrementMaxJumpCount();
        //AudioManager.Instance.PlayFX("HealthPack");

        PopupText.Show("+1 Jumps");
        OnTunaTouched?.Invoke();
     
        Destroy(gameObject);
    }
}