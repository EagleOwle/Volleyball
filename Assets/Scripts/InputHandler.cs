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

    [Range(0,100)]
    [SerializeField] private float sence = 10;

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
                swipeDirection = value;
                ActionSetSwipeDirection?.Invoke(swipeDirection);
            }
        }
    }

    public Action<Vector3> ActionSetSwipeDirection;

    private Vector2 percent;

    private void Update()
    {
        if (StateMachine.currentState is GameState)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _tupPosition = (Vector2)Input.mousePosition;
                InvokeRepeating(nameof(SetTupPosition), 0.1f, 0.1f);
                _onSwipe = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                SwipeBreak();
            }

            if (_onSwipe)
            {
                Vector3 direction = (Input.mousePosition - (Vector3)_tupPosition);

                percent.x = Screen.width / 100 * sence;
                percent.y = Screen.height / 100 * sence;

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
        _onSwipe = false;
        SwipeDirection = Vector3.zero;
        _tupPosition = Vector3.zero;
        CancelInvoke();
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

    void SetTupPosition()
    {
        _tupPosition = (Vector2)Input.mousePosition;
    }

}
