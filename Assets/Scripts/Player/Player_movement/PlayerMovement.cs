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
    private StateHandler _stateHandler;

    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    #endregion

    #region Vari�veis: Dire��o do input para movimento

    private Vector2 _inputMovementDirection;
    public Vector2 InputMovementDirection
    {
        get => _inputMovementDirection;
        set => _inputMovementDirection = value;
    }
    private Vector2 _inputMousePos;
    public Vector2 InputMousePos
    {
        get => _inputMousePos;
        set => _inputMousePos = value;
    }
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
    public float PlayerSpeedRunningModifier
    {
        get => _playerSpeedRunningModifier;
        set => _playerSpeedRunningModifier = value;
    }
    [SerializeField]
    [Range(0, 1)]
    private float _playerSpeedCrouchingModifier;
    public float PlayerSpeedCrouchingModifier
    {
        get => _playerSpeedCrouchingModifier;
        set => _playerSpeedCrouchingModifier = value;
    }
    [SerializeField]
    [Range(0, 1)]
    private float _playerSpeedBackwardsModifier;
    [SerializeField]
    private float _playerAngleFowardMovimentation;

    private float _currentSpeedCrouchModifier;
    public float CurrentSpeedCrouchModifier
    {
        get => _currentSpeedCrouchModifier;
        set => _currentSpeedCrouchModifier = value;
    }
    private float _currentSpeedRunModifier;
    public float CurrentSpeedRunModifier
    {
        get => _currentSpeedRunModifier;
        set => _currentSpeedRunModifier = value;
    }
    #endregion

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _stateHandler = gameObject.GetComponent<StateHandler>();
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

        if (!_stateHandler.HasStamina)
        {
            _currentSpeedRunModifier = 0f;
        }
        else
        {
            if (_currentSpeedRunModifier == 0f)
            {
                _currentSpeedRunModifier = 1f;
            }
        }

        if (_currentSpeedRunModifier != 1f && _currentSpeedRunModifier != 0f)
            _stateHandler.IsRunning = true;

        
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
            _stateHandler.IsWalkingFoward = true;
            _stateHandler.IsWalkingBackward = false;
            PlayerNoise.OnNoiseChange?.Invoke(PlayerNoise.Instance.NormalMovementNoise);
        }
        else
        {
            // Est� se movendo para tr�s
            // Adiciona velocidade para o corpo, reduzida por se mover para tr�s, de acordo com a dire��o passada pelo input
            _rigidbody2D.velocity = _smoothedMovementInput * _playerCurrentSpeed * _playerSpeedBackwardsModifier;
            _stateHandler.IsWalkingBackward = true;
            _stateHandler.IsWalkingFoward = false;
            PlayerNoise.OnNoiseChange?.Invoke(PlayerNoise.Instance.BackwardsMovementNoise);
        }
        

        if (_inputMovementDirection == Vector2.zero || _rigidbody2D.velocity == Vector2.zero)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _stateHandler.IsWalkingFoward = false;
            _stateHandler.IsWalkingBackward = false;
            _stateHandler.IsRunning = false;
            PlayerNoise.OnNoiseChange?.Invoke(0f);
        }
        
    }

    private void HandleRotation()
    {
        // Calcula a dire��o que o player deve olhar
        Vector2 facingDirection = _mainCamera.ScreenToWorldPoint(_inputMousePos) - transform.position;
        float facingAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        _rigidbody2D.MoveRotation(facingAngle);

    }

    
}
