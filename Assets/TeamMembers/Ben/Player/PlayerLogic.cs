using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic Instance { get; private set; }

    private Dictionary<string, int> _inventory = new();

    [Header("Water Settings")]
    public float WaterDrag = 8f;
    public float MaxBreath = 3f; // This + 1 is time until player starts taking damage in water
    public float DrownTimer = 0;
    private bool _isDrowning = false;
    public bool IsDrowning
    {
        get { return _isDrowning; }
        set
        {
            if(_isDrowning == value) return; // If the value is the same, don't do anything (this is to prevent the InvokeRepeating from being called multiple times
            if(value)
            {
                _isDrowning = true;
                InvokeRepeating("Drown", 1, 1f);
            }
            else
            {
                _isDrowning = false;
                CancelInvoke("Drown");
            }
        }
    }

    [Header("Attack Settings")]
    //[SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private GameObject _bulletPrefab;
    private ParticleSystem _shootParticle;

    [Header("Movement Settings")]
    public float MoveSpeed = 10f;
    public float JumpForce = 20f;

    public int MaxJumpCount = 1;
    [HideInInspector] public int JumpCount;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private SpriteRenderer _bodySpriteRenderer;
    private ParticleSystem _dieParticle;
    private PlayerInteractions _playerInteractions;
    private HealthUI _health;

    private bool _hasDied = false;
    private PlayerStateMachine Machine => PlayerStateMachine.Instance; // Doing this just so it's less to type.

    [HideInInspector] public bool isFacingRight => _bodySpriteRenderer.flipX;
    [HideInInspector] public Vector3 facingDirection => isFacingRight ? Vector3.right : Vector3.left;
    [HideInInspector] public bool isShootParticlePlaying => _shootParticle.isPlaying;

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

        // This is needed for mobile building. Doing it in this script cause why not.
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _playerInteractions = GetComponent<PlayerInteractions>();
        _bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _health = GameObject.Find("Health").GetComponent<HealthUI>();

        _shootParticle = transform.Find("Particles").Find("ShootParticle").GetComponent<ParticleSystem>();
        _dieParticle = transform.Find("Particles").Find("DieParticle").GetComponent<ParticleSystem>();

        JumpCount = MaxJumpCount;
    }

    #region Inventory
    public void PrintInventory()
    {
        Debug.Log("Player's current inventory:");
        foreach(var keyValuePair in _inventory)
        {
            Debug.Log(keyValuePair.Key + " : " + keyValuePair.Value);
        }
    }

    public bool CheckInventoryForItem(Items itemToGet, out int amount) => CheckInventoryForItem(itemToGet.GetType().Name, out amount);
    public bool CheckInventoryForItem(string itemNameToGet, out int amount)
    {
        if (_inventory.ContainsKey(itemNameToGet))
        {
            amount = _inventory[itemNameToGet];
            return true;
        }
        else
        {
            amount = 0;
            return false;
        }
    }
    public Dictionary<string, int> GetInventory() => _inventory;
    public void AddItemToInventory(Items itemToAdd, int amountToAdd = 1) => AddItemToInventory(itemToAdd.GetType().Name, amountToAdd);
    public void AddItemToInventory(string itemNameToAdd, int amountToAdd = 1)
    {
        if (CheckInventoryForItem(itemNameToAdd, out int amount))
        {
            _inventory[itemNameToAdd] += amountToAdd;
        }
        else
        {
            _inventory.Add(itemNameToAdd, amountToAdd);
        }
    }
    #endregion

    private void Update()
    {
        SetSpriteOrientation();

        TryAttack();

        TryDie();

        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            PrintInventory();
        }
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
            Machine.SwitchState(PlayerStates.Attack);
        }
    }

    private void Drown()
    {
        if (!IsDrowning) return;

        if(DrownTimer >= MaxBreath)
        {
            _playerInteractions.NotifyObserver(PlayerActions.Hurt);
        }
        else
        {
            DrownTimer++;
        }
    }

    public void Shoot() {
        _shootParticle.transform.position = transform.position + (facingDirection * 1.1f) + (Vector3.up * 0.3f);
        _shootParticle.transform.up = facingDirection;
        _shootParticle.Play();
        ObjectPoolManager.SpawnObject(_bulletPrefab, _shootParticle.transform.position, Quaternion.identity, PoolType.PlayerBullet).GetComponent<Rigidbody2D>().velocity = facingDirection * _bulletSpeed;
    }

    private void TryDie()
    {
        // Check if the player dies, doing it here because some states completely override UpdateState
        if (!_hasDied && _health.getHealth() <= 0)
        {
            PlayDeathVisuals();
            PlayerStateMachine.Instance.SwitchState(PlayerStates.Die);
            _hasDied = true;
            return;
        }
    }

    private void PlayDeathVisuals()
    {
        _bodySpriteRenderer.enabled = false;
        _dieParticle.Play();
        Debug.Log("play death stuff");
    }

    /// <summary>
    /// Get's how much breath the player has left.
    /// </summary>
    /// <returns>The remaining breath of the player as a float.</returns>
    public float GetRemainingBreath()
    {
        return MaxBreath - DrownTimer;
    }
}
