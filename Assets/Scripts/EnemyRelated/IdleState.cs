using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private BaseEnemy baseEnemy;
    public IdleState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject)
    {
        this.baseEnemy = baseEnemy;
    }
    public override Type Tick()
    {
        float distance = CalculateDistanceFromTarget();

        if (distance > baseEnemy.DetectionRadius)
            return typeof(IdleState);
        else
            return typeof(ChaseState);
    }


    private float CalculateDistanceFromTarget()
    {
        return Vector3.Distance(baseEnemy.Target.position, transform.position);
    }

}