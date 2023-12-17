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

}