using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
    private Cort cort;
    private GameObject player;
    private GameObject ball;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        ClearGame();

        cort = Instantiate(Resources.Load("Prefabs/Cort", typeof(Cort)) as Cort);
        player = Instantiate(Resources.Load("Prefabs/Player"), cort.PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
        ball = Instantiate(Resources.Load("Prefabs/Volleyball"), cort.BallSpawnPoint.position, Quaternion.identity) as GameObject;

        UIHud.Instance.ChangePanel("TimerPanel");
    }

    private void ClearGame()
    {
        if(cort != null)
        {
            Destroy(cort.gameObject);
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
