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
    Animator animator;
    void Update()
    {
        if (heatImage.fillAmount == 1)
        {
            PlayerInput.Instance.CanAttack = false;
            animator.SetBool("Overheated", true);
        }
        else if (heatImage.fillAmount == 0)
        {
            PlayerInput.Instance.CanAttack = true;
            animator.SetBool("Overheated", false);
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
        animator = transform.Find("GunSpace").GetComponent<Animator>();
        heatImage = transform.Find("GunSpace").Find("FullGun").GetComponent<Image>();
    }

    //removes object as observer when disabled to avoid unecessary signals
    private void OnDisable()
    {
        player.removeObserver(this);
    }
}
