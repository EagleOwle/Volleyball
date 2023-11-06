using UnityEngine;

public static class SceneInitialize
{
    private static Cort Cort;
    private static Unit Player;
    private static Unit Bot;
    private static Ball Ball;

    public static void Initialise(ScenePreference.Scene scene)
    {
        Cort = GameObject.FindObjectOfType<Cort>();

        if (Cort == null)
        {
            Debug.LogError("Cort is Null");
            return;
        }

        GameObject tmp = null;

        tmp = Object.Instantiate(scene.playerPrefab, Cort.PlayerSpawnPoint.position, Quaternion.identity);
        Player = tmp.GetComponent<Unit>();
        //Player.transform.position = Cort.PlayerSpawnPoint.position;

        tmp = Object.Instantiate(scene.enemyPrefab, Cort.BotSpawnPoint.position, Quaternion.identity);
        Bot = tmp.GetComponent<Unit>();
        //Bot.transform.position = Cort.BotSpawnPoint.position;

        tmp = Object.Instantiate(Preference.Singleton.balls[scene.ballIndex].prefab);
        Ball = tmp.GetComponent<Ball>();
        Ball.Initialise(scene.ballIndex, scene.matchPreference);
        Ball.transform.position = Cort.BallSpawnPoint.position + Vector3.right * AddRandomPosition();

        if (Game.Instance.scene.difficultEnum != ScenePreference.GameDifficult.Hard)
        {
            var trajectoryRender = Object.Instantiate(Preference.Singleton.trajectoryRenderPrefab);
            trajectoryRender.Initialise(Ball);
        }
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

                var position = Vector3.right * AddRandomPosition();
                Ball.transform.position = Cort.BallSpawnPoint.position + position;

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

        Debug.Break();
    }

    private static float AddRandomPosition()
    {
        if(UnityEngine.Random.Range(0,2) == 0)
        {
            return -0.2f;
        }
        else
        {
            return 0.2f;
        }
    }

}
