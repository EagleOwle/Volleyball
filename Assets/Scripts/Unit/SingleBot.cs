using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : MonoBehaviour
{
    private Ball ball;
    private UnitMotion unitMotion;
    private Vector3 direction;

    private float spawnPositionX;

    private void Awake()
    {
        unitMotion = GetComponent<UnitMotion>();
        ball = GameObject.FindObjectOfType<Ball>();
    }

    private void Start()
    {
        spawnPositionX = transform.position.x;
    }

    private void Update()
    {
        Move();
        unitMotion.MoveDirection = direction.normalized;
    }

    private void Move()
    {
        direction = Vector3.zero;

        if (ball.transform.position.x > 0 && TrajectoryCalculate.HitPosition.x > 0)
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
            float dir = spawnPositionX - transform.position.x;
            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, 0, direction.z);
            }
        }
    }

    private void Jump()
    {
        if (ball.transform.position.y > transform.position.y + 1)
        {
            if (Mathf.Abs(ball.transform.position.y - transform.position.y) < 3)
            {
                direction = new Vector3(direction.x, 10, direction.z);
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
