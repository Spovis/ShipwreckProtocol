using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[SelectionBase]
public class Player : Subject
{
    public static Player Instance { get; private set; }

    public float MoveSpeed = 5f;
    public float JumpForce = 10f;

    [HideInInspector] public int JumpCount = 1;
    public int MaxJumpCount = 1;

    private SpriteRenderer _bodySpriteRenderer;
    private int playerHealth = 2;

    private void Awake() {
        // Singleton implementation
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        // Initialization
        _bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Setting initial jump count to be max jump count, meaning the player cannot jump immediately.
        JumpCount = MaxJumpCount;
    }

    private void Update() {
        // Flipping the sprite based on movement direction (formatted with an if/else if because we are specifically ignoring zero).
        if (PlayerInput.Instance.CurrentMovementInput.x > 0) _bodySpriteRenderer.flipX = true;
        else if (PlayerInput.Instance.CurrentMovementInput.x < 0) _bodySpriteRenderer.flipX = false;

        //temporary for testing healing and hurting
        if(Input.GetKeyDown(KeyCode.K))
        {
            playerHealth--;
            NotifyObserver(PlayerActions.Hurt);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            playerHealth++;
            NotifyObserver(PlayerActions.Heal);
        }
    }
}
