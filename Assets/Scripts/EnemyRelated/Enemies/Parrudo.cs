using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parrudo : BaseEnemy
{
    private void Awake()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            //Behaviuor/States here
            { typeof(PatrolState), new PatrolState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(AttackState), new AttackState(this) },
            { typeof(CallNearbyAlliesState), new CallNearbyAlliesState(this) },
            { typeof(ChaseState), new ChaseState(this) }

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