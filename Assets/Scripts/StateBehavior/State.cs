using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public string nameState = "None";

    public virtual void Enter()
    {
        nameState = "None";
    }

    public virtual void Exit()
    {

    }

}
