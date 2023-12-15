using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFOV : MonoBehaviour
{
    [Header("Visão do jogador")]
    [SerializeField]
    private Light2D _lantern;
    [SerializeField]
    private Light2D _closeVision;

    [Header("Camadas para ignorar")]
    [SerializeField]
    private LayerMask _ignoreLayerMask;

    private float _fovLatern;
    private float _viewDistanceLantern;
    private float _viewDistanceCloseVision;

    private PlayerMovement _playerMovementReference;

    // Start is called before the first frame update
    void Start()
    {
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
        RaycastHit2D ray = Physics2D.Raycast(transform.position, collision.transform.position - transform.position, _viewDistanceLantern,~(_ignoreLayerMask));

        if (ray.collider != null)
        {
            if (ray.collider.gameObject.CompareTag("Enemy"))
            {
                if (Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) < _viewDistanceLantern)
                {
                    // Calcula a direção que o player deve olhar
                    Vector2 facingDirection = _playerMovementReference.MainCamera.ScreenToWorldPoint(_playerMovementReference.InputMousePos) - _playerMovementReference.transform.position;
                    Debug.Log(Vector2.Angle((ray.collider.gameObject.transform.position - transform.position).normalized, facingDirection));
                    if (Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) < _viewDistanceCloseVision)
                    {
                        Debug.DrawRay(transform.position, ray.collider.gameObject.transform.position - transform.position, Color.green);
                    }
                    else if(Vector2.Angle((ray.collider.gameObject.transform.position - transform.position).normalized, facingDirection) < _fovLatern/2f)
                    {
                        Debug.DrawRay(transform.position, ray.collider.gameObject.transform.position - transform.position, Color.green);
                    }
                }
            }
        }
    }
}
