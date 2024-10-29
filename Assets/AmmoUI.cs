using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour, IObserver
{
    //will observe player object put here
    [SerializeField] Subject player;
    TMP_Text text;
    private int ammo = 5;


    //when receiving a notification from subject
    public void OnNotify(PlayerActions action)
    {
        //switch case to perform different actions based off name
        switch (action)
        {
            case (PlayerActions.Fire):
                ammo--;
                if (ammo == 0)
                {
                    ammo = 5;
                }
                text.text = ("Ammo: " + ammo);
                Debug.Log("Fire Received");
                return;
        }
    }

    //adds object as observer when enable
    private void OnEnable()
    {
        player.addObserver(this);
        text = transform.Find("AmmoNum").GetComponent<TMP_Text>();
    }

    //removes object as observer when disabled to avoid unecessary signals
    private void OnDisable()
    {
        player.removeObserver(this);
    }
}
