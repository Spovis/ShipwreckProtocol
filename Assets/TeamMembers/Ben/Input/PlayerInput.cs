using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    private PlayerInputAction _playerInput;

    private bool _canInput = true;
    private bool _forceResetPresses = false;
    private bool _canAttack = true;
    
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

    private bool _isAnyButtonPressed;

    /// <summary>
    /// Indicates whether the player is able to input or not.
    /// </summary>
    public bool CanInput { get { return _canInput; } set { _canInput = value; } }

    /// <summary>
    /// Decides whether to force reset all input presses or not on late update or not. Default is false.
    /// </summary>
    public bool ForceResetPresses { get { return _forceResetPresses; } set { _forceResetPresses = value; } }

    /// <summary>
    /// Indicates whether the player is able to attack or not.
    /// </summary>
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }

    /// <summary>
    /// The current movement input vector.
    /// </summary>
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }

    /// <summary>
    /// Indicates whether the movement input was pressed this frame.
    /// </summary>
    public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }

    /// <summary>
    /// Indicates whether the movement input is currently held.
    /// </summary>
    public bool IsMovementHeld { get { return _isMovementHeld; } set { _isMovementHeld = value; } }

    /// <summary>
    /// Indicates whether the dash input was pressed this frame.
    /// </summary>
    public bool IsDashPressed { get { return _isDashPressed; } set { _isDashPressed = value; } }

    /// <summary>
    /// Indicates whether the dash input is currently held.
    /// </summary>
    public bool IsDashHeld { get { return _isDashHeld; } set { _isDashHeld = value; } }

    /// <summary>
    /// Indicates whether the jump input was pressed this frame.
    /// </summary>
    public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }

    /// <summary>
    /// Indicates whether the jump input is currently held.
    /// </summary>
    public bool IsJumpHeld { get { return _isJumpHeld; } set { _isJumpHeld = value; } }

    /// <summary>
    /// Indicates whether the interact input was pressed this frame.
    /// </summary>
    public bool IsInteractPressed { get { return _isInteractPressed; } set { _isInteractPressed = value; } }

    /// <summary>
    /// Indicates whether the interact input is currently held.
    /// </summary>
    public bool IsInteractHeld { get { return _isInteractHeld; } set { _isInteractHeld = value; } }

    /// <summary>
    /// Indicates whether the attack input was pressed this frame.
    /// </summary>
    public bool IsAttackPressed { get { return _isAttackPressed; } set { _isAttackPressed = value; } }
    /// <summary>
    /// Indicates whether the attack input is currently held.
    /// </summary>
    public bool IsAttackHeld { get { return _isAttackHeld; } set { _isAttackHeld = value; } }

    /// <summary>
    /// Indicates whether the pause input was pressed this frame.
    /// </summary>
    public bool IsPausePressed { get { return _isPausePressed; } set { _isPausePressed = value; } }

    /// <summary>
    /// Indicates whether the pause input is currently held.
    /// </summary>
    public bool IsPauseHeld { get { return _isPauseHeld; } set { _isPauseHeld = value; } }

    /// <summary>
    /// Indicates whether any button is pressed this frame.
    /// </summary>
    public bool IsAnyButtonPressed { get { return _isAnyButtonPressed; } set { _isAnyButtonPressed = value; } }

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
        _playerInput.Player.Attack.performed += OnAttackInput;
        _playerInput.Player.Attack.canceled += OnAttackInput;

        _playerInput.Player.Pause.started += OnPauseInput;
        _playerInput.Player.Pause.canceled += OnPauseInput;

        InputSystem.onAnyButtonPress.Call((currentAction) => {
            IsAnyButtonPressed = true;
            //if (currentAction is ButtonControl button && IsAnyButtonPressed)
            //{
            //    //ONLY USE THIS DEBUG FOR TROUBLESHOOTING, CAUSES BIG LAG.
            //    Debug.Log($"Key {currentAction.name} pressed! (text: {currentAction.displayName})");
            //}
        });
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        // Here we set all of the "Pressed" bools to false, in LateUpdate, so it true for just one Update call.
        // A reminder: LateUpdate() is called once per frame after all Update() calls are done.
        if (!CanInput && !ForceResetPresses) return;

        IsMovementPressed = false;
        IsDashPressed = false;
        IsJumpPressed = false;
        IsInteractPressed = false;
        IsAttackPressed = false;
        IsPausePressed = false;
        IsAnyButtonPressed = false;
    }

    private void OnEnable() {
        _playerInput?.Player.Enable();
    }

    private void OnDisable() {
        _playerInput?.Player.Disable();
    }

    void OnMovementInput(InputAction.CallbackContext context) {
        if(!CanInput) return;
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0;
        _isMovementHeld = _isMovementPressed;
    }

    void OnJumpInput(InputAction.CallbackContext context) {
        if(!CanInput) return;
        _isJumpPressed = context.ReadValueAsButton();
        _isJumpHeld = _isJumpPressed;
    }

    void OnInteractInput(InputAction.CallbackContext context) {
        if(!CanInput) return;
        _isInteractPressed = context.ReadValueAsButton();
        _isInteractHeld = _isInteractPressed;
    }

    void OnDashInput(InputAction.CallbackContext context) {
        if(!CanInput) return;
        _isDashPressed = context.ReadValueAsButton();
        _isDashHeld = _isDashPressed;
    }

    void OnAttackInput(InputAction.CallbackContext context) {
        if(!CanInput) return;
        _isAttackPressed = context.ReadValueAsButton();
        _isAttackHeld = _isAttackPressed;
    }

    void OnPauseInput(InputAction.CallbackContext context) {
        _isPausePressed = context.ReadValueAsButton();
        _isPauseHeld = _isAttackPressed;
    }
}