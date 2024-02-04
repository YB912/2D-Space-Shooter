using System;
using UnityEngine;

public class StraightPlayerDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask PlayerMask;

    private BoxCollider2D _sightCollider;
    private Collider2D _playerCollider;

    public event Action PlayerEnteredSight;
    public event Action PlayerExitedSight;

    private void Awake()
    {
        _sightCollider = GetComponent<BoxCollider2D>();
        _playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerEnteredSight?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerExitedSight?.Invoke();
    }

    public bool PlayerInSight()
    {
        return _sightCollider.IsTouching(_playerCollider);
    }
}
