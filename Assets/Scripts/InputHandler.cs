using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public enum InputButton { left, right, jump}
    public enum InputType { up, down }
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
    public Action<InputButton, InputType> ActionOnInputButton;

    [SerializeField] private LayerMask uiButtonMask;

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
#if UNITY_EDITOR
        KeyboardInput();
#elif UNITY_ANDROID

#elif UNITY_IOS
                 
#elif UNITY_STANDALONE_OSX
                 
#elif UNITY_STANDALONE_WIN

#endif


        //if (StateMachine.currentState is GameState)
        //{
        //    //GetSwipe();
        //}
        //else
        //{
        //    //SwipeBreak();
        //}

    }

    private void KeyboardInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnButtonRightDown();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnButtonRightUp();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnButtonLeftDown();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnButtonLeftUp();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnButtonJumpDown();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnButtonJumpUp();
        }

    }

    private void GetSwipe()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GetUIObject(Input.mousePosition) == null)
            {
                tupPosition = (Vector2)Input.mousePosition;
                InvokeRepeating(nameof(SetTupPosition), 0.1f, 0.1f);
                onSwipe = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SwipeBreak();
        }

        if (onSwipe)
        {
            Vector3 direction = (Input.mousePosition - (Vector3)tupPosition);

            direction.x = Mathf.Clamp(direction.x, -100, 100);
            direction.y = Mathf.Clamp(direction.y, -100, 100);

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

    private void SetTupPosition()
    {
        tupPosition = (Vector2)Input.mousePosition;
    }

    private void OnInputButton(InputButton button, InputType type)
    {
        if (StateMachine.currentState is GameState)
        {
            ActionOnInputButton?.Invoke(button, type);
        }
    }

    public void OnButtonJumpDown()
    {
        OnInputButton(InputButton.jump, InputType.down);
    }

    public void OnButtonJumpUp()
    {
        OnInputButton(InputButton.jump, InputType.up);
    }

    public void OnButtonLeftDown()
    {
        OnInputButton(InputButton.left, InputType.down);
    }

    public void OnButtonLeftUp()
    {
        OnInputButton(InputButton.left, InputType.up);
    }

    public void OnButtonRightDown()
    {
        OnInputButton(InputButton.right, InputType.down);
    }

    public void OnButtonRightUp()
    {
        OnInputButton(InputButton.right, InputType.up);
    }

    private GameObject GetUIObject(Vector3 touchScreenPositin)
    {
        GameObject touchObject = null;

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchScreenPositin);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchWorldPosition, Camera.main.transform.forward, 100, uiButtonMask);

        if (hitInformation.collider != null)
        {
            //actionOnTouchObject.Invoke(hitInformation.transform.gameObject, TouchPhase.Began);
            touchObject = hitInformation.transform.gameObject;
        }

        return touchObject;
    }

}
