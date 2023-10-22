using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitialise : MonoBehaviour
{
    public event Action eventCameraOnPosition;

    [SerializeField] private ScreenMarker screenMarker;
    [SerializeField] private float speed;

    private Coroutine moveRoutine;

    private void Start()
    {
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
        eventCameraOnPosition?.Invoke();
        StopCoroutine(moveRoutine);
        Destroy(screenMarker);
        Destroy(this);
        
    }
}
