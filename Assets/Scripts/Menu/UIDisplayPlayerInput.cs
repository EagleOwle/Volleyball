using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayPlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerSide player;
    [SerializeField] private Text keyMoveText, keyJumpText;

    private void OnEnable()
    {
        keyMoveText.text = Preference.Singleton.player[(int)player].leftKey.key.ToString() + "   " + Preference.Singleton.player[(int)player].rightKey.key.ToString();
        keyJumpText.text = Preference.Singleton.player[(int)player].jumpKey.key.ToString();
    }

}
