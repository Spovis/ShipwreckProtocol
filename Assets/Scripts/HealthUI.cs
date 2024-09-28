using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IObserver
{
    //will observe player object put here
    [SerializeField] Subject player;
    [SerializeField] Sprite halfHealth;
    [SerializeField] Sprite fullHealth;
    Image current;


    //when receiving a notification from subject
    public void OnNotify(PlayerActions action)
    {
        //switch case to perform different actions based off name
        switch(action)
        {
            case (PlayerActions.Hurt):
                current.sprite = halfHealth;
                return;
            case (PlayerActions.Heal):
                current.sprite = fullHealth;
                return;
        }
    }

    //adds object as observer when enable
    private void OnEnable()
    {
        player.addObserver(this);
        current = gameObject.GetComponent<Image>();
    }
    
    //removes object as observer when disabled to avoid unecessary signals
    private void OnDisable()
    {
        player.removeObserver(this);
    }
}
