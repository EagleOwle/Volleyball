using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDebugUI : MonoBehaviour
{
    [SerializeField] private Button hideButton, closeButton;
    [SerializeField] private RectTransform consolTransform;
    [SerializeField] private float defaultSizeY;

    private RectTransform buttonImageTransform;

    private void Awake()
    {
        buttonImageTransform = hideButton.GetComponent<RectTransform>();
    }

    private void Start()
    {
        hideButton.onClick.AddListener(HideConsole);
        closeButton.onClick.AddListener(CloseConsole);
        buttonImageTransform.eulerAngles = new Vector3(0, 0, 180);
        consolTransform.sizeDelta = new Vector2(consolTransform.sizeDelta.x, 75);
        Invoke(nameof(ShowTestMessage), 1);
    }

    private void ShowTestMessage()
    {
        Debug.Log("Debug Test");
    }

    private void CloseConsole()
    {
        gameObject.SetActive(false);
    }

    private void HideConsole()
    {
        if(consolTransform.sizeDelta.y == 0)
        {
            consolTransform.sizeDelta = new Vector2(consolTransform.sizeDelta.x, defaultSizeY);
            buttonImageTransform.eulerAngles = Vector3.zero;
        }
        else
        {
            consolTransform.sizeDelta = new Vector2(consolTransform.sizeDelta.x, 35);
            buttonImageTransform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

}
