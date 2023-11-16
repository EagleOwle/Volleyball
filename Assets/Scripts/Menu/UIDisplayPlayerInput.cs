using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayPlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerSide player;
    [SerializeField] private Text playerNameText, keyMoveText, keyJumpText;
    [SerializeField] private GameObject keyboardInfoPlayerPanel, mouseInfoPlayerPanel;

    private PlayerPreference playerPreference;

    public void OnEnable()
    {
        var scene = ScenePreference.Singleton.GameScene;
        int value = (int)player - 1;

        playerPreference = Preference.Singleton.player[value];

        keyMoveText.text = playerPreference.leftKey.key.ToString() + "   " + playerPreference.rightKey.key.ToString();
        keyJumpText.text = playerPreference.jumpKey.key.ToString();

        if (scene.playersType[value] != PlayerType.Human)
        {
            keyboardInfoPlayerPanel.SetActive(false);
            mouseInfoPlayerPanel.SetActive(false);
            playerNameText.text = "Bot";
            
        }
        else
        {
            if (Preference.Singleton.player[value].inputType == InputType.button)
            {
                keyboardInfoPlayerPanel.SetActive(true);
                mouseInfoPlayerPanel.SetActive(false);
            }
            else
            {
                keyboardInfoPlayerPanel.SetActive(false);
                mouseInfoPlayerPanel.SetActive(true);
            }

            playerNameText.text = playerPreference.Name;

        }
    }

}
