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
            consolTransform.sizeDelta = new Vector2(consolTransform.sizeDelta.x, 0);
            buttonImageTransform.eulerAngles = new Vector3(0, 0, 180);
        }
    }

}
