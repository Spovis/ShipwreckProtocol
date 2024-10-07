using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerStates {
    Null = -1, // Null is not a real state, but it's used to represent a state that doesn't exist (mainly because PlayerStates is not a nullable type).
    Idle,
    Walk,
    Swim,
    Tread,
    Jump,
    Fall,
    Attack,
}

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine Instance { get; private set; }

    [Header("Debug")]
    [SerializeField] private bool _displayCurrentState;
    private TMP_Text _debugText;

    private PlayerInput _input;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private SpriteRenderer _bodySpriteRenderer;
    private Animator _animator;

    private PS_Base _currentState;
    private Dictionary<PlayerStates, PS_Base> _states = new Dictionary<PlayerStates, PS_Base>();

    LayerMask _groundLayerMask;

    // Getters & Setters
    public PlayerInput Input { get { return _input; } private set { _input = value; } }
    public Rigidbody2D Rigidbody { get { return _rb; } private set { _rb = value; } }
    public CapsuleCollider2D Collider { get { return _collider; } private set { _collider = value; } }
    public SpriteRenderer BodySpriteRenderer { get { return _bodySpriteRenderer; } private set { _bodySpriteRenderer = value; } }
    public Animator Animator { get { return _animator; } private set { _animator = value; } }
    public PS_Base CurrentState { get { return _currentState; } private set { _currentState = value; } }

    public bool IsGrounded => Collider.IsTouchingLayers(_groundLayerMask);

    private void Awake() {
        // Singleton implementation
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        Input = GetComponent<PlayerInput>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();
        Animator = GetComponentInChildren<Animator>();
        BodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _groundLayerMask = LayerMask.GetMask("Ground");

#if UNITY_EDITOR // By using #if UNITY_EDITOR, this code won't be compiled during a build.
        _debugText = GetComponentInChildren<TMP_Text>();
        _debugText.enabled = false;
#endif

        // Initialize the dictionary with each player state
        _states.Add(PlayerStates.Idle, new PS_Idle(this));
        _states.Add(PlayerStates.Walk, new PS_Walk(this));
        _states.Add(PlayerStates.Swim, new PS_Swim(this));
        _states.Add(PlayerStates.Tread, new PS_Tread(this));
        _states.Add(PlayerStates.Jump, new PS_Jump(this));
        _states.Add(PlayerStates.Fall, new PS_Fall(this));
        _states.Add(PlayerStates.Attack, new PS_Attack(this));
    }

    private void Start() {
        // Sets the default / initial state
        SwitchState(PlayerStates.Idle);
    }

    /// <summary>
    /// Function used to get the current state of the player state machine.
    /// </summary>
    /// <returns>Player's current state as PlayerStates enum.</returns>
    public PlayerStates GetCurrentState() {
        // We are doing a sort-of reverse lookup in the dictionary to get the key from the value.
        foreach (var keyValuePair in _states) {
            if (keyValuePair.Value == CurrentState) {
                return keyValuePair.Key;
            }
        }

        return PlayerStates.Null;
    }


    /// <summary>
    /// Function used to compare the current state of the player state machine with the inputted state.
    /// </summary>
    /// <param name="state"></param>
    /// <returns>Whether the player's current state is the inputted state, as a bool.</returns>
    public bool IsCurrentState(PlayerStates state) {
        return GetCurrentState() == state;
    }

    /// <summary>
    /// Switches the player's state to the new state and performs the corresponding logic.
    /// </summary>
    /// <param name="newState"></param>
    public void SwitchState(PlayerStates newState) {
        CurrentState?.ExitState();
        CurrentState = _states[newState];
        CurrentState.EnterState();

        // Displaying the debug text. By using #if UNITY_EDITOR, this code won't be compiled during a build.
#if UNITY_EDITOR
        _debugText.enabled = _displayCurrentState;
        if (_debugText.enabled) {
            _debugText.text = "Current State: \n" + CurrentState.GetType().Name;
        }
#endif
    }

    private void Update() {
        CurrentState.UpdateState();

        if (UnityEngine.Input.GetKeyDown(KeyCode.R)) {
            Debug.Log(GetCurrentState());
        }
    }

    private void FixedUpdate() {
        CurrentState.FixedUpdateState();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        CurrentState.OnCollisionEnter2DState(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        CurrentState.OnCollisionExit2DState(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        CurrentState.OnTriggerEnter2DState(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        CurrentState.OnTriggerExit2DState(collision);
    }
}
