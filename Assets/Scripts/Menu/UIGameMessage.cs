using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIGameMessage : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private LocalizeStringEvent stringEvent;

    private void Start()
    {
        ClearMessage();
    }
    
    public void ShowLocalizedMessage(LocalizedString local, string playerName, float liveTime = 3)
    {
        CancelInvoke();
        ClearMessage();
        stringEvent.StringReference = local;
        text.text += " " + playerName;
        Invoke(nameof(ClearMessage), liveTime);
    }

    public void ShowLocalizedMessage(LocalizedString local, float liveTime = 3)
    {
        CancelInvoke();
        ClearMessage();
        stringEvent.StringReference = local;
        Invoke(nameof(ClearMessage), liveTime);
    }

    private void ClearMessage()
    {
        text.text = "";
    }
}
