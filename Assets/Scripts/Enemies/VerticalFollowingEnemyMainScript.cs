
using UnityEngine;

public class VerticalFollowingEnemyMainScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private float ProjectileVelocity;
    [SerializeField]
    private float ProjectileSpread;
    [SerializeField]
    private LayerMask PlayerLayerMask;

    private ProjectileSpawnerController _projectileSpawnerController;
    private ShootWhenInSight _shootInSight;

    private float _fireTimer;
    private bool _shootingInterrupted;

    private void Awake()
    {
        _projectileSpawnerController = GetComponentInChildren<ProjectileSpawnerController>();
        _shootInSight = GetComponent<ShootWhenInSight>();

        _shootInSight.ShootWhenInSightEvent += OnShootWhenInSight;
        ResetFireTimer();

        transform.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.forward, Mathf.Infinity, PlayerLayerMask) == false)
        {
            _shootingInterrupted = false;
        }
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
        if (_shootingInterrupted == false)
        {
            _shootingInterrupted = true;
            ResetFireTimer();
        }
        if (_fireTimer < 0)
        {
            _projectileSpawnerController.Shoot(Projectile, transform.right, ProjectileVelocity, ProjectileSpread);
            ResetFireTimer();
        }
    }

    private void ResetFireTimer()
    {
        _fireTimer = 1 / FireRate;
    }
}
