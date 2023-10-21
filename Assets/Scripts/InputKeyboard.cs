using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour
{
    private void Update()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            InputHandler.Instance.OnButtonRightDown();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            InputHandler.Instance.OnButtonRightUp();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            InputHandler.Instance.OnButtonLeftDown();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            InputHandler.Instance.OnButtonLeftUp();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            InputHandler.Instance.OnButtonJumpDown();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            InputHandler.Instance.OnButtonJumpUp();
        }

    }
}
