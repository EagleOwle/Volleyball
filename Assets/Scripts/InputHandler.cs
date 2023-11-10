using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InputType { button, joystick }


public class InputHandler : MonoBehaviour
{
    public enum InputButton { left, right, jump}
    public enum InputDirection { up, down }

    public static InputHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InputHandler>();
            }

            return instance;
        }
    }
    private static InputHandler instance;

    public Action<Vector2> ActionSetInputDirectionForPlayer0;
    public Action<Vector2> ActionSetInputDirectionForPlayer1;
    public Action<InputButton, InputDirection> ActionOnInputForPlayer0;
    public Action<InputButton, InputDirection> ActionOnInputForPlayer1;

    public void OnInputDirection(Vector2 value, int playerIndex)
    {
        if (StateMachine.currentState is GameState)
        {
            if (playerIndex == 0)
            {
                ActionSetInputDirectionForPlayer0?.Invoke(value);
            }

            if (playerIndex == 1)
            {
                ActionSetInputDirectionForPlayer1?.Invoke(value);
            }
        }
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

    

}
