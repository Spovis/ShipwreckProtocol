using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    [SerializeField] Transform player; // Assign the player’s Transform in the Inspector
    [SerializeField] RectTransform uiElement; // Assign the UI element's RectTransform in the Inspector
    [SerializeField] Camera mainCamera; // Assign the main camera in the Inspector
    [SerializeField] Slider slider;
    [SerializeField] RectTransform fill;

    private void Start()
    {

        slider.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (player != null && mainCamera != null && uiElement)
        {

            // Set the UI element's position to the screen position
            slider.gameObject.transform.position = new Vector3(mainCamera.WorldToScreenPoint(player.transform.position).x, mainCamera.WorldToScreenPoint(player.transform.position).y + 120, mainCamera.WorldToScreenPoint(mainCamera.transform.position).z);
            slider.value = PlayerLogic.Instance.GetRemainingBreath() / PlayerLogic.Instance.MaxBreath;
            if (slider.value == 0)
            {
                fill.gameObject.SetActive(false);
            }
        }
        if(PlayerStateMachine.Instance.IsCurrentState(PlayerStates.Swim))
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            fill.gameObject.SetActive(true);
            slider.gameObject.SetActive(false);
        }
    }
}
