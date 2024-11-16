using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Items : MonoBehaviour
{
    public static event Action PickUpItem;

    // Use this when displaying inventory UI. If false, skip over it.
    public bool DisplayInInventory = true;

    protected void AddToPlayerInventory()
    {
        PlayerLogic.Instance.AddItemToInventory(this);
    }

    public virtual void Collect()
    {
        AddToPlayerInventory();
        PickUpItem?.Invoke();
        //AudioManager.Instance.PlayFX("ItemPickUp"); //no sound yet
        Destroy(gameObject);
    }
}
