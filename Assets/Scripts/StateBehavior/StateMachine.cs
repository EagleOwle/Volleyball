using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateMachine
{
    private static Dictionary<Type, State> behaviorMap;
    public static State currentState;
    public static Action<State> actionChangeState;

    public static void InitBeheviors()
    {
        behaviorMap = new Dictionary<Type, State>();
        behaviorMap[typeof(GameState)] = new GameState();
        behaviorMap[typeof(PauseState)] = new PauseState();
        behaviorMap[typeof(FallState)] = new FallState();
        behaviorMap[typeof(TimerState)] = new TimerState();
        SetDefaultState();
    }

    public static void SetState<T>() where T : State
    {
        var type = typeof(T);
        var newState = behaviorMap[type];
        SetState(newState);
    }

    private static void SetDefaultState()
    {
        currentState = GetState<PauseState>();
        currentState.Enter();
    }

    private static void SetState(State newState)
    {
        if (currentState != null)
        {
            if(currentState == newState)
            {
                return;
            }
            else
            {
                currentState.Exit();
            }
        }
        
        currentState = newState;
        currentState.Enter();
        actionChangeState?.Invoke(currentState);
    }

    public static State GetState<T>() where T : State
    {
        var type = typeof(T);
        return behaviorMap[type];
    }
}
