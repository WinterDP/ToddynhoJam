using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StunState : BaseState
{
    private BaseEnemy baseEnemy;

    private Transform currentTarget;

    private bool comingFromOtherState = true;
    private bool started = false;

    private Task stunTask;

    public StunState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject)
    {
        this.baseEnemy = baseEnemy;
        currentTarget = baseEnemy.Target;
        baseEnemy.Agent.SetDestination(transform.position);
    }
    public override Type Tick()
    {
        if (!stunTask.IsCompleted)
            return typeof(StunState);

        comingFromOtherState = false;
        return typeof(ChaseState);

    }

    private async Task StartStun()
    {
        baseEnemy.Agent.SetDestination(transform.position);
        await Task.Delay(baseEnemy.StunTimeInMilliseconds);

       
        Debug.Log("Called Stun");
    }


    public override void EnterState()
    {
        comingFromOtherState = true;
        stunTask = StartStun();
    }

}
