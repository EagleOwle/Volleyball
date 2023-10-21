using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitialise : MonoBehaviour
{
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
            position.z -= speed * Time.deltaTime;
            transform.position = position;
            yield return null;
        }
    }

    private void ScreenMarker_eventOnVisible()
    {
        StopCoroutine(moveRoutine);
        
    }
}
