using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class LanternHandler : MonoBehaviour
{
    private Light2D _lanternReference;

    private bool _isLanternTurnedOn = false;
    public bool IsLanternTurnedOn
    {
        get => _isLanternTurnedOn;
        set => _isLanternTurnedOn = value;
    }

    [SerializeField]
    private float _batteryMaxCharge;
    private float _batteryCurrentCharge;
    [SerializeField]
    private float _batteryDecay;

    [SerializeField]
    private float _lanternMaxRange;
    private float _currentLanternRange;

    private void Awake()
    {
        _lanternReference = gameObject.GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _batteryCurrentCharge = _batteryMaxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLanternTurnedOn)
        {
            TurnOnLantern();
            SpendBattery();
        }
        else
        {
            TurnOffLantern();
        }
    }

    public void SpendBattery()
    {
        if (_batteryCurrentCharge > 0)
        {
            _batteryCurrentCharge -= _batteryDecay * Time.deltaTime;
        }
        else
        {
            _batteryCurrentCharge = 0;
            _isLanternTurnedOn = false;
        }
    }

    public bool HasBattery()
    {
        if (_batteryCurrentCharge != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChargeBattery(float charge){
        if (_batteryMaxCharge < (_batteryCurrentCharge + charge))
        {
            _batteryCurrentCharge = _batteryMaxCharge;
        }
        else
        {
            _batteryCurrentCharge = _batteryCurrentCharge + charge;
        }
    }

    public void TurnOnLantern()
    {
        if(_currentLanternRange != _lanternMaxRange)
        {
            _lanternReference.pointLightOuterRadius = _lanternMaxRange;
            _lanternReference.pointLightInnerRadius = _lanternMaxRange / 2f;
            _currentLanternRange = _lanternMaxRange;
        }
    }

    public void TurnOffLantern()
    {
        if (_currentLanternRange != 0.5)
        {
            _lanternReference.pointLightOuterRadius = 0.5f;
            _lanternReference.pointLightInnerRadius = 0.5f / 2f;
            _currentLanternRange = 0.5f;
        }
    }
}
