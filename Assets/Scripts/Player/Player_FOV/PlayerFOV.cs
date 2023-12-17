using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFOV : MonoBehaviour
{
    #region Variáveis: Componentes do game Object
    [Header("Visão do jogador")]
    [SerializeField]
    private Light2D _lantern;
    [SerializeField]
    private Light2D _closeVision;

    [Header("Camadas para ignorar")]
    [SerializeField]
    private LayerMask _ignoreLayerMask;

    // Referencia ao player
    private PlayerMovement _playerMovementReference;
    #endregion

    #region Variáveis: Propriedades do FOV
    private float _fovLatern;
    private float _viewDistanceLantern;
    private float _viewDistanceCloseVision;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        // Inicia as variáveis necessárias
        _fovLatern = _lantern.pointLightOuterAngle;
        _viewDistanceLantern = _lantern.pointLightOuterRadius;

        _viewDistanceCloseVision = _closeVision.pointLightOuterRadius;

        _playerMovementReference = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
            Função é disparada quando algo colide e fica no Trigger que representa o "campo de visão" do player 
        */

        // Atualiza as configurações do FOV caso alguma tenha sido alterada
        // Lanterna:
        _fovLatern = _lantern.pointLightOuterAngle;
        _viewDistanceLantern = _lantern.pointLightOuterRadius;

        // Visão Próxima:
        _viewDistanceCloseVision = _closeVision.pointLightOuterRadius;

        // Faz um Raycast direcionado aos itens que ativaram o trigger, evitando colidir no próprio player através da layer mask
        RaycastHit2D[] rays = Physics2D.RaycastAll(transform.position, collision.transform.position - transform.position, _viewDistanceLantern,~(_ignoreLayerMask));

        if (rays != null)
        {
            // em caso do primeiro raycast atingir alguma parede ele é ignorado
            if (rays.Length > 0 && !rays[0].collider.gameObject.CompareTag("Building"))
                foreach (RaycastHit2D ray in rays)
                {
                    EntitySpotted(ray);
                }
        }
    }

    public void EntitySpotted(RaycastHit2D ray)
    {
        // Verifica se a entidade avistada está no alcance da lanterna
        if (Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) < _viewDistanceLantern || (Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) < _viewDistanceCloseVision))
        {
            // Calcula a direção que o player está olhando
            Vector2 facingDirection = _playerMovementReference.MainCamera.ScreenToWorldPoint(_playerMovementReference.InputMousePos) - _playerMovementReference.transform.position;
            // Verifica se a entidade avistada está no alcance da visão próxima ou se não está na angulação visivel na lanterna
            if ((Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) < _viewDistanceCloseVision) || (Vector2.Angle((ray.collider.gameObject.transform.position - transform.position).normalized, facingDirection) < _fovLatern / 2f))
            {
                // Verifica se o raio atingiu algum inimigo
                if (ray.collider.gameObject.CompareTag("Enemy"))
                    EnemySpotted(ray);
            }
        }
    }

    public void EnemySpotted(RaycastHit2D ray)
    {
        // É chamada caso um inimigo seja observado 
        Debug.DrawRay(transform.position, ray.collider.gameObject.transform.position - transform.position, Color.green);
    }
}
