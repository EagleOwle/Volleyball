using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
    private Cort Cort;
    private Unit Player;
    private Unit Bot;
    private Ball Ball;

    public void Initialise(ScenePreference.Scene scene)
    {
        Cort = GameObject.FindObjectOfType<Cort>();

        if (Cort == null)
        {
            Debug.LogError("Cort is Null");
            return;
        }

        //GameObject tmp = null;

        var tmpPlayer = Object.Instantiate(scene.playerPrefab, Cort.PlayerSpawnPoint.position, Quaternion.identity);
        Player = tmpPlayer.GetComponent<Unit>();

        var tmpBot = Object.Instantiate(scene.enemyPrefab, Cort.BotSpawnPoint.position, Quaternion.identity);
        Bot = tmpBot.GetComponent<Unit>();

        var tmpBall = Object.Instantiate(Preference.Singleton.balls[scene.ballIndex].prefab);
        Ball = tmpBall.GetComponent<Ball>();
        Ball.Initialise(scene.ballIndex, scene.matchPreference);
        Ball.transform.position = Cort.BallSpawnPoint.position + Vector3.right * AddRandomPosition();

        if (Game.Instance.scene.difficultEnum != ScenePreference.GameDifficult.Hard)
        {
            var trajectoryRender = Object.Instantiate(Preference.Singleton.trajectoryRenderPrefab);
            trajectoryRender.Initialise(Ball);
        }
    }

    public void StartRound(PlayerType lastLuser)
    {
        ArrangementOfActors(lastLuser);
        UIGame.Instance.StartRound();
        UIHud.Instance.OnChangePanel(UIPanelName.Timer);
    }

    private void ArrangementOfActors(PlayerType lastLuser)
    {
        switch (lastLuser)
        {
            case PlayerType.None:
                Player.transform.position = Cort.PlayerSpawnPoint.position;
                Bot.transform.position = Cort.BotSpawnPoint.position;
                Ball.transform.position = Cort.BallSpawnPoint.position + (Vector3.right * AddRandomPosition());
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

    private float AddRandomPosition()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            return -0.1f;
        }
        else
        {
            return 0.1f;
        }
    }
}
