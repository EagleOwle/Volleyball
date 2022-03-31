using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
    private Enviropment enviropment;
    private GameObject player;
    private GameObject ball;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        ClearGame();

        enviropment = Instantiate(Resources.Load("Prefabs/Enviropment", typeof(Enviropment)) as Enviropment);
        player = Instantiate(Resources.Load("Prefabs/Player"), enviropment.PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
        ball = Instantiate(Resources.Load("Prefabs/Ball"), enviropment.BallSpawnPoint.position, Quaternion.identity) as GameObject;

        UIHud.Instance.ChangePanel("TimerPanel");
    }

    private void ClearGame()
    {
        if(enviropment != null)
        {
            Destroy(enviropment.gameObject);
        }

        if(player != null)
        {
            Destroy(player);
        }

        if(ball != null)
        {
            Destroy(ball);
        }
    }

}
