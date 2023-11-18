using UnityEngine;

public class MediumQualityVisualEffect : MonoBehaviour
{
    public ParticleSystem particle;
    public float hSliderValueRatio = 1.0f;
    public float hSliderValueMax = 10.0f;
    public bool randomDistribution = true;

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

    private void Update()
    {
        var lights = particle.lights;
        lights.ratio = hSliderValueRatio;
        lights.maxLights = (int)hSliderValueMax;
        lights.useRandomDistribution = randomDistribution;
    }

}