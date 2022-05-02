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
            if(_instance==null)
            {
                _instance = GameObject.FindObjectOfType<Game>();
            }

            return _instance;
        }
    }

    public string currentState;
    public Action<bool> ActionRoundFall;

    public Match match;

    [SerializeField]private SceneInitialize sceneInitialize;

    private void Start()
    {
        if(SceneLoader.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        StateMachine.actionChangeState += ChangeState;

        StateMachine.InitBeheviors();
        match = new Match();
        match.Initialise();
    }

    private void ChangeState(State obj)
    {
        currentState = obj.nameState;
    }

    public void OnRoundFall(PlayerType luser)
    {
        bool endMatch = match.SetScore(luser);
        ActionRoundFall?.Invoke(endMatch);
        StateMachine.SetState<FallState>();
    }

    public void StartRound()
    {
        match.round++;
        StartCoroutine(FallWait());
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
    }

    private IEnumerator FallWait()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        sceneInitialize.Restart();
    }

}
