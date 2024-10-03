using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic Instance { get; private set; }

    [Header("Movement Settings")]
    public float MoveSpeed = 10f;
    public float JumpForce = 20f;

    public int MaxJumpCount = 1;
    [HideInInspector] public int JumpCount;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private SpriteRenderer _bodySpriteRenderer;
    private PlayerStateMachine Machine => PlayerStateMachine.Instance; // Doing this just so it's less to type.

    // Start is called before the first frame update
    void Awake()
    {
        // Singleton Implementation
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        JumpCount = MaxJumpCount;
    }

    private void Update() {
        SetSpriteOrientation();

        TryAttack();
    }

    /// <summary>
    /// Sets the player's sprite to be flipped or not based on the current input.
    /// </summary>
    private void SetSpriteOrientation() {
        if (PlayerInput.Instance.CurrentMovementInput.x > 0) _bodySpriteRenderer.flipX = true;
        else if (PlayerInput.Instance.CurrentMovementInput.x < 0) _bodySpriteRenderer.flipX = false;
    }

    /// <summary>
    /// Moves the player based on the current input, the player's move speed, and an optional multiplier.
    /// </summary>
    /// <param name="multiplier"></param>
    public void MovePlayer(float multiplier = 1f) {
        _rigidbody.velocity = new Vector2(PlayerInput.Instance.CurrentMovementInput.x * MoveSpeed * multiplier, _rigidbody.velocity.y);
    }

    /// <summary>
    /// Jumps the player based on the player's jump force and an optional multiplier.
    /// </summary>
    /// <param name="multiplier"></param>
    public void JumpPlayer(float multiplier = 1f) {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce * multiplier);
    }

    public void TryAttack() {
        if (PlayerInput.Instance.IsAttackPressed) {
            // If the player is already attacking and tries to attack again, skip the initiation.

                Machine.SwitchState(PlayerStates.Attack);
        }
    }
}
