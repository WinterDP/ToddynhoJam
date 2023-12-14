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

    [SerializeField]
    private float _fovLatern;
    [SerializeField]
    private float _viewDistanceLantern;

    [SerializeField]
    private float _viewDistanceCloseVision;

    // Start is called before the first frame update
    void Start()
    {
        _fovLatern = _lantern.pointLightOuterAngle - _lantern.pointLightInnerAngle;
        _viewDistanceLantern = _lantern.pointLightOuterRadius;

        _viewDistanceCloseVision = _closeVision.pointLightOuterRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
