using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public string nameState = "None";
    public abstract void Enter();
    public abstract void Exit();

    public static implicit operator State(Type v)
    {
        throw new NotImplementedException();
    }
}
