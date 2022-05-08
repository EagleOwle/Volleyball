using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
    private Cort cort;
    private GameObject player;
    private GameObject bot;
    private Ball ball;

    private bool isIntialise = false;

    public void StartRound()
    {
        if (isIntialise == false)
        {
            cort = Instantiate(Resources.Load("Prefabs/Cort", typeof(Cort)) as Cort);
            ball = Instantiate(Resources.Load("Prefabs/Volleyball", typeof(Ball))) as Ball;
            ball.Initialise();
            player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
            bot = Instantiate(Resources.Load("Prefabs/Enemy")) as GameObject;

            isIntialise = true;
        }

        RestartRound();

    }

    private void RestartRound()
    {
        Vector3 rnd = new Vector3(Random.Range(-0.1f, 0.1f), 0, 0);
        ball.transform.position = cort.BallSpawnPoint.position + rnd;
        player.transform.position = cort.PlayerSpawnPoint.position;
        bot.transform.position = cort.BotSpawnPoint.position;

        UIGame.Instance.StartRound(ball);
        UIHud.Singletone.OnChangePanel(UIPanelName.Timer);
    }

    private void ClearGame()
    {
        if (cort != null)
        {
            Destroy(cort.gameObject);
        }

        if (player != null)
        {
            Destroy(player.gameObject);
        }

        if (ball != null)
        {
            Destroy(ball.gameObject);
        }

        if (bot != null)
        {
            Destroy(bot.gameObject);
        }
    }

}
