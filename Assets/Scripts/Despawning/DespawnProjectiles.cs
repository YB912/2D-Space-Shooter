
using UnityEngine;

public class DespawnProjectiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (gameObject.tag)
        {
            case "Wall":
                if (collision.gameObject.tag == "PlayerProjectile" || collision.tag == "EnemyProjectile")
                {
                    Despawn(collision);
                }
                break;
            case "PlayerCollider":
                if (collision.gameObject.tag == "EnemyProjectile")
                {
                    Despawn(collision);
                }
                break;
            case "EnemyCollider":
                if (collision.gameObject.tag == "PlayerProjectile")
                {
                    Despawn(collision);
                }
                break;
        }
    }

    private void Despawn(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ObjectPoolManager.instance.ReturnToPool(collision.gameObject);
        }
    }
}
