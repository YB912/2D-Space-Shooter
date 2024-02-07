
using UnityEngine;

public class AlienFighter1 : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private float ProjectileVelocity;
    [SerializeField]
    private float ProjectileSpread;

    [SerializeField] private float _maximumHealth;

    private ProjectileSpawnerController _projectileSpawnerController;
    private ShootWhenInSight _shootInSight;
    private StraightPlayerDetection _straightDetection;
    private Animator _animator;
    private ProjectileDamageInput _projectileDamageInput;

    private float _currentHealth;

    private float _fireTimer;
    private bool _enteredSight;

    public float maximumHealth { get => _maximumHealth; }

    public float currentHealth { get => _currentHealth; }

    private void Awake()
    {
        _projectileSpawnerController = GetComponentInChildren<ProjectileSpawnerController>();
        _shootInSight = GetComponentInChildren<ShootWhenInSight>();
        _straightDetection = GetComponentInChildren<StraightPlayerDetection>();
        _animator = GetComponent<Animator>();
        _projectileDamageInput = GetComponentInChildren<ProjectileDamageInput>();

        _projectileDamageInput.takeDamage += TakeDamage;

        _shootInSight.ShootWhenInSightEvent += OnShootWhenInSight;
        _straightDetection.PlayerEnteredSight += OnPlayerEnteredSight;
        ResetFireTimer();
    }

    private void Start()
    {
        _currentHealth = _maximumHealth;
    }

    private void FixedUpdate()
    {
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.fixedDeltaTime;
        }
    }

    private void OnShootWhenInSight()
    {
        if (_enteredSight == true)
        {
            _enteredSight = false;
            _projectileSpawnerController.Shoot(Projectile, GameObject.FindWithTag("Enemy1Pool").transform, transform.right, ProjectileVelocity, ProjectileSpread);
            _animator.SetTrigger("Shooting");
            ResetFireTimer();
        }
        if (_fireTimer < 0)
        {
            _projectileSpawnerController.Shoot(Projectile, GameObject.FindWithTag("Enemy1Pool").transform, transform.right, ProjectileVelocity, ProjectileSpread);
            _animator.SetTrigger("Shooting");
            ResetFireTimer();
        }
    }

    private void OnPlayerEnteredSight()
    {
        _enteredSight = true;
    }

    private void ResetFireTimer()
    {
        _fireTimer = 1 / FireRate;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        Debug.Log($"Enemy1 {gameObject} suffered {damage} damage. Remaining health: {_currentHealth}");
    }
}
