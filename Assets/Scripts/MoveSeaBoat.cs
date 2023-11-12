using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSeaBoat : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float leftLimit = -5.0f;
    [SerializeField] private float rightLimit = 5.0f;

    [SerializeField] private bool movingRight = true;

    private void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
}
