using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRender : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = TrajectoryCalculate.Calculate(origin, speed, collisionMask);
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

}
