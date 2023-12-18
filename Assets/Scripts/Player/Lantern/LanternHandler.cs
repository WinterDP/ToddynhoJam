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

    [SerializeField]
    private List<float> _lowBatteryWarningPoints;
    private bool _warnedLowBatteryLevel;

    private void Awake()
    {
        _lanternReference = gameObject.GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // carrega a bateria na carga maxima quando inciado
        _batteryCurrentCharge = _batteryMaxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLanternTurnedOn)
        {
            // Caso o player ligue a lanterna, ativa a lanterna e começa a gastar bateria
            TurnOnLantern();
            SpendBattery();
        }
        else
        {
            // Caso o player desligue a lanterna, desativa a lanterna
            TurnOffLantern();
        }
    }

    #region Métodos: Bateria
    public void SpendBattery()
    {
        // Caso a bateria esteja com carga
        if (_batteryCurrentCharge > 0)
        {
            // Pisca a lanterna nos pontos de aviso de carga determinados
            foreach (float lowBatteryWarningPoint in _lowBatteryWarningPoints)
            {
                if ((lowBatteryWarningPoint + 1f) > _batteryCurrentCharge && (lowBatteryWarningPoint - 1f) < _batteryCurrentCharge && !_warnedLowBatteryLevel)
                {
                    StartCoroutine(WarningLowBattery());
                }
                else
                {
                    _warnedLowBatteryLevel = false;
                }
            }
            
            // Tira carga da bateria quando o tempo passa de acordo com o decaimento determinado
            _batteryCurrentCharge -= _batteryDecay * Time.deltaTime;
        }
        else
        {
            // Desliga a lanterna
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

    IEnumerator WarningLowBattery()
    {
        // Pisca a lanterna
        for (int i = 0; i < 5; i++)
        {
            // Faz isso
            TurnOnLantern();
            yield return new WaitForSeconds(0.1f);
            // Faz isso depois de alguns segundosa
            TurnOffLantern();
            _warnedLowBatteryLevel = true;
        }
    }
    #endregion

    #region Métodos: Lanterna
    public void TurnOnLantern()
    {
        // Se a lanterna estiver desligada liga a lanterna
        if(_currentLanternRange != _lanternMaxRange)
        {
            _lanternReference.pointLightOuterRadius = _lanternMaxRange;
            _lanternReference.pointLightInnerRadius = _lanternMaxRange / 2f;
            _currentLanternRange = _lanternMaxRange;
        }
    }

    public void TurnOffLantern()
    {
        // Se a lanterna estiver ligada desliga a lanterna
        if (_currentLanternRange != 0.5)
        {
            _lanternReference.pointLightOuterRadius = 0.5f;
            _lanternReference.pointLightInnerRadius = 0.5f / 2f;
            _currentLanternRange = 0.5f;
        }
    }
    #endregion
}
