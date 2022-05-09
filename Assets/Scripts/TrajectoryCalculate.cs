using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrajectoryCalculate
{
    private static RaycastHit hit;
    public static RaycastHit Hit => hit;
    private static List<Vector3> points;

    public static Vector3[] Calculate(Vector3 origin, Vector3 speed, LayerMask collisionMask)
    {
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
                break;
            }

            lastPoint = nextPoint;
        }

        return points.ToArray();
    }

    private static bool CheckNextPointCollision(Vector3 startPosition, Vector3 targetPosition, LayerMask collisionMask)
    {
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
