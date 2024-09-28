using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    //script to put on observer objects, will observe a subject and will receive notifications

    //prototype function so each observer can have custom actions
    public void OnNotify(PlayerActions action)
    {

    }
}
