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

    public LayerMask targetMask;

    [Header("Chase Specifics")]
    public float AttackRadius = 2f;
    public float DetectionRadius = 7f;
    public float StartAggroRadius = 5f;
    public float LoseAgroRadius = 10f;
    public float Speed = 2f;
    public int Damage = 1;
    public int StunTimeInMilliseconds = 200;

    [Header("Patrol Related")]
    [SerializeField] private float rangeX;
    [SerializeField] private float rangeY;
    private Vector3 startPosition;
    public float RangeX => rangeX;
    public float RangeY => rangeY;
    public Vector3 StartPosition => startPosition;
    protected void InitializeStateMachine(Dictionary<Type, BaseState> states)
    {
        startPosition = transform.position;
        StateMachine.SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, StartAggroRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, LoseAgroRadius);
    }
}