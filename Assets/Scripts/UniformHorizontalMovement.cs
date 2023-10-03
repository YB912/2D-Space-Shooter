
using UnityEngine;

public class UniformHorizontalMovement : MonoBehaviour
{
    [SerializeField]
    private bool RightToLeft = true;
    [SerializeField]
    private float HorizontalSpeed;

    private Rigidbody2D _rigidBody;
    private float _horizontalDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _horizontalDirection = RightToLeft ? -1 : 1;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_horizontalDirection * HorizontalSpeed * Time.fixedDeltaTime,
                                          _rigidBody.velocity.y);
    }
}


