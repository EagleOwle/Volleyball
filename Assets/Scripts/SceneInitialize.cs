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

        tmp = Object.Instantiate(scene.playerPrefab);
        Player = tmp.GetComponent<Unit>();
        Player.transform.position = Cort.PlayerSpawnPoint.position;

        tmp = Object.Instantiate(scene.enemyPrefab);
        Bot = tmp.GetComponent<Unit>();
        Bot.transform.position = Cort.BotSpawnPoint.position;

        tmp = Object.Instantiate(Preference.Singleton.balls[scene.ballIndex].prefab);
        Ball = tmp.GetComponent<Ball>();
        Ball.Initialise(scene.ballIndex);
        Ball.transform.position = Cort.BallSpawnPoint.position;

        //Player = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Player", typeof(Unit)) as Unit);
        //Bot = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Enemy", typeof(Unit)) as Unit);
        //Ball = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Volleyball", typeof(Ball))) as Ball;
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
