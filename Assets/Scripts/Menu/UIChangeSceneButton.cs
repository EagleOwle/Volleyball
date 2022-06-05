using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeSceneButton : MonoBehaviour
{
    public Sprite defaultSprite, activeSprite;
    public Image buttonImage;

    public bool SetActiveSprite
    {
        set
        {
            if(value == true)
            {
                buttonImage.sprite = activeSprite;
            }
            else
            {
                buttonImage.sprite = defaultSprite;
            }
        }
    }

}
