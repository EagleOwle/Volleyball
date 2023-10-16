using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighQualityVisualEffect : MonoBehaviour
{
    private void Awake()
    {
        // if (useTargetFramerate) Application.targetFrameRate = targetFrameRate;
        int mediumqualityLevel = QualitySettings.GetQualityLevel();

        if (mediumqualityLevel == 0)
        {
            gameObject.SetActive(false);
        }
        if (mediumqualityLevel == 1)
        {
            gameObject.SetActive(false);
        }
        if (mediumqualityLevel == 2)
        {
            gameObject.SetActive(true);
        }
    
    }
}
