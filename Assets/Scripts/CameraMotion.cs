using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public event Action eventCameraEndMotion;
    private const float speed = 5;
    private Coroutine moveRoutine;
    private CameraViewMarker screenMarker;

    private void Start()
    {
        screenMarker = GameObject.FindAnyObjectByType<CameraViewMarker>();

        if (screenMarker == null)
        {
            Debug.LogFormat("ScreenMarker not found");
            eventCameraEndMotion?.Invoke();
            return;
        }

        screenMarker.eventOnVisible += ScreenMarker_eventOnVisible;
        var position = transform.position;
        position.z = 0;
        transform.position = position;
        moveRoutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while(true)
        {
            if(transform.position.z < -100)
            {
                Debug.LogError("Position to far");
                break;
            }

            var position = transform.position;
            position.z = Mathf.Lerp(position.z, position.z -1, speed * Time.deltaTime);
            transform.position = position;
            yield return null;
        }
    }

    private void ScreenMarker_eventOnVisible()
    {
        Debug.Log("Camera On Position");
        eventCameraEndMotion?.Invoke();
        StopCoroutine(moveRoutine);
        Destroy(screenMarker.gameObject);
        Destroy(this);
        
    }
}
