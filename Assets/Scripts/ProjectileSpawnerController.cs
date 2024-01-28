
using UnityEngine;

public class ProjectileSpawnerController : MonoBehaviour
{
    public void Shoot(GameObject projectile, Transform parent, Vector2 direction, float velocity, float spread)
    {
        GameObject current = ObjectPoolManager.instance.TakeFromPool(projectile, transform.position, Quaternion.identity, parent);
        current.GetComponent<Rigidbody2D>().velocity = Time.fixedDeltaTime * velocity * direction.normalized;
    }
}
