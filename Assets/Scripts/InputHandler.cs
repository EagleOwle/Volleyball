using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance;
    public static InputHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InputHandler>();
            }

            return _instance;
        }
    }

    [SerializeField] private float sence = 0.005f;

    private bool _onSwipe;
    public bool OnSwipe => _onSwipe;
    private Vector2 _tupPosition;
    private Vector3 swipeDirection;
    public Vector3 SwipeDirection
    {
        set
        {
            if (swipeDirection != value)
            {
                swipeDirection = value * sence;
                ActionSetSwipeDirection?.Invoke(swipeDirection);
            }

        }
    }

    public Action<Vector3> ActionSetSwipeDirection;

    private void Update()
    {
        if (StateMachine.currentState is GameState)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _tupPosition = (Vector2)Input.mousePosition;
                SwipeDirection = _tupPosition;
                _onSwipe = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                SwipeBreak();
            }

            if (_onSwipe)
            {
                SwipeDirection = ((Vector2)Input.mousePosition - _tupPosition);
            }
        }
        else
        {
            SwipeBreak();
        }

    }

    private void SwipeBreak()
    {
        _onSwipe = false;
        SwipeDirection = Vector2.zero;
        _tupPosition = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (_onSwipe)
        {
            Gizmos.color = Color.blue;

            Vector3 startPosition = _tupPosition;
            startPosition.z = Camera.main.nearClipPlane;

            Vector3 endPosition = _tupPosition + (Vector2)swipeDirection;
            endPosition.z = Camera.main.nearClipPlane;

            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(startPosition), Camera.main.ScreenToWorldPoint(endPosition));
        }
    }

}
