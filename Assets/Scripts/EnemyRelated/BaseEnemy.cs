using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    public Transform Target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();
    [HideInInspector] public NavMeshAgent Agent => GetComponent<NavMeshAgent>();


    [Header("Masks")]
    public LayerMask targetMask;

    [Header("Field of View")]
    public float DetectionRadius = 5f;

    protected void InitializeStateMachine(Dictionary<Type, BaseState> states)
    {
        StateMachine.SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}