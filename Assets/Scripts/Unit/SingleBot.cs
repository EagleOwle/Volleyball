using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : Unit
{
    private Ball _ball;

    private Vector3 direction;
    private float defaultPositionX;
    private float nextHeightForJamp;

    private void Start()
    {
        defaultPositionX = transform.position.x;
        _ball = GameObject.FindObjectOfType<Ball>();
        nextHeightForJamp = Random.Range(3, 4);
    }

    protected override void ClearValue()
    {
        direction = Vector3.zero;
        nextHeightForJamp = Random.Range(3, 4);
    }

    private void Update()
    {
        switch (Game.Instance.scene.difficultEnum)
        {
            case GameDifficult.Easy:
                EasyBehavior();
                break;
            case GameDifficult.Normal:
                NormalBehavior();
                break;
            case GameDifficult.Hard:
                HardBehavior();
                break;
        }

        unitMotion.MoveDirection = direction.normalized;

    }

    private void EasyBehavior()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryBallOnSelfSide(_ball.TrajectoryHit.point.x))
        {
            defaultPositionX = 0;
            float targetPositionX = _ball.TrajectoryHit.point.x + CalculateAdjustment();
            float dir = targetPositionX - transform.position.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            #region Jump

            direction = new Vector3(direction.x, 0, direction.z);
            if (TrajectoryBallOnSelfSide(_ball.transform.position.x) && unitMotion.OnGround)
            {
                if (_ball.transform.position.y > transform.position.y + 1)
                {
                    if (Mathf.Abs(_ball.transform.position.y - transform.position.y) < nextHeightForJamp)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
                        nextHeightForJamp = Random.Range(3, 4);
                    }
                }
            }
            #endregion

        }
        else
        {
            float dir = WaitPosition - transform.position.x;
            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, 0, direction.z);
            }
        }
    }

    private void NormalBehavior()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryBallOnSelfSide(_ball.TrajectoryHit.point.x))
        {
            defaultPositionX = 0;
            float targetPositionX = _ball.TrajectoryHit.point.x + CalculateAdjustment();
            float dir = targetPositionX - transform.position.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            #region Jump

            direction = new Vector3(direction.x, 0, direction.z);
            if (TrajectoryBallOnSelfSide(_ball.transform.position.x) && unitMotion.OnGround)
            {
                if (_ball.transform.position.y > transform.position.y + 1)
                {
                    if (Mathf.Abs(_ball.transform.position.y - transform.position.y) < nextHeightForJamp)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
                        nextHeightForJamp = Random.Range(3, 4);
                    }
                }
            }
            #endregion

        }
        else
        {
            float dir = WaitPosition - transform.position.x;
            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, 0, direction.z);
            }
        }
    }

    private void HardBehavior()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryBallOnSelfSide(_ball.TrajectoryHit.point.x))
        {
            defaultPositionX = 0;
            float targetPositionX = _ball.TrajectoryHit.point.x + CalculateAdjustment();
            float dir = targetPositionX - transform.position.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            #region Jump

            direction = new Vector3(direction.x, 0, direction.z);
            if (TrajectoryBallOnSelfSide(_ball.transform.position.x) && unitMotion.OnGround)
            {
                if (_ball.transform.position.y > transform.position.y + 1)
                {
                    if (Mathf.Abs(_ball.transform.position.y - transform.position.y) < nextHeightForJamp)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
                        nextHeightForJamp = Random.Range(3, 4);
                    }
                }
            }
            #endregion

        }
        else
        {
            float dir = WaitPosition - transform.position.x;
            if (Mathf.Abs(dir) > 0.3f)
            {
                direction = new Vector3(dir, 0, direction.z);
            }
        }
    }

    private bool TrajectoryBallOnSelfSide(float xPosition)
    {
        if (player == PlayerSide.Left)
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

        if (player == PlayerSide.Right)
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
        float adjustment = 0;

        if (Mathf.Abs(_ball.transform.position.x - transform.position.x) > .1f)
        {
            if (_ball.transform.position.x < transform.position.x)
            {
                adjustment = -.5f;
            }
            else
            {
                if (_ball.transform.position.x > transform.position.x)
                {
                    adjustment = .5f;
                }
            }
        }

        return adjustment;
    }

    private float WaitPosition
    {
        get
        {
            if (defaultPositionX == 0)
            {
                defaultPositionX = Random.Range(0.2f, 5);
            }

            return defaultPositionX;
        }
    }


}