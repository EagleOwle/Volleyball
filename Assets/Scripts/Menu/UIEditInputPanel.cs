using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditInputPanel : MonoBehaviour
{
    [SerializeField] private GameObject buttonEditPanel, joysticEditPanel;

    [SerializeField] private Button showEditInputButton;

    private void Start()
    {
        showEditInputButton.onClick.AddListener(OnButtonEditInputPanel);
    }

    private void OnEnable()
    {
        buttonEditPanel.SetActive(false);
        joysticEditPanel.SetActive(false);
    }

    private void OnButtonEditInputPanel()
    {
        switch (Preference.Singleton.inputType)
        {
            case InputType.button:
                ShowButtonPanel();
                break;
            case InputType.joystick:
                ShowJoysticPanel();
                break;
        }
    }

    private void ShowButtonPanel()
    {
        buttonEditPanel.SetActive(true);
        joysticEditPanel.SetActive(false);
    }


    private void ShowJoysticPanel()
    {
        buttonEditPanel.SetActive(false);
        joysticEditPanel.SetActive(true);
    }

    
}
