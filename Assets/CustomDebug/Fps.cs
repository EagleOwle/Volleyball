using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    const string display = "{0} fps";
    const float fpsMeasurePeriod = 1;

    private int fpsAccumulator = 0;
    private float fpsNextPeriod = 0;
    private int currentFps;

    [SerializeField] private Text text;

    public async void Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

        while (true)
        {
            fpsAccumulator++;

            await Task.Yield();

            if (Time.realtimeSinceStartup > fpsNextPeriod)
            {
                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
                fpsAccumulator = 0;
                fpsNextPeriod += fpsMeasurePeriod;
                text.text = string.Format(display, currentFps);
            }


#if UNITY_EDITOR
            if (EditorApplication.isPlaying == false)
            {
                break;
            }
#endif
        }
    }

}
