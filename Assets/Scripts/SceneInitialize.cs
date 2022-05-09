using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
    private Cort cort;
    private Unit player;
    private Unit bot;
    private Ball ball;

    private bool isIntialise = false;
    private bool readyForStart = false;

    public void StartRound()
    {
        if (isIntialise == false)
        {
            cort = Instantiate(Resources.Load("Prefabs/Cort", typeof(Cort)) as Cort);
            player = Instantiate(Resources.Load("Prefabs/Player", typeof(Unit)) as Unit);
            bot = Instantiate(Resources.Load("Prefabs/Enemy", typeof(Unit)) as Unit);
            ball = Instantiate(Resources.Load("Prefabs/Volleyball", typeof(Ball))) as Ball;
            isIntialise = true;
        }

        player.transform.position = cort.PlayerSpawnPoint.position;
        bot.transform.position = cort.BotSpawnPoint.position;
        ball.transform.position = cort.BallSpawnPoint.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 0, 0);

        ReadyForStart();
    }

    private async void ReadyForStart()
    {
        while (readyForStart == false)
        {
            await Task.Yield();
        }

        UIGame.Instance.StartRound();
        UIHud.Singletone.OnChangePanel(UIPanelName.Timer);
    }

    private void SceneLoadComplete()
    {
        readyForStart = true;
    }

    private void OnEnable()
    {
        //readyForStart = false;
        SceneLoader.OnSceneLoadComplete += SceneLoadComplete;
    }

    private void OnDisable()
    {
        if (SceneLoader.Instance == null) return;
        SceneLoader.OnSceneLoadComplete -= SceneLoadComplete;
    }

    private void OnDestroy()
    {
        if (SceneLoader.Instance == null) return;
        SceneLoader.OnSceneLoadComplete -= SceneLoadComplete;
    }

}
