using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InputType { button, joystick }


public class InputHandler : MonoBehaviour
{
    public enum InputButton { left, right, jump}
    public enum InputDirection { up, down }
    
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
    public Action<InputButton, InputDirection> ActionOnInputForPlayer0;
    public Action<InputButton, InputDirection> ActionOnInputForPlayer1;

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

            percent.x = Screen.width / 100 * Preference.Singleton.player[0].InputDeadZone;
            percent.y = Screen.height / 100 * Preference.Singleton.player[0].InputDeadZone;

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

    private void OnInputButton(InputButton button, InputDirection type, int playerIndex)
    {
        if (StateMachine.currentState is GameState)
        {
            if (playerIndex == 0)
            {
                ActionOnInputForPlayer0?.Invoke(button, type);
            }

            if (playerIndex == 1)
            {
                ActionOnInputForPlayer1?.Invoke(button, type);
            }
        }
    }

    public void OnJoysticInput(Vector2 value)
    {
        if (Mathf.Abs(value.x) < Preference.Singleton.player[0].InputDeadZone)
        {
            value.x = 0;
        }
        if (Mathf.Abs(value.y) < Preference.Singleton.player[0].InputDeadZone)
        {
            value.y = 0;
        }

        SwipeDirection = value;

    }

    public void OnButtonJumpDown(int playerIndex)
    {
        OnInputButton(InputButton.jump, InputDirection.down, playerIndex);
    }

    public void OnButtonJumpUp(int playerIndex)
    {
        OnInputButton(InputButton.jump, InputDirection.up, playerIndex);
    }

    public void OnButtonLeftDown(int playerIndex)
    {
        OnInputButton(InputButton.left, InputDirection.down, playerIndex);
    }

    public void OnButtonLeftUp(int playerIndex)
    {
        OnInputButton(InputButton.left, InputDirection.up, playerIndex);
    }

    public void OnButtonRightDown(int playerIndex)
    {
        OnInputButton(InputButton.right, InputDirection.down, playerIndex);
    }

    public void OnButtonRightUp(int playerIndex)
    {
        OnInputButton(InputButton.right, InputDirection.up, playerIndex);
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
