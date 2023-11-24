using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < Preference.Singleton.player.Length; i++)
        {
            #region Left
            if (Input.GetKey(Preference.Singleton.player[i].leftKey.Key))
            {
                InputHandler.Instance.OnButtonLeftDown(i);
            }

            if (Input.GetKeyUp(Preference.Singleton.player[i].leftKey.Key))
            {
                InputHandler.Instance.OnButtonLeftUp(i);
            }
            #endregion

            #region Right
            if (Input.GetKey(Preference.Singleton.player[i].rightKey.Key))
            {
                InputHandler.Instance.OnButtonRightDown(i);
            }

            if (Input.GetKeyUp(Preference.Singleton.player[i].rightKey.Key))
            {
                InputHandler.Instance.OnButtonRightUp(i);
            }
            #endregion

            #region Jump
            if (Input.GetKey(Preference.Singleton.player[i].jumpKey.Key))
            {
                InputHandler.Instance.OnButtonJumpDown(i);
            }

            if (Input.GetKeyUp(Preference.Singleton.player[i].jumpKey.Key))
            {
                InputHandler.Instance.OnButtonJumpUp(i);
            }
            #endregion

        }
    }


}
