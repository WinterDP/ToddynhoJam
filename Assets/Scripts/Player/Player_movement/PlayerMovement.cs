using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    #region Vari�veis: Componentes do game Object

    private Rigidbody2D _rigidbody2D;
    private PlayerInput _playerInput;

    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    #endregion

    #region Vari�veis: Dire��o do input para movimento

    private Vector2 _inputMovementDirection;
    private Vector2 _inputMousePos;
    public Vector2 InputMousePos => _inputMousePos;

    #endregion

    #region Vari�veis: m�todos Smooth Vector 2

    // Cuida da velocidade da mudan�a
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    [Header("Atributos de suaviza��o da movimenta��o")]
    [SerializeField]
    private float _smoothTime;

    #endregion

    #region Vari�veis: Atributos de movimenta��o

    [Header("Atributos de movimenta��o")]
    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    [Range(1, 2)]
    private float _playerSpeedRunningModifier;
    [SerializeField]
    [Range(0, 1)]
    private float _playerSpeedCrouchingModifier;
    [SerializeField]
    [Range(0, 1)]
    private float _playerSpeedBackwardsModifier;
    [SerializeField]
    private float _playerAngleFowardMovimentation;

    private float _currentSpeedCrouchModifier;
    private float _currentSpeedRunModifier;
    #endregion

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _mainCamera = Camera.main;

        _currentSpeedCrouchModifier = 1;
        _currentSpeedRunModifier = 1;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleMovement()
    {
        float _playerCurrentSpeed = _playerSpeed * _currentSpeedCrouchModifier * _currentSpeedRunModifier;

        // Faz uma transi��o suave em um dado tempo para a varia��o do valor do input
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _inputMovementDirection,
            ref _movementInputSmoothVelocity,
            _smoothTime
            );

        if (Vector2.Angle(_mainCamera.ScreenToWorldPoint(_inputMousePos) - transform.position, _inputMovementDirection) < _playerAngleFowardMovimentation)
        {
            // Est� se movendo para frente
            // Adiciona velocidade para o corpo, de acordo com a dire��o passada pelo input
            _rigidbody2D.velocity = _smoothedMovementInput * _playerCurrentSpeed;
        }
        else
        {
            // Est� se movendo para tr�s
            // Adiciona velocidade para o corpo, reduzida por se mover para tr�s, de acordo com a dire��o passada pelo input
            _rigidbody2D.velocity = _smoothedMovementInput * _playerCurrentSpeed * _playerSpeedBackwardsModifier;
        }
        
    }

    private void HandleRotation()
    {
        // Calcula a dire��o que o player deve olhar
        Vector2 facingDirection = _mainCamera.ScreenToWorldPoint(_inputMousePos) - transform.position;
        float facingAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        _rigidbody2D.MoveRotation(facingAngle);

    }

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
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnMove(InputAction.CallbackContext inputValue)
    {
        // Recebe o input do jogador, tranformando em um vetor para movimenta��o
        _inputMovementDirection = inputValue.ReadValue<Vector2>().normalized;
    }

    private void OnRun(InputAction.CallbackContext inputValue)
    {
        if (inputValue.action.IsPressed() && _currentSpeedCrouchModifier == 1)
        {
            _currentSpeedRunModifier = _playerSpeedRunningModifier;
        }
        else
        {
            _currentSpeedRunModifier = 1f;
        }
    }

    private void OnCrouch(InputAction.CallbackContext inputValue)
    {
        if (inputValue.action.IsPressed() && _currentSpeedRunModifier == 1)
        {
            _currentSpeedCrouchModifier = _playerSpeedCrouchingModifier;
        }
        else
        {
            _currentSpeedCrouchModifier = 1;
        }
    }

    private void OnLook(InputAction.CallbackContext inputValue)
    {
        // Pega a posi��o do mouse como input para a rota��o do player
        _inputMousePos = inputValue.ReadValue<Vector2>();
    }
}
