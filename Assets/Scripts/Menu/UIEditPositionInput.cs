using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditPositionInput : MonoBehaviour
{
    [SerializeField] private Button returnButton;

    private void Start()
    {
        returnButton.onClick.AddListener(OnButtonReturn);
    }

    private void OnButtonReturn()
    {
        gameObject.SetActive(false);
    }

}
