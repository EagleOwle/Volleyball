using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum Status { game, pause }

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

    public Action<Status> OnChangeMenu;

    public void RechangeStatus()
    {
        switch (status)
        {
            case Status.game:
                _currentStatus = Status.pause;
                break;
            case Status.pause:
                _currentStatus = Status.game;
                break;
        }

        OnChangeMenu.Invoke(_currentStatus);
    }

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
        }

        OnChangeMenu.Invoke(_currentStatus);

    }

}
