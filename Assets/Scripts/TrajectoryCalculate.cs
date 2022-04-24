using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrajectoryCalculate
{
    private static Vector3 hitPosition;
    public static Vector3 HitPosition => hitPosition;

    private static List<Vector3> points;

    public static Vector3[] Calculate(Vector3 origin, Vector3 speed, LayerMask collisionMask)
    {
        hitPosition = Vector3.zero;

        if (points == null)
        {
            points = new List<Vector3>();
        }
        else
        {
            points.Clear();
        }

        Vector3 lastPoint = origin;

        for (int i = 0; i < 100; i++)
        {
            float time = i * 0.1f;

            Vector3 nextPoint = origin + speed * time + Physics.gravity * time * time / 2f;
            points.Add(nextPoint);

            if (CheckNextPointCollision(lastPoint, nextPoint, collisionMask))
            {
                hitPosition = lastPoint;
                break;
            }

            lastPoint = nextPoint;
        }

        return points.ToArray();
    }

    private static bool CheckNextPointCollision(Vector3 startPosition, Vector3 targetPosition, LayerMask collisionMask)
    {
        RaycastHit hit;

        float distance = (targetPosition - startPosition).magnitude;

        if (Physics.Raycast(startPosition, targetPosition - startPosition, out hit, distance, collisionMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
