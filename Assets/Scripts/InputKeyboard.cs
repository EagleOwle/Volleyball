using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour
{
    private void Update()
    {
        foreach (var item in Preference.Singleton.player)
        {
            #region Left
            if (Input.GetKey(item.leftKey.key))
            {
                InputHandler.Instance.OnButtonLeftDown();
            }

            if (Input.GetKeyUp(item.leftKey.key))
            {
                InputHandler.Instance.OnButtonLeftUp();
            }
            #endregion

            #region Right
            if (Input.GetKey(item.rightKey.key))
            {
                InputHandler.Instance.OnButtonRightDown();
            }

            if (Input.GetKeyUp(item.rightKey.key))
            {
                InputHandler.Instance.OnButtonRightUp();
            }
            #endregion

            #region Jump
            if (Input.GetKey(item.jumpKey.key))
            {
                InputHandler.Instance.OnButtonJumpDown();
            }

            if (Input.GetKeyUp(item.jumpKey.key))
            {
                InputHandler.Instance.OnButtonJumpUp();
            }
            #endregion

        }
    }


}
