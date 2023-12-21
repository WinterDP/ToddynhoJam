using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChaseByLightSensivityState : BaseState
{
    private BaseEnemy baseEnemy;
    public ChaseByLightSensivityState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject) { this.baseEnemy = baseEnemy; }
    public override Type Tick()
    {

        TickChase();

        float distance = CalculateDistanceFromTarget();

        if (!baseEnemy.AudioSource.isPlaying)
            baseEnemy.AudioSource.PlayOneShot(baseEnemy.ScreamSound);

        if (distance > baseEnemy.LoseAgroRadius)
        {
            return typeof(PatrolState);
        }
        else if (distance < baseEnemy.AttackRadius)
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
        baseEnemy.SetTarget(GameManager.Instance.GetPlayerReference());
    }
}
