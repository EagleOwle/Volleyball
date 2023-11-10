using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour
{
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private void Start()
    {
        jumpButton.onClick.AddListener(OnButtonJumpDown);
        jumpButton.onClick.AddListener(OnButtonJumpUp);

        leftButton.onClick.AddListener(OnButtonLeftDown);
        leftButton.onClick.AddListener(OnButtonLeftUp);

        rightButton.onClick.AddListener(OnButtonRightDown);
        rightButton.onClick.AddListener(OnButtonRightUp);

    }

    public void OnButtonJumpDown(int playerIndex)
    {
        InputHandler.Instance.OnButtonJumpDown(playerIndex);
    }

    public void OnButtonJumpDown()
    {
        InputHandler.Instance.OnButtonJumpDown(0);
    }

    public void OnButtonJumpUp(int playerIndex)
    {
        InputHandler.Instance.OnButtonJumpUp(playerIndex);
    }

    public void OnButtonJumpUp()
    {
        InputHandler.Instance.OnButtonJumpUp(0);
    }

    public void OnButtonLeftDown(int playerIndex)
    {
        InputHandler.Instance.OnButtonLeftDown(playerIndex);
    }

    public void OnButtonLeftDown()
    {
        InputHandler.Instance.OnButtonLeftDown(0);
    }

    public void OnButtonLeftUp(int playerIndex)
    {
        InputHandler.Instance.OnButtonLeftUp(playerIndex);
    }

    public void OnButtonLeftUp()
    {
        InputHandler.Instance.OnButtonLeftUp(0);
    }

    public void OnButtonRightDown(int playerIndex)
    {
        InputHandler.Instance.OnButtonRightDown(playerIndex);
    }

    public void OnButtonRightDown()
    {
        InputHandler.Instance.OnButtonRightDown(0);
    }

    public void OnButtonRightUp(int playerIndex)
    {
        InputHandler.Instance.OnButtonRightUp(playerIndex);
    }

    public void OnButtonRightUp()
    {
        InputHandler.Instance.OnButtonRightUp(0);
    }
}
