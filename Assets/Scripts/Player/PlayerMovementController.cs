
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

    public GameObject DebugSquare;

    private Vector2 _newDirection = Vector2.zero;
    private Vector2 _currentDirection = Vector2.zero;
    private Vector2 _smoothVelocity;

    private Rigidbody2D _rigidBody;
    private Animator _playerShipAnimator;
    private Animator _engineFlameAnimator;
    private PlayerInput _playerInput;

    private InputAction _movementAction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerShipAnimator = GetComponent<Animator>();
        _engineFlameAnimator = GameObject.FindGameObjectWithTag("EngineFlame").GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();

        _movementAction = _playerInput.actions["Move"];
    }

    private void Update()
    {
        _newDirection = _movementAction.ReadValue<Vector2>().normalized;
        _currentDirection = Vector2.SmoothDamp(_currentDirection, _newDirection, ref _smoothVelocity, SmoothInputSpeed);

        // Handle Y axis animations transitions
        _playerShipAnimator.SetInteger("VerticalDirection", Math.Sign(_newDirection.y));

        // Handle X axis animation transitions
        _engineFlameAnimator.SetInteger("HorizontalDirection", Math.Sign(_newDirection.x));
    }
    
    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_currentDirection.x * HorizontalSpeedModifier * Time.deltaTime,
                                          _currentDirection.y * VerticalSpeedModifier * Time.deltaTime);
    }
}
