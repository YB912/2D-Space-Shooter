
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _minimumDamage;
    [SerializeField] private float _maximumDamage;

    public float damage
    {
        get
        {
            return Random.Range(_minimumDamage, _maximumDamage);
        }
    }
}
