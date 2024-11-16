using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour
{
    [Header("Item Requirement")]
    [Tooltip("The name of the item required to interact with this (Leave empty for no required item)")] public string requiredItemName;
    [Tooltip("The amount of the required item needed to successfully interact")][Min(1)] public int requiredItemAmount = 1;

    [Header("Interactable Settings")]
    public float InteractionRange = 0.75f;
    public UnityEvent OnSuccessfulInteract;
    public UnityEvent OnFailedInteract;

    private CircleCollider2D _interactionCollider;

    private bool _isPlayerInInteractionRange = false;
    public bool IsPlayerInInteractionRange => _isPlayerInInteractionRange;

    private void Awake()
    {
        gameObject.tag = "Interactable";

        _interactionCollider = GetComponent<CircleCollider2D>();
        _interactionCollider.isTrigger = true;
        _interactionCollider.radius = InteractionRange;
    }

    /// <summary>
    /// Checks whether this interactable requires an item to be interacted with
    /// </summary>
    /// <returns>Returns null if no item is required, or returns the name of the required item.</returns>
    public string GetRequiredItem() => string.IsNullOrEmpty(requiredItemName) ? null : requiredItemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerInInteractionRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerInInteractionRange = false;
    }   

    private void Update()
    {
        if(_isPlayerInInteractionRange && PlayerInput.Instance.IsInteractPressed)
        {
            //Basically, if we do require an item(is not null or empty), then check if we have it and that we have the right amount of it
            if (string.IsNullOrEmpty(requiredItemName) || (PlayerLogic.Instance.CheckInventoryForItem(requiredItemName, out int amount) && amount >= requiredItemAmount))
            {
                OnSuccessfulInteract.Invoke();
            }
            else
            {
                OnFailedInteract.Invoke();
            }
        }
    }
}
