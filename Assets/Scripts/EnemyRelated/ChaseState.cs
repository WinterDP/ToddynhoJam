using NavMeshPlus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private BaseEnemy baseEnemy;
    public ChaseState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject) { this.baseEnemy = baseEnemy; }
    public override Type Tick()
    {

        TickChase();

        float distance = CalculateDistanceFromTarget();

        if (distance > baseEnemy.LoseAgroRadius)
        {
            return typeof(PatrolState);
        }
        else if(distance < baseEnemy.AttackRadius)
        {
            return typeof(AttackState);
        }
        else
        {
            return typeof(ChaseState);
        }

    }

    private void TickChase()
    {
        baseEnemy.Agent.SetDestination(baseEnemy.Target.position);
    }

    private float CalculateDistanceFromTarget()
    {
        return Vector3.Distance(baseEnemy.Target.position, transform.position);
    }

    public override void EnterState()
    {
        
    }
}
