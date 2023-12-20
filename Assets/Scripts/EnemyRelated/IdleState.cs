using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private BaseEnemy baseEnemy;

    private bool calledAllies = false;
    public IdleState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject)
    {
        this.baseEnemy = baseEnemy;
    }

    public override void EnterState()
    {
        
    }

    public override Type Tick()
    {
        float distance = CalculateDistanceFromTarget();

        if (distance < baseEnemy.StartAggroRadius && !baseEnemy.StateMachine.AvaibleStates.ContainsKey(typeof(CallNearbyAlliesState)))
            return typeof(ChaseState);
        else if(distance < baseEnemy.DetectionRadius)
        {
            if(!calledAllies && !baseEnemy.StateMachine.AvaibleStates.ContainsKey(typeof(CallNearbyAlliesState)))
            {
                calledAllies = true;
                return typeof(IdleState);
            }
            else
                return typeof(CallNearbyAlliesState);
        }
        else
            return typeof(PatrolState);
    }


    private float CalculateDistanceFromTarget()
    {
        return Vector3.Distance(GameManager.Instance.GetPlayerReference().position, transform.position) + PlayerNoise.NoiseDistance;
    }

}