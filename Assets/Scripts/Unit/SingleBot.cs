using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : Unit
{
    private Ball ball;
    private UnitMotion unitMotion;
    private Vector3 direction;

    private float defaultPositionX;

    private void Awake()
    {
        unitMotion = GetComponent<UnitMotion>();
        ball = GameObject.FindObjectOfType<Ball>();
    }

    private void Start()
    {
        defaultPositionX = transform.position.x;
    }

    private void Update()
    {
        Move();
        unitMotion.MoveDirection = direction.normalized;

    }

    private void Move()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryCalculate.HitPosition.x > 0)
        {
            float dir = TrajectoryCalculate.HitPosition.x - transform.position.x;

            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            Jump();
        }
        else
        {
            float dir = defaultPositionX - transform.position.x;
            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, 0, direction.z);
            }
        }
    }

    private void Jump()
    {
        if (ball.transform.position.y > transform.position.y + 2)
        {
            if (Mathf.Abs(ball.transform.position.y - transform.position.y) < 3)
            {
                direction = new Vector3(direction.x, 1, direction.z);
            }
            else
            {
                direction = new Vector3(direction.x, 0, direction.z);
            }
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TrajectoryCalculate.HitPosition, 0.1f);

    }

}
