using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour, IObserver
{
    //will observe player object put here
    [SerializeField] Subject player;
    Image heatImage;

    void Update()
    {
        if (heatImage.fillAmount == 1)
        {
            PlayerInput.Instance.CanAttack = false;
        }
        else if (heatImage.fillAmount == 0)
        {
            PlayerInput.Instance.CanAttack = true;
        }
        heatImage.fillAmount = heatImage.fillAmount - Time.deltaTime * .3f;

    }

    //when receiving a notification from subject
    public void OnNotify(PlayerActions action)
    {
        //switch case to perform different actions based off name
        switch (action)
        {
            case (PlayerActions.Fire):
                heatImage.fillAmount = heatImage.fillAmount + .2f;
                Debug.Log("Fire Received");
                return;
        }
    }

    //adds object as observer when enable
    private void OnEnable()
    {
        player.addObserver(this);
        heatImage = transform.Find("FullGun").GetComponent<Image>();
    }

    //removes object as observer when disabled to avoid unecessary signals
    private void OnDisable()
    {
        player.removeObserver(this);
    }
}
