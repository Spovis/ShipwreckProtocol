using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic Instance { get; private set; }

    [Header("Water Settings")]
    public float WaterDrag = 8f;
    [SerializeField] private float _maxBreath = 4f;
    private float _drownTimer = 0;
    private bool _isDrowning = false;
    public bool IsDrowning
    {
        get { return _isDrowning; }
        set
        {
            if(value)
            {
                _isDrowning = true;
                InvokeRepeating("Drown", 1, 1.25f);
            }
            else
            {
                _isDrowning = false;
                _drownTimer = 0;
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

        _shootParticle = transform.Find("Particles").Find("ShootParticle").GetComponent<ParticleSystem>();
        _dieParticle = transform.Find("Particles").Find("DieParticle").GetComponent<ParticleSystem>();

        JumpCount = MaxJumpCount;
    }

    private void Update()
    {
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

    private void Drown()
    {
        if (!IsDrowning) return;

        if(_drownTimer >= _maxBreath)
        {
            _playerInteractions.NotifyObserver(PlayerActions.Hurt);
        }
        else
        {
            _drownTimer++;
        }
    }

    public void Shoot() {
        _shootParticle.transform.position = transform.position + (facingDirection * 1.1f) + (Vector3.up * 0.3f);
        _shootParticle.transform.up = facingDirection;
        _shootParticle.Play();
        ObjectPoolManager.SpawnObject(_bulletPrefab, _shootParticle.transform.position, Quaternion.identity, PoolType.PlayerBullet).GetComponent<Rigidbody2D>().velocity = facingDirection * _bulletSpeed;
    }

    public void PlayDeathVisuals()
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
        return _maxBreath - _drownTimer;
    }
}
