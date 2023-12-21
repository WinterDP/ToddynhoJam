using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class CallNearbyAlliesState : BaseState
{
    private BaseEnemy baseEnemy;

    private Task callingAlliesTask;

    private bool alreadyCalledAllies = false;

    public CallNearbyAlliesState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject)
    {
        this.baseEnemy = baseEnemy;
    }

    public override void EnterState()
    {
        if (alreadyCalledAllies)
            return;

        callingAlliesTask = StartCallingAllies();
    }

    private async Task StartCallingAllies()
    {
        Debug.Log("Calling");
        baseEnemy.Agent.SetDestination(transform.position);
        await Task.Delay(5000);

        var dinos = Physics2D.OverlapCircleAll(transform.position, baseEnemy.LoseAgroRadius);
        foreach (var dino in dinos)
        {
            var dinoStateMachine = dino.gameObject.GetComponent<StateMachine>();
            if (dinoStateMachine != null)
            {
                dinoStateMachine.ForceAggro();
            }
        }
        alreadyCalledAllies = true;
        Debug.Log("Called");
    }

    public override Type Tick()
    {
        if (!callingAlliesTask.IsCompleted)
            return typeof(CallNearbyAlliesState);

        baseEnemy.AudioSource.PlayOneShot(baseEnemy.ScreamSound);
        return typeof(ChaseState);
    }


    private float CalculateDistanceFromTarget()
    {
        return Vector3.Distance(GameManager.Instance.GetPlayerReference().position, transform.position);
    }
}
