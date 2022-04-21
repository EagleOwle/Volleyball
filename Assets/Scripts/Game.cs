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

    public Action OnRoundFall;

    public Match match;

    [SerializeField]private SceneInitialize sceneInitialize;

    private void Start()
    {
        StateMachine.InitBeheviors();
        match = new Match();
    }

    private void OnEnable()
    {
        OnRoundFall += RoundFall;
    }

    private void OnDisable()
    {
        OnRoundFall -= RoundFall;
    }

    private void RoundFall()
    {
        StateMachine.SetState<FallState>();
    }

    public void RestartMatch()
    {
        match.round.roundNumber++;
        match.score++;
        StartCoroutine(FallWait());
    }

    private IEnumerator FallWait()
    {
        yield return new WaitForSeconds(1);
        sceneInitialize.Restart();
    }

}
