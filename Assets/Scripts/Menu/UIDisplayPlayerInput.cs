using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayPlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerSide player;
    [SerializeField] private Text keyMoveText, keyJumpText;
    [SerializeField] private GameObject keyboardInfoPlayerPanel, mouseInfoPlayerPanel;

    public void OnEnable()
    {
        var scene = ScenePreference.Singleton.GameScene;
        int value = (int)player - 1;

        keyMoveText.text = Preference.Singleton.player[value].leftKey.key.ToString() + "   " + Preference.Singleton.player[value].rightKey.key.ToString();
        keyJumpText.text = Preference.Singleton.player[value].jumpKey.key.ToString();

        if (scene.playersType[value] != PlayerType.Human)
        {
            keyboardInfoPlayerPanel.SetActive(false);
            mouseInfoPlayerPanel.SetActive(false);
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
        }
    }

}
