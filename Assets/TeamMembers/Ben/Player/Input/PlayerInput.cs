using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    private PlayerInputAction _playerInput;

    private Vector2 _currentMovementInput;

    private bool _isMovementPressed;
    private bool _isMovementHeld;

    private bool _isDashPressed;
    private bool _isDashHeld;

    private bool _isJumpPressed;
    private bool _isJumpHeld;

    private bool _isInteractPressed;
    private bool _isInteractHeld;

    private bool _isAttackPressed;
    private bool _isAttackHeld;

    private bool _isPausePressed;
    private bool _isPauseHeld;

    /// <summary>
    /// The current movement input vector.
    /// </summary>
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }

    /// <summary>
    /// Indicates whether the movement input is currently pressed.
    /// </summary>
    public bool IsMovementPressed { get { return _isMovementPressed; } }

    /// <summary>
    /// Indicates whether the movement input is currently held.
    /// </summary>
    public bool IsMovementHeld { get { return _isMovementHeld; } }

    /// <summary>
    /// Indicates whether the dash input is currently pressed.
    /// </summary>
    public bool IsDashPressed { get { return _isDashPressed; } }

    /// <summary>
    /// Indicates whether the dash input is currently held.
    /// </summary>
    public bool IsDashHeld { get { return _isDashHeld; } }

    /// <summary>
    /// Indicates whether the jump input is currently pressed.
    /// </summary>
    public bool IsJumpPressed { get { return _isJumpPressed; } }

    /// <summary>
    /// Indicates whether the jump input is currently held.
    /// </summary>
    public bool IsJumpHeld { get { return _isJumpHeld; } }

    /// <summary>
    /// Indicates whether the interact input is currently pressed.
    /// </summary>
    public bool IsInteractPressed { get { return _isInteractPressed; } }

    /// <summary>
    /// Indicates whether the interact input is currently held.
    /// </summary>
    public bool IsInteractHeld { get { return _isInteractHeld; } }

    /// <summary>
    /// Indicates whether the attack input is currently pressed.
    /// </summary>
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    /// <summary>
    /// Indicates whether the attack input is currently held.
    /// </summary>
    public bool IsAttackHeld { get { return _isAttackHeld; } }

    /// <summary>
    /// Indicates whether the pause input is currently pressed.
    /// </summary>
    public bool IsPausePressed { get { return _isPausePressed; } }

    /// <summary>
    /// Indicates whether the pause input is currently held.
    /// </summary>
    public bool IsPauseHeld { get { return _isPauseHeld; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Singleton implementation
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        // Input Action Events Subscribing
        _playerInput = new PlayerInputAction(); // This is the auto-generated class from the Input Actions asset.

        _playerInput.Player.Move.started += OnMovementInput;
        _playerInput.Player.Move.canceled += OnMovementInput;
        _playerInput.Player.Move.performed += OnMovementInput;

        _playerInput.Player.Dash.started += OnDashInput;
        _playerInput.Player.Dash.canceled += OnDashInput;

        _playerInput.Player.Jump.started += OnJumpInput;
        _playerInput.Player.Jump.canceled += OnJumpInput;

        _playerInput.Player.Interact.started += OnInteractInput;
        _playerInput.Player.Interact.canceled += OnInteractInput;

        _playerInput.Player.Attack.started += OnAttackInput;
        _playerInput.Player.Attack.canceled += OnAttackInput;

        _playerInput.Player.Pause.started += OnPauseInput;
        _playerInput.Player.Pause.canceled += OnPauseInput;
    }

    private void LateUpdate() {
        // Here we set all of the "Pressed" bools to false, in LateUpdate, so it true for just one Update call.
        // A reminder: LateUpdate() is called once per frame after all Update() calls are done.
        _isMovementPressed = false;
        _isDashPressed = false;
        _isJumpPressed = false;
        _isInteractPressed = false;
        _isAttackPressed = false;
    }

    private void OnEnable() {
        _playerInput.Player.Enable();
    }

    private void OnDisable() {
        _playerInput.Player.Disable();
    }

    void OnMovementInput(InputAction.CallbackContext context) {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0;
        _isMovementHeld = _isMovementPressed;
    }

    void OnJumpInput(InputAction.CallbackContext context) {
        _isJumpPressed = context.ReadValueAsButton();
        _isJumpHeld = _isJumpPressed;
    }

    void OnInteractInput(InputAction.CallbackContext context) {
        _isInteractPressed = context.ReadValueAsButton();
        _isInteractHeld = _isInteractPressed;
    }

    void OnDashInput(InputAction.CallbackContext context) {
        _isDashPressed = context.ReadValueAsButton();
        _isDashHeld = _isDashPressed;
    }

    void OnAttackInput(InputAction.CallbackContext context) {
        _isAttackPressed = context.ReadValueAsButton();
        _isAttackHeld = _isAttackPressed;
    }

    void OnPauseInput(InputAction.CallbackContext context) {
        _isPausePressed = context.ReadValueAsButton();
        _isPauseHeld = _isAttackPressed;
    }
}