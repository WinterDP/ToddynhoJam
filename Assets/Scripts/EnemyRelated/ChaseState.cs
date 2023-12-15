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

        if (distance > baseEnemy.DetectionRadius)
            return typeof(IdleState);
        else
            return typeof(ChaseState);

    }

    private void TickChase()
    {
    
    }

    private float CalculateDistanceFromTarget()
    {
        return Vector3.Distance(baseEnemy.Target.position, transform.position);
    }
}
