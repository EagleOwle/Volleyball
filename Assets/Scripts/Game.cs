using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Singleton
    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Game>();
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] private AudioClip roundFallClip;
   
    public Action<RoundResult> actionRoundFail;
    public Action<PlayerType, int> actionUnitHit;
    public ScenePreference.Scene scene;
    public Match match;

    private PlayerType lastLuser = PlayerType.None;

    private Cort Cort;
    private Unit Player;
    private Unit Bot;
    private Ball Ball;

    private void Start()
    {
        if (SceneLoader.Instance == null)
        {
            SceneManager.LoadScene(0);
            return;
        }

        scene = ScenePreference.Singleton.GameScene;

        StateMachine.InitBeheviors();
        StateMachine.actionChangeState += ChangeState;
        StateMachine.SetState<PreviewSceneState>();

        match = new Match();
        match.Initialise(scene.matchPreference.Rounds);

        lastLuser = PlayerType.None;

        InstantiatePerson(scene);
    }

    private void InstantiatePerson(ScenePreference.Scene scene)
    {
        Cort = GameObject.FindObjectOfType<Cort>();
        Player = Instantiate(scene.playerPrefab, Cort.PlayerSpawnPoint.position, Quaternion.identity);
        Bot = Instantiate(scene.enemyPrefab, Cort.BotSpawnPoint.position, Quaternion.identity);
        Ball = Instantiate(Preference.Singleton.balls[scene.ballIndex].prefab);
        Ball.Initialise(scene.ballIndex, scene.matchPreference);
        Ball.transform.position = Cort.BallSpawnPoint.position + Vector3.right * AddRandomPosition();

        if (Game.Instance.scene.difficultEnum != ScenePreference.GameDifficult.Hard)
        {
            var trajectoryRender = Instantiate(Preference.Singleton.trajectoryRenderPrefab);
            trajectoryRender.Initialise(Ball);
        }
    }

    private void ChangeState(State next, State last)
    {
        if (last is PreviewSceneState)
        {
            StartRound();
        }
    }

    public void UnitHitBall(PlayerType playerType, int hitCount)
    {
        actionUnitHit?.Invoke(playerType, hitCount);
    }

    public void OnRoundFall(PlayerType luser)
    {
        AudioController.Instance.PlayInstanceClip(roundFallClip);

        RoundResult roundResult = match.SetScore(luser);
        lastLuser = luser;
        actionRoundFail?.Invoke(roundResult);

        StateMachine.SetState<FallState>();
    }

    public void RestartRound()
    {
        StartRound();
    }

    private void StartRound()
    {
        match.round++;
        ArrangementOfActors(lastLuser);
        UIGame.Instance.StartRound();
        UIHud.Instance.OnChangePanel(UIPanelName.Timer);
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
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

    private void OnDestroy()
    {
        StateMachine.actionChangeState -= ChangeState;
    }

}
