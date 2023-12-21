using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolState : BaseState
{
    private BaseEnemy baseEnemy;

    private Vector3 currentPatrolTargetPosition;

    private bool comingFromOtherState = false;

    public PatrolState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject) 
    { 
        this.baseEnemy = baseEnemy;
        currentPatrolTargetPosition = CalculateRandomPosition();
        baseEnemy.Agent.SetDestination(currentPatrolTargetPosition);
    }
    public override Type Tick()
    {
        if (!baseEnemy.AudioSource.isPlaying)
            baseEnemy.AudioSource.PlayOneShot(baseEnemy.RunSound);


        if (comingFromOtherState)
        {
            comingFromOtherState = false;
            currentPatrolTargetPosition = baseEnemy.StartPosition;
            baseEnemy.Agent.SetDestination(currentPatrolTargetPosition);
        }
        if(Vector3.Distance(currentPatrolTargetPosition, transform.position) < .55f)
        {
            currentPatrolTargetPosition = CalculateRandomPosition();
            SetPatrolTarget(currentPatrolTargetPosition);
            baseEnemy.Agent.SetDestination(currentPatrolTargetPosition);
        }

        float playerDistance = CalculateDistanceFromPlayer();

        if (playerDistance < baseEnemy.DetectionRadius)
        {
            comingFromOtherState = true;
            return typeof(IdleState);
        }
        else
        {
            return typeof(PatrolState);
        }

    }

    private Vector3 CalculateRandomPosition()
    {
        float rngX =  UnityEngine.Random.Range(-baseEnemy.RangeX, baseEnemy.RangeX);
        float rngY =  UnityEngine.Random.Range(-baseEnemy.RangeY, baseEnemy.RangeY);
        return new Vector3(transform.position.x + rngX, transform.position.y + rngY, transform.position.z);
    }

    private void SetPatrolTarget(Vector3 position)
    {
        baseEnemy.Agent.SetDestination(position);
    }

    private float CalculateDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.GetPlayerReference().position) + PlayerNoise.NoiseDistance;
    }

    public override void EnterState()
    {
        baseEnemy.StateMachine.ChangeAnimationClip(baseEnemy.Walk);
    }
}
