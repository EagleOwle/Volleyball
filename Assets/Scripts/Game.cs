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
    public SceneInitialize sceneInitialize;

    private PlayerType lastLuser = PlayerType.None;

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

        sceneInitialize.Initialise(scene);
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
        sceneInitialize.StartRound(lastLuser);
       
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
    }

}
