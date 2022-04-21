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

        SetDefaultState();
    }
    
    public static void SetState<T>() where T : State
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        var type = typeof(T);
        var newState = behaviorMap[type];
        SetState(newState);
    }

    private static void SetDefaultState()
    {
        var defaultState = GetState<GameState>();
        SetState(defaultState);
    }

    private static void SetState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
        actionChangeState?.Invoke(currentState);
    }

    private static State GetState<T>() where T : State
    {
        var type = typeof(T);
        return behaviorMap[type];
    }
}
