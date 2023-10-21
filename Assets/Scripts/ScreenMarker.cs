using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMarker : MonoBehaviour
{
    public event Action eventOnVisible;

    private void OnBecameVisible()
    {
        eventOnVisible?.Invoke();
    }
}
