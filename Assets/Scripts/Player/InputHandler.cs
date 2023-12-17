using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private PlayerInput _playerInput;

    #region Movimenta��o
    private PlayerMovement _playerMovementReference;
    #endregion

    #region Lanterna
    private LanternHandler _lanternHandlerReference;
    #endregion

    private StateHandler _stateHandler;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerMovementReference = gameObject.GetComponent<PlayerMovement>();

        _lanternHandlerReference = gameObject.GetComponentInChildren<LanternHandler>();

        _stateHandler = gameObject.GetComponent<StateHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Configura��o input
    private void OnEnable()
    {
        _playerInput.Enable();

        // Movimenta��o
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;

        // Corrida
        _playerInput.Player.Run.performed += OnRun;
        _playerInput.Player.Run.canceled += OnRun;

        // Agachar 
        _playerInput.Player.Crouch.performed += OnCrouch;
        _playerInput.Player.Crouch.canceled += OnCrouch;

        // Observar
        _playerInput.Player.Look.performed += OnLook;

        // Agachar 
        _playerInput.Player.TurnOnLantern.performed += OnLanternTurnOn;
        _playerInput.Player.TurnOnLantern.canceled += OnLanternTurnOn;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    #endregion

    #region Movimenta��o
    private void OnMove(InputAction.CallbackContext inputValue)
    {
        // Recebe o input do jogador, tranformando em um vetor para movimenta��o
        _playerMovementReference.InputMovementDirection = inputValue.ReadValue<Vector2>().normalized;
    }

    private void OnRun(InputAction.CallbackContext inputValue)
    {
        if (inputValue.action.IsPressed() && _playerMovementReference.CurrentSpeedCrouchModifier == 1)
        {
            _playerMovementReference.CurrentSpeedRunModifier = _playerMovementReference.PlayerSpeedRunningModifier;
            _stateHandler.IsRunning = true;
        }
        else
        {
            _playerMovementReference.CurrentSpeedRunModifier = 1f;
            _stateHandler.IsRunning = false;
        }
    }

    private void OnCrouch(InputAction.CallbackContext inputValue)
    {
        if (inputValue.action.IsPressed() && _playerMovementReference.CurrentSpeedRunModifier == 1)
        {
            _playerMovementReference.CurrentSpeedCrouchModifier = _playerMovementReference.PlayerSpeedCrouchingModifier;
            _stateHandler.IsCrouching = true;
        }
        else
        {
            _playerMovementReference.CurrentSpeedCrouchModifier = 1;
            _stateHandler.IsCrouching = false;
        }
    }

    private void OnLook(InputAction.CallbackContext inputValue)
    {
        // Pega a posi��o do mouse como input para a rota��o do player
        _playerMovementReference.InputMousePos = inputValue.ReadValue<Vector2>();
    }

    #endregion

    #region Lanterna

    private void OnLanternTurnOn(InputAction.CallbackContext inputValue)
    {
        if (inputValue.action.IsPressed() && _lanternHandlerReference.HasBattery())
        {
            _lanternHandlerReference.IsLanternTurnedOn = !_lanternHandlerReference.IsLanternTurnedOn;
        }
    }
    #endregion
}
