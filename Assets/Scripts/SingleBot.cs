using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : MonoBehaviour
{
    private Ball ball;
    private UnitMotion unitMotion;
    private Vector3 direction;


    private void Awake()
    {
        unitMotion = GetComponent<UnitMotion>();
        ball = GameObject.FindObjectOfType<Ball>();
    }

    private void Update()
    {
        Move();
        Jump();
        unitMotion.MoveDirection = direction;
        //direction = transform.position;
    }

    private void Move()
    {
        if(Mathf.Abs(ball.transform.position.x - transform.position.x) < 3)
        {
            direction = new Vector3(ball.transform.position.x - transform.position.x, direction.y, direction.z);
        }
        else
        {
            direction = new Vector3(3 - transform.position.x, direction.y, direction.z);
        }
    }

    private void Jump()
    {
        if (Mathf.Abs(ball.transform.position.y - transform.position.y) < 3)
        {
            direction = new Vector3(direction.x, 10, direction.z);
        }
        else
        {
            direction = new Vector3(3 - transform.position.x, direction.y, direction.z);
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TrajectoryCalculate.HitPosition, 0.1f);

    }

}
