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
        //hitPosition = Vector3.zero;

        //Vector3[] points = new Vector3[100];
        //lineRenderer.positionCount = points.Length;

        //for (int i = 0; i < points.Length; i++)
        //{
        //    float time = i * 0.1f;

        //    points[i] = origin + speed * time + Physics.gravity * time * time / 2f;

        //    if (CheckSphereCollision(points[i], 0.1f))
        //    {
        //        lineRenderer.positionCount = i + 1;
        //        hitPosition = points[i];
        //        break;
        //    }

        //    if (points[i].y < 0)
        //    {
        //        lineRenderer.positionCount = i + 1;
        //        break;
        //    }
        //}

        //lineRenderer.SetPositions(points);
    }

    //void OnDrawGizmosSelected()
    //{

    //    if (hitPosition != Vector3.zero)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(hitPosition, 0.1f);
    //    }
    //}

}
