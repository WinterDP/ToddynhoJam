using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    private static float _noiseDistance = 0f;
    private static PlayerNoise _instance;
    public static float NoiseDistance => _noiseDistance;
    public static PlayerNoise Instance => _instance;

    public static Action<float> OnNoiseChange;

    public float NormalMovementNoise = 1f;
    public float BackwardsMovementNoise = 0.5f;
    public float RunningNoise = 2f;
    public float RunningBackwardsNoise = 1f;
    public float ShootingNoise = 5f;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(_instance);
    }

    private void OnEnable()
    {
        OnNoiseChange += NoiseChange;
    }

    private void OnDisable()
    {
        OnNoiseChange -= NoiseChange;
    }

    private void NoiseChange(float value)
    {
        _noiseDistance = value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, NormalMovementNoise);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, BackwardsMovementNoise);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RunningNoise);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, RunningBackwardsNoise);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ShootingNoise);
    }

}
