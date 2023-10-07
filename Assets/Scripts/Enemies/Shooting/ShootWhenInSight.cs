
using System;
using UnityEngine;

public class ShootWhenInSight : MonoBehaviour
{
    private StraightPlayerDetection _straightDetection;

    public event Action ShootWhenInSightEvent;

    private void Awake()
    {
        _straightDetection = GetComponent<StraightPlayerDetection>();
    }

    private void Update()
    {
        if (_straightDetection.PlayerInSight())
        {
            ShootWhenInSightEvent?.Invoke();
        }
    }
}
