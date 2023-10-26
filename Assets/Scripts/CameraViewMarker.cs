using System;
using UnityEngine;

public class CameraViewMarker : MonoBehaviour
{
    public event Action eventOnVisible;
    [SerializeField] private Collider collider;

    private void Update()
    {
        Bounds bounds = GetComponentInChildren<Collider>().bounds;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, bounds))
        {
            eventOnVisible?.Invoke();
        }

    }
}
