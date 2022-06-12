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

    public void OnButtonJumpDown()
    {
        InputHandler.Instance.OnButtonJumpDown();
    }

    public void OnButtonJumpUp()
    {
        InputHandler.Instance.OnButtonJumpUp();
    }

    public void OnButtonLeftDown()
    {
        InputHandler.Instance.OnButtonLeftDown();
    }

    public void OnButtonLeftUp()
    {
        InputHandler.Instance.OnButtonLeftUp();
    }

    public void OnButtonRightDown()
    {
        InputHandler.Instance.OnButtonRightDown();
    }

    public void OnButtonRightUp()
    {
        InputHandler.Instance.OnButtonRightUp();
    }

}
