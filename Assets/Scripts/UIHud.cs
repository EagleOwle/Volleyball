using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    public static Action<UIPanelName> ActionChangePanel;
    public static void OnChangePanel(UIPanelName panelName)
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
