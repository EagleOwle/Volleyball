using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    private static UIHud uiHud;
    public static UIHud Singletone
    {
        get
        {
            if (uiHud == null)
            {
                uiHud = GameObject.FindObjectOfType<UIHud>();
            }

            return uiHud;
        }
    }

    public Action<UIPanelName> ActionChangePanel;
    public void OnChangePanel(UIPanelName panelName)
    {
        ActionChangePanel?.Invoke(panelName);
    }

    public UIPanel[] uIPanels;

    private void Start()
    {
        foreach (var item in uIPanels)
        {
            item.Init();
        }
    }

}
