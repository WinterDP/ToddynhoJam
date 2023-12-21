using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> avaibleStates;
    public Dictionary<Type, BaseState> AvaibleStates => avaibleStates;
    public BaseState currentState { get; private set; }

    public event Action<BaseState> OnStateChanged;

    public Animator Animator => GetComponentInChildren<Animator>();

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        avaibleStates = states;
        currentState = avaibleStates.Values.First();
    }

    void Update()
    {
        var nextState = currentState.Tick();

        if (nextState != null && nextState != currentState.GetType())
        {
            SwitchState(nextState);
        }
    }

    private void SwitchState(Type nextState)
    {
        currentState = avaibleStates[nextState];
        currentState.EnterState();
        OnStateChanged?.Invoke(currentState);
    }

    public void ActivateLightSensivity()
    {
        if (!avaibleStates.ContainsKey(typeof(ChaseByLightSensivityState)))
            return;

        currentState = avaibleStates[typeof(ChaseByLightSensivityState)];
        currentState.EnterState();
        OnStateChanged?.Invoke(currentState);
    }

    public void ForceAggro()
    {
        currentState = avaibleStates[typeof(ChaseState)];
        currentState.EnterState();
        OnStateChanged?.Invoke(currentState);
    }

    public void ForceStunState()
    {
        currentState = avaibleStates[typeof(StunState)];
        currentState.EnterState();
        OnStateChanged?.Invoke(currentState);
    }

    public void ChangeAnimationClip(string name)
    {
        foreach (var animation in Animator.GetCurrentAnimatorClipInfo(0))
        {
            if (animation.clip.name == name && !IsPlayingAnimation())
            {
                Animator.Play(name);
            }
        }
    }

    public bool IsPlayingAnimation()
    {
        bool returnValue = false;
        foreach (var animation in Animator.GetCurrentAnimatorClipInfo(0))
        {
            switch (animation.clip.name)
            {
                case "IdleDinoNormal":
                    returnValue = true;
                    break;
                case "attackDinoNormal":
                    returnValue = true;
                    break;
                case "runDinoNormal":
                    returnValue = true;
                    break;
                case "walkDinoNormal":
                    returnValue = true;
                    break;
                default:
                    returnValue = false;
                    break;
            }
        }

        return returnValue;
    }


}