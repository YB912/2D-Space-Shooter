
using UnityEngine;

public class UniformHorizontalMovement : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(transform.right.x * HorizontalSpeed * Time.fixedDeltaTime,
                                          _rigidBody.velocity.y);
    }
}


