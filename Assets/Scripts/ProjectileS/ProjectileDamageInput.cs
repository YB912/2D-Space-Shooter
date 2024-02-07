
using sys = System;
using UnityEngine;

public class ProjectileDamageInput : MonoBehaviour
{
    public event sys.Action<float> takeDamage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Collision detected with {collision.gameObject}.");
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            takeDamage?.Invoke(projectile.damage);
        }
        else
        {
            Debug.Log("projectile is null.");
        }
    }
}
