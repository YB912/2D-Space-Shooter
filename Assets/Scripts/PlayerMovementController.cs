
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovementController : MonoBehaviour
{
    public float VerticalSpeedModifier;
    public float HorizontalSpeedModifier;

    private Vector2 _direction = Vector2.zero;


    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private PlayerInput _playerInput;

    private InputAction _movementAction;
    private InputAction _shootAction;
    private InputAction _bombAction;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();

        _movementAction = _playerInput.actions["Move"];
        _shootAction = _playerInput.actions["Shoot"];
        _bombAction = _playerInput.actions["Bomb"];
    }

    private void Start()
    {
        _shootAction.started += OnShoot;
        _shootAction.canceled += OnShoot;

        _bombAction.started += OnBomb;
        _bombAction.canceled += OnBomb;
    }

    private void Update()
    {
        _direction = _movementAction.ReadValue<Vector2>().normalized;
    }
    
    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_direction.x * HorizontalSpeedModifier * Time.deltaTime,
                                          _direction.y * VerticalSpeedModifier * Time.deltaTime);

        // Handle Y axis animation transitions
        _animator.SetInteger("VerticalDirection", Math.Sign(_rigidBody.velocity.y));

        // Handle X axis animation transitions
        _animator.SetInteger("HorizontalDirection", Math.Sign(_rigidBody.velocity.x));
    }

    private void OnShoot(InputAction.CallbackContext context)
    {

    }

    private void OnBomb(InputAction.CallbackContext context)
    {

    }
}
