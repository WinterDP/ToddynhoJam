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

    public PlayerAttack _playerAttackReference;

    private bool _isShooting;
    public bool IsShooting
    {
        get => _isShooting;
        set => _isShooting = value;
    }
    #endregion

    #region lanterna
    private bool _hasLantern = false;
    public bool HasLantern
    {
        get => _hasLantern;
        set => _hasLantern = value;
    }
    #endregion

    #region Stamina
    private bool _hasStamina = true;
    public bool HasStamina
    {
        get => _hasStamina;
        set => _hasStamina = value;
    }
    #endregion

    #region Interação
    private bool _isInteracting = false;
    public bool IsInteracting
    {
        get => _isInteracting;
        set => _isInteracting = value;
    }

    private bool _hasCardBlue;
    public bool HasCardBlue
    {
        get => _hasCardBlue;
        set => _hasCardBlue = value;
    }

    private bool _hasCardPurple;
    public bool HasCardPurple
    {
        get => _hasCardPurple;
        set => _hasCardPurple = value;
    }

    private bool _hasCardGray;
    public bool HasCardGray
    {
        get => _hasCardGray;
        set => _hasCardGray = value;
    }
    #endregion

    #region Animação

    public PlayerAnimations PlayerAnimationsReference;
    #endregion

    private void Awake()
    {
        PlayerAnimationsReference = gameObject.GetComponentInChildren<PlayerAnimations>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerAttackReference = gameObject.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        RunningRecoil(); 
        if (_isInteracting && PlayerAnimationsReference.IsNotPlayingAnimation("Interact"))
        {
            PlayerAnimationsReference.PlayInteract();
        }
    }

    public void RunningRecoil()
    {
        if (_isRunning)
        {
            _playerAttackReference.CalculateRecoil();
        }
    }
}
