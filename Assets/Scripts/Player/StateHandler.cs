using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    #region Movimentação   
    private bool _isRunning = false;
    public bool IsRunning {
        get => _isRunning;
        set => _isRunning = value;
    }

    private bool _isCrouching = false;
    public bool IsCrouching
    {
        get => _isCrouching;
        set => _isCrouching = value;
    }

    private bool _isWalkingFoward = false;
    public bool IsWalkingFoward
    {
        get => _isWalkingFoward;
        set => _isWalkingFoward = value;
    }    

    private bool _isWalkingBackward = false;
    public bool IsWalkingBackward 
    {
        get => _isWalkingBackward;
        set => _isWalkingBackward = value;
    }
    #endregion

    #region Ataque Disparo
    private PlayerAttack _playerAttackReference;
    #endregion

    #region Stamina
    private bool _hasStamina = true;
    public bool HasStamina
    {
        get => _hasStamina;
        set => _hasStamina = value;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _playerAttackReference = gameObject.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        RunningRecoil();
    }

    public void RunningRecoil()
    {
        if (_isRunning)
        {
            _playerAttackReference.CalculateRecoil();
        }
    }
}
