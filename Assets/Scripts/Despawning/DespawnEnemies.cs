
using UnityEngine;

public class DespawnEnemies : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ObjectPoolManager.instance.ReturnToPool(collision.gameObject);
        }
    }
}
