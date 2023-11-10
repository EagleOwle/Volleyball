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
                if (Input.GetKeyDown(item) && MatchChecking(item))
                {
                    Debug.Log("Key " + item + " is allowed");
                    targetInput.key = item;
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

    private bool MatchChecking(KeyCode key)
    {
        if (playerPreference.jumpKey.key == key)
            return false;

        if (playerPreference.leftKey.key == key)
            return false;

        if (playerPreference.rightKey.key == key)
            return false;

        return true;
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
