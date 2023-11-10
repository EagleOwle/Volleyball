using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditPlayerInput : MonoBehaviour
{
    [SerializeField] private int playerId;
    [SerializeField] private UIKeyChecker keyChecker;
    [SerializeField] private Button editKeyLeft, editKeyRight, editKeyJump;
    [SerializeField] private Text currentKeyLeft, currentKeyRight, currentKeyJump;
    [SerializeField] private Slider inputTypeSlider, senceMouseMoveSlider, senceMouseJumpSlider;
    [SerializeField] private GameObject editInputKeyPanel, editInputMousePanel;

    private PlayerPreference playerPreference;

    private void Awake()
    {
        playerPreference = Preference.Singleton.player[playerId];

        editKeyLeft.onClick.AddListener(OnButtonChangeKeyLeft);
        editKeyRight.onClick.AddListener(OnButtonChangeKeyRight);
        editKeyJump.onClick.AddListener(OnButtonChangeKeyJump);

        inputTypeSlider.onValueChanged.AddListener(ChangeEditInputPanel);

        senceMouseMoveSlider.onValueChanged.AddListener(OnChangeMouseMoveSence);

        senceMouseJumpSlider.onValueChanged.AddListener(OnChangeMouseJumpSence);
    }

    private void OnEnable()
    {
        keyChecker.eventHide += KeyChecker_eventHide;
        KeyChecker_eventHide();

        inputTypeSlider.SetValueWithoutNotify((float)playerPreference.inputType);
        ChangeEditInputPanel((float)playerPreference.inputType);

        senceMouseMoveSlider.SetValueWithoutNotify(playerPreference.mouseMoveSence);
        OnChangeMouseMoveSence(playerPreference.mouseMoveSence);

        senceMouseJumpSlider.SetValueWithoutNotify(playerPreference.mouseJumpSence);
        OnChangeMouseJumpSence(playerPreference.mouseJumpSence);
    }

    private void OnChangeMouseMoveSence(float value)
    {
        playerPreference.mouseMoveSence = value;
    }

    private void OnChangeMouseJumpSence(float value)
    {
        playerPreference.mouseJumpSence = value;
    }

    private void ChangeEditInputPanel(float value)
    {
        Debug.Log("ChangeEditInputPanel: " + value);
        switch (value)
        {
            case 0:
                editInputKeyPanel.SetActive(true);
                editInputMousePanel.SetActive(false);
                playerPreference.inputType = InputType.button;
                break;

            case 1:
                editInputKeyPanel.SetActive(false);
                editInputMousePanel.SetActive(true);
                playerPreference.inputType = InputType.joystick;
                break;
        }
    }

    private void UpdateText()
    {
        currentKeyLeft.text = playerPreference.leftKey.key.ToString();
        currentKeyRight.text = playerPreference.rightKey.key.ToString();
        currentKeyJump.text = playerPreference.jumpKey.key.ToString();
    }

    private void OnButtonChangeKeyLeft()
    {
        
        keyChecker.Show(playerPreference.leftKey, playerPreference);

    }

    private void KeyChecker_eventHide()
    {
        UpdateText();
    }

    private void OnButtonChangeKeyRight()
    {
        keyChecker.Show(playerPreference.rightKey, playerPreference);

    }

    private void OnButtonChangeKeyJump()
    {
        keyChecker.Show(playerPreference.jumpKey, playerPreference);

    }

    private void OnDisable()
    {
        keyChecker.eventHide -= KeyChecker_eventHide;
    }

    private void OnDestroy()
    {
        keyChecker.eventHide -= KeyChecker_eventHide;
    }

}
