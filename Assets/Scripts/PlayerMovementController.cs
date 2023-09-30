
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float VerticalSpeedModifier;
    [SerializeField]
    private float HorizontalSpeedModifier;
    [SerializeField]
    private float SmoothInputSpeed;
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private float ProjectileVelocity;
    [SerializeField]
    private float ProjectileSpread;

    private float _fireTimer;

    public GameObject DebugSquare;

    private Vector2 _newDirection = Vector2.zero;
    private Vector2 _currentDirection = Vector2.zero;
    private Vector2 _smoothVelocity;

    private Rigidbody2D _rigidBody;
    private Animator _playerShipAnimator;
    private Animator _engineFlameAnimator;
    private PlayerInput _playerInput;
    private ProjectileSpawnerController _projectileSpawnerController;

    private InputAction _movementAction;
    private InputAction _shootAction;
    private InputAction _bombAction;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerShipAnimator = GetComponent<Animator>();
        _engineFlameAnimator = GameObject.FindGameObjectWithTag("EngineFlame").GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _projectileSpawnerController = GetComponentInChildren<ProjectileSpawnerController>();

        _movementAction = _playerInput.actions["Move"];
        _shootAction = _playerInput.actions["Shoot"];
        _bombAction = _playerInput.actions["Bomb"];
    }

    private void Update()
    {
        _newDirection = _movementAction.ReadValue<Vector2>().normalized;
        _currentDirection = Vector2.SmoothDamp(_currentDirection, _newDirection, ref _smoothVelocity, SmoothInputSpeed);

        // Handle Y axis and firing animations transitions
        _playerShipAnimator.SetBool("Firing", _shootAction.ReadValue<float>() == 1 ? true : false);
        _playerShipAnimator.SetInteger("VerticalDirection", Math.Sign(_newDirection.y));

        // Handle X axis animation transitions
        _engineFlameAnimator.SetInteger("HorizontalDirection", Math.Sign(_newDirection.x));
    }
    
    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_currentDirection.x * HorizontalSpeedModifier * Time.deltaTime,
                                          _currentDirection.y * VerticalSpeedModifier * Time.deltaTime);
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
                _projectileSpawnerController.Shoot(Projectile ,Vector2.right, ProjectileVelocity, ProjectileSpread);
            }
        }
    }

    private void ResetFireTimer()
    {
        _fireTimer = 1 / FireRate;
    }
}
