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

    private PlayerPreference playerPreference;

    private void Awake()
    {
        editKeyLeft.onClick.AddListener(OnButtonChangeKeyLeft);
        editKeyRight.onClick.AddListener(OnButtonChangeKeyRight);
        editKeyJump.onClick.AddListener(OnButtonChangeKeyJump);
        playerPreference = Preference.Singleton.player[playerId];
    }

    private void OnEnable()
    {
        keyChecker.eventHide += KeyChecker_eventHide;
        KeyChecker_eventHide();
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
