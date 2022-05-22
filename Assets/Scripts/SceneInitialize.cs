using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class SceneInitialize
{
    private static Cort Cort;
    private static Unit Player;
    private static Unit Bot;
    private static Ball Ball;

    public static void Initialise()
    {
        Cort = GameObject.FindObjectOfType<Cort>();
        Player = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Player", typeof(Unit)) as Unit);
        Player.transform.position = Cort.PlayerSpawnPoint.position;
        Bot = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Enemy", typeof(Unit)) as Unit);
        Bot.transform.position = Cort.BotSpawnPoint.position;
        //Ball = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Head", typeof(Ball))) as Ball;
        Ball = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Volleyball", typeof(Ball))) as Ball;
        
        Ball.transform.position = Cort.BallSpawnPoint.position;
    }

    public static void StartRound(PlayerType lastLuser)
    {
        ArrangementOfActors(lastLuser);

        UIGame.Instance.StartRound();
        UIHud.Singletone.OnChangePanel(UIPanelName.Timer);
    }

    private static void ArrangementOfActors(PlayerType lastLuser)
    {
        switch (lastLuser)
        {
            case PlayerType.None:
                Player.transform.position = Cort.PlayerSpawnPoint.position;
                Bot.transform.position = Cort.BotSpawnPoint.position;
                Ball.transform.position = Cort.BallSpawnPoint.position;
                break;
            case PlayerType.Local:
                Player.transform.position = Cort.PlayerSpawnPoint.position;
                Bot.transform.position = Cort.BotSpawnPoint.position;
                Ball.transform.position = Cort.BotSpawnPoint.position + Vector3.up * 2.5f;
                break;
            case PlayerType.Rival:
                Player.transform.position = Cort.PlayerSpawnPoint.position;
                Bot.transform.position = Cort.BotSpawnPoint.position;
                Ball.transform.position = Cort.PlayerSpawnPoint.position + Vector3.up * 2.5f; 
                break;
        }
    }

}
