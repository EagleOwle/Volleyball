using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DropdownItem : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private LocalizeStringEvent nameSceneLocalize;
    [SerializeField] private LocalizedString stringEasy;
    [SerializeField] private LocalizedString stringNormal;
    [SerializeField] private LocalizedString stringHard;
    private void OnEnable()
    {
        Invoke(nameof(ShowText), Time.deltaTime);
    }

    private void ShowText()
    {
        if(text.text == ScenePreference.GameDifficult.Easy.ToString())
        {
            nameSceneLocalize.StringReference = stringEasy;
        }

        if (text.text == ScenePreference.GameDifficult.Normal.ToString())
        {
            nameSceneLocalize.StringReference = stringNormal;
        }

        if (text.text == ScenePreference.GameDifficult.Hard.ToString())
        {
            nameSceneLocalize.StringReference = stringHard;
        }
    }
}
