
using UnityEngine;

public class VerticalFollow : MonoBehaviour
{
    [SerializeField]
    private float FollowSpeed;

    private Rigidbody2D _rigidBody2D;
    private GameObject _player;
    private float _currentYMovement;
    private float _currentVelocity;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float targetY = (_player.transform.position - transform.position).normalized.y;
        _currentYMovement = Mathf.SmoothDamp(transform.position.y, targetY, ref _currentVelocity, 0);
    }

    private void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 
                                            _currentYMovement * FollowSpeed * Time.fixedDeltaTime);
    }
}
