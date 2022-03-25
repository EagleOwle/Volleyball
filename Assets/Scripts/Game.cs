using System;
using UnityEngine;
using UnityEngine.Events;

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

    public EventChangeStatus eventChangeStatus = new EventChangeStatus();

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
            default:
                _currentStatus = Status.game;
                break;
        }

        eventChangeStatus.Invoke(_currentStatus);
    }

    [Serializable]
    public class EventChangeStatus : UnityEvent<Status>
    {
    }
}
