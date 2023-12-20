using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Normal : BaseEnemy
{
    private void Awake()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            //Behaviuor/States here
            { typeof(PatrolState), new PatrolState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(AttackState), new AttackState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(StunState), new StunState(this) }

        };

        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Agent.speed = Speed;

        InitializeStateMachine(states);
    }

    private void Start()
    {
        SetTarget(GameManager.Instance.GetPlayerReference());
    }
}
