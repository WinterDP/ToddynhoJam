using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCameraReference;
    private CinemachineBasicMultiChannelPerlin _perlinNoiseReference;

    private void Awake()
    {
        _virtualCameraReference = GetComponent<CinemachineVirtualCamera>();
        _perlinNoiseReference = _virtualCameraReference.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        _perlinNoiseReference.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));
    }

    IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }

    public void ResetIntensity()
    {
        _perlinNoiseReference.m_AmplitudeGain = 0f;
    }
}
