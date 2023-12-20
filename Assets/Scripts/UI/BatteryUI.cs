using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{

    private Slider batterySlider;
    public static Action<float, float> OnBatterySpent;

    private void Awake()
    {
        batterySlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        OnBatterySpent += BatterySpent;
    }

    private void OnDisable()
    {
        OnBatterySpent -= BatterySpent;
    }

    private void BatterySpent(float value, float maxValue)
    {
        batterySlider.value = value/maxValue;
    }
}
