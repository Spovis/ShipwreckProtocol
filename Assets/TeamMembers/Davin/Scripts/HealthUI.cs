using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IObserver
{
    //will observe player object put here
    [SerializeField] Subject player;
    Image healthUI;


    //when receiving a notification from subject
    public void OnNotify(PlayerActions action)
    {
        //switch case to perform different actions based off name
        switch(action)
        {
            case (PlayerActions.Hurt):
                healthUI.fillAmount = healthUI.fillAmount - .2f;
                Debug.Log("Hurt received");
                return;
            case (PlayerActions.Heal):
                healthUI.fillAmount = healthUI.fillAmount + .2f;
                Debug.Log("Heal received");
                return;
        }
    }

    //adds object as observer when enable
    private void OnEnable()
    {
        player.addObserver(this);
        healthUI = GetComponentInChildren<Image>();
    }
    
    //removes object as observer when disabled to avoid unecessary signals
    private void OnDisable()
    {
        player.removeObserver(this);
    }
}
