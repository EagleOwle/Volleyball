using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DropdownItemPlayerType : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private LocalizeStringEvent playerTypeLocalize;
    [SerializeField] private LocalizedString stringHuman;
    [SerializeField] private LocalizedString stringBot;

    private void OnEnable()
    {
        Invoke(nameof(UpdateText), Time.deltaTime);
    }

    public void UpdateText()
    {
        if (text.text == "Human")
        {
            playerTypeLocalize.StringReference = stringHuman;
        }

        if (text.text == "Bot")
        {
            playerTypeLocalize.StringReference = stringBot;
        }
    }
}
