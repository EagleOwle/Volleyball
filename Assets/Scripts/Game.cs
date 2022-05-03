using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
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

    [SerializeField] private AudioClip roundFallClip;
    public string currentState;
    public Action<bool, PlayerType> ActionRoundFall;

    public Match match;

    [SerializeField] private SceneInitialize sceneInitialize;

    private void Start()
    {
        if (SceneLoader.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        StateMachine.InitBeheviors();
        StateMachine.actionChangeState += ChangeState;

        match = new Match();
        match.Initialise();      

        StartRound();
    }

    private void ChangeState(State obj)
    {
        currentState = obj.nameState;
    }

    public void OnRoundFall(PlayerType luser)
    {
        AudioController.Instance.PlayClip(roundFallClip);
        bool endMatch = match.SetScore(luser);
        ActionRoundFall?.Invoke(endMatch, luser);
        StateMachine.SetState<FallState>();
    }

    public void StartRound()
    {
        match.round++;
        sceneInitialize.StartRound();
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
    }

}
