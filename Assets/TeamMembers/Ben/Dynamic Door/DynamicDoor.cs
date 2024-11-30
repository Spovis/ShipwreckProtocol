using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DynamicDoor : MonoBehaviour
{
    private Interactable _interactable;

    private Vector3 finalPos;

    [HideInInspector] public bool IsMoving = false;

    [SerializeField] private bool _interactOnCollision = false;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();

        // Goes up
        finalPos = transform.position + new Vector3(0, 3.6f, 0);
    }

    private void OnEnable()
    {
        _interactable.OnSuccessfulInteract.AddListener(OnSuccessfulOpen);
        _interactable.OnFailedInteract.AddListener(OnFailedOpen);
    }

    private void OnDisable()
    {
        _interactable.OnSuccessfulInteract.RemoveListener(OnSuccessfulOpen);
        _interactable.OnFailedInteract.RemoveListener(OnFailedOpen);
    }

    public void OnSuccessfulOpen()
    {
        IsMoving = true;
        transform.DOMove(finalPos, 1.0f).SetEase(Ease.Linear).onComplete = () => 
        { 
            // This is called when the dotween is done (when it stops moving)
            IsMoving = false; 
        };
    }

    public void OnFailedOpen()
    {
        //Debug.Log("Failed to open door!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _interactOnCollision)
        {
            _interactable.Interact();
        }

    }
}
