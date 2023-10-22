using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMarker : MonoBehaviour
{
    public event Action eventOnVisible;

    private void Update()
    {
        Bounds bounds = GetComponentInChildren<Renderer>().bounds;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, bounds))
        {
            eventOnVisible?.Invoke();
        }

    }

    //private void OnBecameVisible()
    //{
    //    eventOnVisible?.Invoke();
    //}
}
