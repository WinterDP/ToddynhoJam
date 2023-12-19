using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private StateHandler _stateHandlerReference;

    [SerializeField]
    private float _maxStamina;
    [SerializeField]
    private float _moveCost;
    [SerializeField]
    private float _regenStamina;
    [SerializeField]
    private float _regenDelay;
    [SerializeField]
    private float _minStaminaToMove;
    private float _currentStamina;
    private float _regenTimer;


    private void Awake()
    {

        _stateHandlerReference = gameObject.GetComponent<StateHandler>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _currentStamina = _maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();
    }

    public void UpdateStamina()
    {
        if (_stateHandlerReference.IsRunning)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= _moveCost * Time.deltaTime;
            }
            else
            {
                _currentStamina = 0f;
                _stateHandlerReference.HasStamina = false;
            }
        }
        else
        {
            if (_currentStamina < _maxStamina && _regenTimer >= _regenDelay)
            {
                _currentStamina += _regenStamina * Time.deltaTime;

                if (_maxStamina < _currentStamina)
                    _currentStamina = _maxStamina;


                if (!_stateHandlerReference.HasStamina && _currentStamina >= _minStaminaToMove)
                {
                    _stateHandlerReference.HasStamina = true;

                }
            }
            else
            {
                _regenTimer += Time.deltaTime;
            }
        }

    }
}
