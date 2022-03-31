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
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InputHandler>();
            }

            return _instance;
        }
    }

    [Range(0, 0.1f)]
    [SerializeField] private float sence = 0.005f;

    private bool _onSwipe;
    public bool OnSwipe => _onSwipe;
    private Vector2 _tupPosition;
    private Vector2 _swipeDirection;
    public Vector2 SwipeDirection => _swipeDirection * sence;

    private void Update()
    {
        if (Game.Instance.status != Game.Status.game)
        {
            SwipeBreak();
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _tupPosition = (Vector2)Input.mousePosition;
            _swipeDirection = _tupPosition;
            _onSwipe = true;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            SwipeBreak();
        }


        if(_onSwipe)
        {
            _swipeDirection = ((Vector2)Input.mousePosition - _tupPosition);
        }
    }

    private void SwipeBreak()
    {
        _onSwipe = false;
        _swipeDirection = Vector2.zero;
        _tupPosition = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (_onSwipe)
        {
            Gizmos.color = Color.blue;

            Vector3 startPosition = _tupPosition;
            startPosition.z = Camera.main.nearClipPlane;

            Vector3 endPosition = _tupPosition + _swipeDirection;
            endPosition.z = Camera.main.nearClipPlane;

            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(startPosition), Camera.main.ScreenToWorldPoint(endPosition));
        }
    }

}
