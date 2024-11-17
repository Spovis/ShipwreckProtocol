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
    [Tooltip("Whether this interactable is a one-time use or not")] public bool isOneTimeUse = true;
    [Space(10)]
    [SerializeField] private bool showOutline = true;
    private Material _normalMat;
    [SerializeField] private Material _outlineMat;
    [Space(10)]
    [Tooltip("Text to display above the player if successfully interacted with")] public string onSuccessText = "";
    [Tooltip("Text to display above the player if interaction failed (did not meet requirements)")] public string onFailText = "";
    public UnityEvent OnSuccessfulInteract;
    public UnityEvent OnFailedInteract;

    private int _interactionCount = 0;

    private CircleCollider2D _interactionCollider;
    private SpriteRenderer _spriteRenderer;

    private bool _isPlayerInInteractionRange = false;
    public bool IsPlayerInInteractionRange => _isPlayerInInteractionRange;

    private void Awake()
    {
        gameObject.tag = "Interactable";

        _interactionCollider = GetComponent<CircleCollider2D>();
        _interactionCollider.isTrigger = true;
        _interactionCollider.radius = InteractionRange;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalMat = _spriteRenderer.material;
    }

    /// <summary>
    /// Checks whether this interactable requires an item to be interacted with
    /// </summary>
    /// <returns>Returns null if no item is required, or returns the name of the required item.</returns>
    public string GetRequiredItem() => string.IsNullOrEmpty(requiredItemName) ? null : requiredItemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(showOutline && _outlineMat != null)
        {
            _spriteRenderer.material = _outlineMat;
        }
        _isPlayerInInteractionRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.material = _normalMat;
        _isPlayerInInteractionRange = false;
    }   

    private void Update()
    {
        if(_isPlayerInInteractionRange && PlayerInput.Instance.IsInteractPressed)
        {
            //Basically, if we do require an item(is not null or empty), then check if we have it and that we have the right amount of it
            if (string.IsNullOrEmpty(requiredItemName) || (PlayerLogic.Instance.CheckInventoryForItem(requiredItemName, out int amount) && amount >= requiredItemAmount))
            {
                if(isOneTimeUse && _interactionCount > 0) return;
                PopupText.Show(onSuccessText);
                OnSuccessfulInteract.Invoke();
                _interactionCount++;
            }
            else
            {
                PopupText.Show(onFailText, true);
                OnFailedInteract.Invoke();
            }
        }
    }
}
