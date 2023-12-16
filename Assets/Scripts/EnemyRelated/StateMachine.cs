using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> avaibleStates;
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
            Debug.Log(nextState);
            SwitchState(nextState);
        }
    }

    private void SwitchState(Type nextState)
    {
        currentState = avaibleStates[nextState];
        OnStateChanged?.Invoke(currentState);
    }
}