using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DropdownOptionPlayerType : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private LocalizeStringEvent playerTypeLocalize;
    [SerializeField] private LocalizedString stringHuman;
    [SerializeField] private LocalizedString stringBot;

    private void OnEnable()
    {
        dropdown.onValueChanged.AddListener(OnValueChange);
        Invoke(nameof(UpdateText), Time.deltaTime);
    }

    public void OnValueChange(int value)
    {
        Invoke(nameof(UpdateText), Time.deltaTime);
    }

    public void UpdateText()
    {
        if (dropdown.value == 0)
        {
            playerTypeLocalize.StringReference = stringHuman;
        }

        if (dropdown.value == 1)
        {
            playerTypeLocalize.StringReference = stringBot;
        }
    }
}
