
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private float ProjectileVelocity;
    [SerializeField]
    private float ProjectileSpread;

    private float _fireTimer;

    private Animator _playerShipAnimator;
    private PlayerInput _playerInput;

    private ProjectileSpawnerController _projectileSpawnerController;

    private InputAction _shootAction;

    private void Awake()
    {
        _playerShipAnimator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _projectileSpawnerController = GetComponentInChildren<ProjectileSpawnerController>();

        _shootAction = _playerInput.actions["Shoot"];
    }

    private void Update()
    {
        _playerShipAnimator.SetBool("Firing", _shootAction.ReadValue<float>() == 1 ? true : false);
    }

    private void FixedUpdate()
    {
        // Handle shooting
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.fixedDeltaTime;
        }
        if (_shootAction.ReadValue<float>() == 1)
        {
            if (_fireTimer <= 0)
            {
                ResetFireTimer();
                _projectileSpawnerController.Shoot(Projectile, Vector2.right, ProjectileVelocity, ProjectileSpread);
            }
        }
    }

    private void ResetFireTimer()
    {
        _fireTimer = 1 / FireRate;
    }
}
