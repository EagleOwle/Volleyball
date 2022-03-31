using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum Status { game, pause, fall }

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

    private Status _currentStatus;
    public Status status => _currentStatus;

    public Action OnRoundFall;
    public Action<Status> OnChangeMenu;

    public Match match;

    public void SetStatus(Status status)
    {
        switch (status)
        {
            case Status.game:
                _currentStatus = Status.game;
                break;
            case Status.pause:
                _currentStatus = Status.pause;
                break;

            case Status.fall:
                _currentStatus = Status.fall;
                break;
        }

        OnChangeMenu?.Invoke(_currentStatus);

    }

    [SerializeField]private SceneInitialize sceneInitialize;

    private void Start()
    {
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
        SetStatus(Status.fall);
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
