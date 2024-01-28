
using UnityEngine;

public class DespawnProjectiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            ObjectPoolManager.instance.ReturnToPool(collision.gameObject);
        }
    }
}
