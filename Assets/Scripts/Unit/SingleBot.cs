using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : Unit
{
    private Ball _ball;

    private Vector3 direction;
    private float defaultPositionX;
    private float nextHeightForJamp;

    float targetPositionX;
    float dir;
    float adjustment;

    private void Start()
    {
        defaultPositionX = transform.position.x;
        _ball = GameObject.FindObjectOfType<Ball>();
        nextHeightForJamp = Random.Range(2, 4);
    }

    protected override void ClearValue()
    {
        direction = Vector3.zero;
        nextHeightForJamp = Random.Range(2, 4);
    }

    private void FixedUpdate()
    {
        AiLogic();
    }

    private void AiLogic()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && PositionOnSelfSide(_ball.TrajectoryHit.point.x))
        {
            defaultPositionX = 0;

            if (!unitMotion.OnGround)
            {
                targetPositionX = _ball.transform.position.x;
            }
            else
            {
                targetPositionX = _ball.TrajectoryHit.point.x + CalculateAdjustment();
            }

            dir = targetPositionX - transform.position.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            #region Jump
            direction = new Vector3(direction.x, 0, direction.z);
            if (PositionOnSelfSide(_ball.transform.position.x) && unitMotion.OnGround)
            {
                if (Mathf.Abs(_ball.transform.position.y) <= (transform.position.y + nextHeightForJamp))
                {
                    direction = new Vector3(direction.x, 1, direction.z);
                    nextHeightForJamp = Random.Range(2, 4);
                }
            }
            #endregion

        }
        else
        {
            dir = defaultPositionX - transform.position.x;
            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, 0, direction.z);
            }
        }

        unitMotion.moveDirection = direction.normalized;
    }

    private bool PositionOnSelfSide(float xPosition)
    {
        if (playerSide == PlayerSide.Left)
        {

            if (xPosition < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (playerSide == PlayerSide.Right)
        {

            if (xPosition > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private float CalculateAdjustment()
    {
        adjustment = 0;

        if (Mathf.Abs(_ball.transform.position.x - transform.position.x) > .1f)
        {
            if (_ball.transform.position.x < transform.position.x)
            {
                adjustment = -0.5f;
            }
            else
            {
                if (_ball.transform.position.x > transform.position.x)
                {
                    adjustment = 0.5f;
                }
            }
        }

        return adjustment;
    }

    //private float WaitPosition
    //{
    //    get
    //    {
    //        if (defaultPositionX == 0)
    //        {
    //            defaultPositionX = Random.Range(0.2f, 5);
    //        }

    //        return defaultPositionX;
    //    }
    //}


}