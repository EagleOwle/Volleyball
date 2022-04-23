using UnityEngine;
using System.Collections;

public class PauseState : State
{
    public override void Enter()
    {
        nameState = "Pause";
    }
}
