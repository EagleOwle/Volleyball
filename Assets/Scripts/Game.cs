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
    public Action<PlayerSide, int> actionUnitHit;
    public ScenePreference.Scene scene;
    public Match match;

    public  Match.Player Luser => match.LastLuser();
    public Match.Player Winner => match.LastWinner();

    private Cort Cort;
    private Unit Player1;
    private Unit Player2;
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

        InstantiatePerson(scene);
        InstaiateBall();

        match = new Match();
        match.Initialise(scene.matchPreference.Rounds, Player1.PlayerName, Player2.PlayerName);

        if (Game.Instance.scene.difficultEnum != GameDifficult.Hard)
        {
            var trajectoryRender = Instantiate(Preference.Singleton.trajectoryRenderPrefab);
            trajectoryRender.Initialise(Ball);
        }
        //StartCoroutine(SendVKRequest());
    }

    //IEnumerator SendVKRequest()
    //{
    //    string url = "https://api.vk.com/method/users.get?user_ids=1&access_token=YOUR_ACCESS_TOKEN&v=5.131";

    //    UnityWebRequest www = UnityWebRequest.Get(url);

    //    yield return www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.LogError(www.error);
    //    }
    //    else
    //    {
    //        // Обработка успешного ответа
    //        Debug.Log(www.downloadHandler.text);
    //    }
    //}
    private void InstantiatePerson(ScenePreference.Scene scene)
    {
        Cort = GameObject.FindObjectOfType<Cort>();

        if (scene.playersType[0] == PlayerType.Human)
        {
            Player1 = Instantiate(scene.playerPrefab, Cort.Player1SpawnPoint.position, Quaternion.identity);
            Player1.Initialise(PlayerSide.Left, Preference.Singleton.player[0].Name);
        }
        else
        {
            Player1 = Instantiate(scene.aiPrefab, Cort.Player1SpawnPoint.position, Quaternion.identity);
            Player1.Initialise(PlayerSide.Left, "Bot Basil");
        }

        if (scene.playersType[1] == PlayerType.Human)
        {
            Player2 = Instantiate(scene.playerPrefab, Cort.Player2SpawnPoint.position, Quaternion.identity);
            Player2.Initialise(PlayerSide.Right, Preference.Singleton.player[1].Name);
        }
        else
        {
            Player2 = Instantiate(scene.aiPrefab, Cort.Player2SpawnPoint.position, Quaternion.identity);
            Player2.Initialise(PlayerSide.Right, "Bot Fedor");
        }
    }

    private void InstaiateBall()
    {
        Ball = Instantiate(Preference.Singleton.balls[scene.ballIndex].prefab);
        Ball.Initialise(scene.ballIndex, scene.matchPreference);
        Ball.transform.position = Cort.BallSpawnPoint.position + Vector3.right * AddRandomPosition();
    }

    private void ChangeState(State next, State last)
    {
        if (last is PreviewSceneState)
        {
            StartRound();
        }
    }

    public void UnitHitBall(PlayerSide playerType, int hitCount)
    {
        actionUnitHit?.Invoke(playerType, hitCount);
    }

    public void OnRoundFall(PlayerSide luser)
    {
        AudioController.Instance.PlayInstanceClip(roundFallClip);
        RoundResult result = match.SetLuser(luser);
        actionRoundFail?.Invoke(result);

        StateMachine.SetState<FallState>();
    }

    public void RestartRound()
    {
        StartRound();
    }

    private void StartRound()
    {
        PlayerSide side = PlayerSide.None;
        if (match.LastLuser() != null)
        {
            side = match.LastLuser().playerSide;
        }
        ArrangementOfActors(side);
        UIGame.Instance.StartRound();
        UIHud.Instance.OnChangePanel(UIPanelName.Timer);
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
    }

    private void ArrangementOfActors(PlayerSide lastLuser)
    {
        Player1.transform.position = Cort.Player1SpawnPoint.position;
        Player2.transform.position = Cort.Player2SpawnPoint.position;

        switch (lastLuser)
        {
            case PlayerSide.None:
                Ball.transform.position = Cort.BallSpawnPoint.position + (Vector3.right * AddRandomPosition());
                break;

            case PlayerSide.Left:
                Ball.transform.position = Cort.Player2SpawnPoint.position + Vector3.up * 2.5f;
                break;

            case PlayerSide.Right:
                Ball.transform.position = Cort.Player1SpawnPoint.position + Vector3.up * 2.5f;
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
