using UnityEngine;
using System.Collections;

public class GameState : State
{
    public override void Enter()
    {
        nameState = "Game";
    }

    public override void Exit()
    {
        
    }
}
