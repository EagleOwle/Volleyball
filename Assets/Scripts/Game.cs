using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string debugCurrentGameState;

    public Action<RoundResult> actionRoundFail;

    public ScenePreference.Scene scene;//Curret scene preference
    public Match match;

    private PlayerType lastLuser = PlayerType.None;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (SceneLoader.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        scene = ScenePreference.Singleton.scenes[ScenePreference.Singleton.nextScene];

        StateMachine.InitBeheviors();
        StateMachine.actionChangeState += ChangeState;

        match = new Match();
        match.Initialise(scene.rounds);      

        StartRound();
    }

    private void ChangeState(State obj)
    {
        debugCurrentGameState = obj.nameState;
    }

    public void OnRoundFall(PlayerType luser)
    {
        AudioController.Instance.PlayClip(roundFallClip);

        RoundResult roundResult = match.SetScore(luser);
        actionRoundFail?.Invoke(roundResult);

        lastLuser = luser;

        StateMachine.SetState<FallState>();
    }

    public void StartRound()
    {
        match.round++;
        SceneInitialize.StartRound(lastLuser);
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
    }

}
