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

    #endregion


    #region Vari�veis: Dire��o do input para movimento

    private Vector2 _inputMovementDirection;
    private Vector2 _inputMousePos;

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

    #endregion

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _mainCamera = Camera.main;
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
        // Faz uma transi��o suave em um dado tempo para a varia��o do valor do input
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _inputMovementDirection,
            ref _movementInputSmoothVelocity,
            _smoothTime
            );
        // Adiciona velocidade para o corpo, de acordo com a dire��o passada pelo input
        _rigidbody2D.velocity = _smoothedMovementInput * _playerSpeed;
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

        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;

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

    private void OnLook(InputAction.CallbackContext inputValue)
    {
        // Pega a posi��o do mouse como input para a rota��o do player
        _inputMousePos = inputValue.ReadValue<Vector2>();
    }
}
