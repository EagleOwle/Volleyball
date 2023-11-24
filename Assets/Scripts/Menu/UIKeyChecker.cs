using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyChecker : MonoBehaviour
{
    public event Action eventHide;
    [SerializeField] GameObject erroreMessageText;

    private PlayerInput targetInput;
    private PlayerPreference playerPreference;

    public void Show(PlayerInput targetInput, PlayerPreference playerPreference)
    {
        this.targetInput = targetInput;
        this.playerPreference = playerPreference;
        erroreMessageText.SetActive(false);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var item in Preference.Singleton.AllowedKeys)
            {
                if (Input.GetKeyDown(item))
                {
                    targetInput.WriteKey(item);
                    MatchChecking(targetInput);
                    HideSelf();
                    CancelInvoke();
                    return;
                }
            }

            CancelInvoke();
            erroreMessageText.SetActive(true);
            Invoke(nameof(HideErroreMessage), 1);
        }
    }

    private void MatchChecking(PlayerInput targetInput)
    {
        foreach (var pref in Preference.Singleton.player)
        {
            if (pref.jumpKey != targetInput)
            {
                if (pref.jumpKey.Key == targetInput.Key)
                {
                    pref.jumpKey.WriteKey(KeyCode.None);
                }
            }

            if (pref.leftKey != targetInput)
            {
                if (pref.leftKey.Key == targetInput.Key)
                {
                    pref.leftKey.WriteKey(KeyCode.None);
                }
            }

            if (pref.rightKey != targetInput)
            {
                if (pref.rightKey.Key == targetInput.Key)
                {
                    pref.rightKey.WriteKey(KeyCode.None);
                }
            }
        }
    }

    private void HideErroreMessage()
    {
        CancelInvoke();
        erroreMessageText.SetActive(false);
    }

    private void HideSelf()
    {
        CancelInvoke();
        eventHide?.Invoke();
        gameObject.SetActive(false);
    }
}
