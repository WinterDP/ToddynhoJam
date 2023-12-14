using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyExample01 : BaseEnemy
{
    //Custom Enemy, Custom Behaviours, Custom properties
    private void Awake()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            //Behaviuor/States here
            { typeof(IdleState), new IdleState(this) },
            { typeof(ChaseState), new ChaseState(this) }

        };

        SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
        InitializeStateMachine(states);
    }
}