
using System;
using UnityEngine;

public class ShootWhenInSight : MonoBehaviour
{
    [SerializeField]
    private LayerMask PlayerLayerMask;

    public event Action ShootWhenInSightEvent;

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.forward, Mathf.Infinity, PlayerLayerMask))
        {
            ShootWhenInSightEvent?.Invoke();
        }
    }
}
