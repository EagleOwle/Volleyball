using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderAdvanced : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;


            if (points[i].y < 0)
            {
                lineRenderer.positionCount = i + 1;
                break;
            }
        }

        lineRenderer.SetPositions(points);
    }

    public async void TrajectoryCalculation(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        int i = points.Length;

        while (i > 0)
        {
            await Task.Delay(100);

            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;

            i++;

        }
    }


}
