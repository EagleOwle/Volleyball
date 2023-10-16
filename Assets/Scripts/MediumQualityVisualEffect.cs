using UnityEngine;

public class MediumQualityVisualEffect : MonoBehaviour
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
            gameObject.SetActive(true);
        }
        if (mediumqualityLevel == 2)
        {
            gameObject.SetActive(true);
        }
    }
}