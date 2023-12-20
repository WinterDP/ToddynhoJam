using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class AttackState : BaseState
{
    private BaseEnemy baseEnemy;
    private bool playingAttack = false;
    private bool playedAttack = false;
    public AttackState(BaseEnemy baseEnemy) : base(baseEnemy.gameObject) { this.baseEnemy = baseEnemy; }

    public override void EnterState()
    {
        playingAttack = false;
        playedAttack = false;
    }

    public override Type Tick()
    {
        AttackAnimation();

        if (playedAttack)
            return typeof(ChaseState);
        else
            return typeof(AttackState);

    }

    private async void AttackAnimation()
    {
        if (playingAttack)
            return;
        playingAttack = true;

        baseEnemy.Agent.SetDestination(transform.position);
        await Task.Delay(1000); //anim duration test
        GameManager.Instance.GetPlayerReference().GetComponent<UnitHealth>().TakeDamage(baseEnemy.Damage);
        playedAttack = true;
        playingAttack = false;
    }


}
