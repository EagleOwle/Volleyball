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
    public Action ActionRoundFall;

    public Match match;

    [SerializeField]private SceneInitialize sceneInitialize;

    private void Start()
    {
        if(SceneLoader.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        StateMachine.InitBeheviors();
        match = new Match();
        match.Initialise();
    }

    private void OnEnable()
    {
       StateMachine.actionChangeState += ChangeState;
    }

    private void ChangeState(State obj)
    {
        currentState = obj.nameState;
    }

    public void OnRoundFall(PlayerType luser)
    {
        match.SetScore(luser);
        ActionRoundFall?.Invoke();
        StateMachine.SetState<FallState>();
    }

    public void RestartMatch()
    {
        match.round++;
        StartCoroutine(FallWait());
    }

    private IEnumerator FallWait()
    {
        yield return new WaitForSeconds(1);
        sceneInitialize.Restart();
    }

}
