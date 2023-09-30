
using UnityEngine;

public class ProjectileSpawnerController : MonoBehaviour
{
    public void Shoot(GameObject projectile,Vector2 direction, float velocity, float spread)
    {
        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
        current.GetComponent<Rigidbody2D>().velocity = Time.deltaTime * velocity * direction.normalized;
    }
}
