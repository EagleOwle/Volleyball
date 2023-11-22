using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    public static UIHud Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIHud>();
            }

            return instance;
        }
    }
    private static UIHud instance;

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

        OnChangePanel(UIPanelName.Main);
    }

}
