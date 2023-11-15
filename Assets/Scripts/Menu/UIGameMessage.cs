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
    [SerializeField] private float liveTime;
    [SerializeField] private LocalizeStringEvent stringEvent;

    private float time;
    private List<string> messageQueue = new List<string>();
    private Coroutine coroutine;

    private void Start()
    {
        ClearMessage();
    }

    public void Show(string message)
    {
        messageQueue.Add(message);
        if (coroutine == null)
        {
            coroutine = StartCoroutine(ShowMessage());
        }
    }
    
    public void ShowLocalizedMessage(LocalizedString local)
    {
        StopAllCoroutines();
        ClearMessage();
        stringEvent.StringReference = local;
        Invoke(nameof(ClearMessage), liveTime);
    }

    private IEnumerator ShowMessage()
    {
        while (messageQueue.Count > 0)
        {
            text.text = messageQueue[0];
            messageQueue.RemoveAt(0);
            yield return new WaitForSeconds(liveTime);
        }
        ClearMessage();
        coroutine = null;
    }

    private void ClearMessage()
    {
        text.text = "";
    }
}
