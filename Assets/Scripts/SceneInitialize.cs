using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class SceneInitialize
{
    private static Cort cort;
    private static Unit player;
    private static Unit bot;
    private static Ball ball;

    public static void StartRound(PlayerType lastLuser)
    {
        if (ball == null)
        {
            InstanceActors();
        }

        ArrangementOfActors(lastLuser);

        UIGame.Instance.StartRound();
        UIHud.Singletone.OnChangePanel(UIPanelName.Timer);
    }

    private static void ArrangementOfActors(PlayerType lastLuser)
    {
        switch (lastLuser)
        {
            case PlayerType.None:
                player.transform.position = cort.PlayerSpawnPoint.position;
                bot.transform.position = cort.BotSpawnPoint.position;
                ball.transform.position = cort.BallSpawnPoint.position;
                break;
            case PlayerType.Local:
                player.transform.position = cort.PlayerSpawnPoint.position;
                bot.transform.position = cort.BotSpawnPoint.position;
                ball.transform.position = cort.BotSpawnPoint.position + Vector3.up * 3;
                break;
            case PlayerType.Rival:
                player.transform.position = cort.PlayerSpawnPoint.position;
                bot.transform.position = cort.BotSpawnPoint.position;
                ball.transform.position = cort.PlayerSpawnPoint.position + Vector3.up * 3; 
                break;
        }
    }

    private static void InstanceActors()
    {
        cort = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Cort", typeof(Cort)) as Cort);
        player = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Player", typeof(Unit)) as Unit);
        bot = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Enemy", typeof(Unit)) as Unit);
        ball = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Volleyball", typeof(Ball))) as Ball;
    }

}
