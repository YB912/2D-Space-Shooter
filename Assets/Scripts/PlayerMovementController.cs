
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovementController : MonoBehaviour
{
    public float VerticalSpeedModifier;
    public float HorizontalSpeedModifier;

    private bool _isMoving;

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
        _movementAction.started += OnMovement;
        _movementAction.canceled += OnMovement;

        _shootAction.started += OnShoot;
        _shootAction.canceled += OnShoot;

        _bombAction.started += OnBomb;
        _bombAction.canceled += OnBomb;

        _movementAction.Enable();
    }

    private void Update()
    {
        if (_isMoving)
        {
            _direction = _movementAction.ReadValue<Vector2>().normalized;
        }
        _animator.SetInteger("VerticalDirection", Math.Sign(_direction.y));
        _animator.SetInteger("HorizontalDirection", Math.Sign(_direction.x));
    }
    
    private void FixedUpdate()
    {
            _rigidBody.velocity = new Vector2(_direction.x * HorizontalSpeedModifier * Time.deltaTime,
                                              _direction.y * VerticalSpeedModifier * Time.deltaTime);
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isMoving = true;
        }
        else if (context.canceled)
        {
            _isMoving = false;
            _direction = Vector2.zero;
        }
        Debug.Log("Movement input detected");
    }

    private void OnShoot(InputAction.CallbackContext context)
    {

    }

    private void OnBomb(InputAction.CallbackContext context)
    {

    }
}
