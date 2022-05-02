using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
    private Cort cort;
    private GameObject player;
    private GameObject bot;
    private Ball ball;

    public void Restart()
    {
        ClearGame();

        cort = Instantiate(Resources.Load("Prefabs/Cort", typeof(Cort)) as Cort);
        Vector3 rnd =new Vector3(Random.Range(-0.1f, 0.1f),0,0);
        ball = Instantiate(Resources.Load("Prefabs/Volleyball", typeof(Ball)), cort.BallSpawnPoint.position + rnd, Quaternion.identity) as Ball;
        player = Instantiate(Resources.Load("Prefabs/Player"), cort.PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
        bot = Instantiate(Resources.Load("Prefabs/Enemy"), cort.BotSpawnPoint.position, Quaternion.identity) as GameObject;

        StartGame();
        StateMachine.SetState<PauseState>();
    }

    private void StartGame()
    {
        UIGame.Instance.Initialise();
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
