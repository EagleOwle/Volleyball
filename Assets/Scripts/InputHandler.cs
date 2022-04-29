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

    public Action<Vector3> ActionSetSwipeDirection;

    private bool onSwipe;
    private Vector2 tupPosition;
    private Vector3 swipeDirection;
    private Vector3 SwipeDirection
    {
        set
        {
            if (swipeDirection != value)
            {
                swipeDirection = value;
                ActionSetSwipeDirection?.Invoke(swipeDirection);
            }
        }
    }
    private Vector2 percent;

    private void Update()
    {
        if (StateMachine.currentState is GameState)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tupPosition = (Vector2)Input.mousePosition;
                InvokeRepeating(nameof(SetTupPosition), 0.1f, 0.1f);
                onSwipe = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                SwipeBreak();
            }

            if (onSwipe)
            {
                Vector3 direction = (Input.mousePosition - (Vector3)tupPosition);

                percent.x = Screen.width / 100 * Preference.Singleton.InputDeadZone;
                percent.y = Screen.height / 100 * Preference.Singleton.InputDeadZone;

                if (Mathf.Abs(direction.x) < percent.x)
                {
                    direction.x = 0;
                }

                if (Mathf.Abs(direction.y) < percent.y)
                {
                    direction.y = 0;
                }

                SwipeDirection = direction;
            }
        }
        else
        {
            SwipeBreak();
        }

    }

    private void SwipeBreak()
    {
        onSwipe = false;
        SwipeDirection = Vector3.zero;
        tupPosition = Vector3.zero;
        CancelInvoke();
    }

    private void OnDrawGizmos()
    {
        if (onSwipe)
        {
            Gizmos.color = Color.blue;

            Vector3 startPosition = tupPosition;
            startPosition.z = Camera.main.nearClipPlane;

            Vector3 endPosition = tupPosition + (Vector2)swipeDirection;
            endPosition.z = Camera.main.nearClipPlane;

            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(startPosition), Camera.main.ScreenToWorldPoint(endPosition));
        }
    }

    void SetTupPosition()
    {
        tupPosition = (Vector2)Input.mousePosition;
    }

}
