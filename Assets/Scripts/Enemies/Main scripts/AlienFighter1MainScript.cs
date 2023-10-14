
using UnityEngine;

public class AlienFighter1MainScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private float ProjectileVelocity;
    [SerializeField]
    private float ProjectileSpread;

    private ProjectileSpawnerController _projectileSpawnerController;
    private ShootWhenInSight _shootInSight;
    private StraightPlayerDetection _straightDetection;
    private Animator _animator;

    private float _fireTimer;
    private bool _enteredSight;

    private void Awake()
    {
        _projectileSpawnerController = GetComponentInChildren<ProjectileSpawnerController>();
        _shootInSight = GetComponent<ShootWhenInSight>();
        _straightDetection = GetComponent<StraightPlayerDetection>();
        _animator = GetComponent<Animator>();

        _shootInSight.ShootWhenInSightEvent += OnShootWhenInSight;
        _straightDetection.PlayerEnteredSight += OnPlayerEnteredSight;
        ResetFireTimer();
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
            _projectileSpawnerController.Shoot(Projectile, transform.right, ProjectileVelocity, ProjectileSpread);
            _animator.SetTrigger("Shooting");
            ResetFireTimer();
        }
        if (_fireTimer < 0)
        {
            _projectileSpawnerController.Shoot(Projectile, transform.right, ProjectileVelocity, ProjectileSpread);
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
}
