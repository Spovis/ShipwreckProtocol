using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Items : MonoBehaviour,ICollectables
{
    public static event Action PickUpItem;

    public virtual void Collect()
    {
        PickUpItem?.Invoke();
        Debug.Log("Item Picked Up"); //debug, needs removed
        //AudioManager.Instance.PlayFX("ItemPickUp"); //no sound yet
        Destroy(gameObject);
    }
}
