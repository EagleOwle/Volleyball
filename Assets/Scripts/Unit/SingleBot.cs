using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : Unit
{
    [SerializeField] protected LayerMask ballLayer = 0;
    private Ball _ball;
    private Ball ball
    {
        get
        {
            if (_ball == null)
            {
                _ball = GameObject.FindObjectOfType<Ball>();
            }

            return _ball;
        }
    }
    private UnitMotion unitMotion;
    private Vector3 direction;
    private float lastTargetPositionX;
    private float defaultPositionX;

    private void Awake()
    {
        unitMotion = GetComponent<UnitMotion>();
        
    }

    private void Start()
    {
        defaultPositionX = transform.position.x;
    }

    private void OnEnable()
    {
        StateMachine.actionChangeState += OnChangeGameState;
    }

    private void OnDisable()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    private void OnDestroy()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    public override void Initialise()
    {
        direction = Vector3.zero;
    }

    private void OnChangeGameState(State state)
    {
        if (state is GameState)
        {
            Initialise();
        }
    }

    private void Update()
    {
         switch (Game.Instance.scene.difficultEnum)
        {
            case ScenePreference.GameDifficult.Easy:
                EasyBehavior();
                break;
            case ScenePreference.GameDifficult.Normal:
                NormalBehavior();
                break;
            case ScenePreference.GameDifficult.Hard:
                HardBehavior();
                break;
        }

        unitMotion.MoveDirection = direction.normalized;

    }

    private void EasyBehavior()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryCalculate.Hit.point.x >= 0)
        {
            float dir = TrajectoryCalculate.Hit.point.x - transform.position.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }
            #region Jump

            if (ball.transform.position.y > transform.position.y + 1)
            {
                if (Mathf.Abs(ball.transform.position.y - transform.position.y) < Preference.Singleton.deadzoneForJump)
                {
                    direction = new Vector3(direction.x, 1, direction.z);
                }
                else
                {
                    direction = new Vector3(direction.x, 0, direction.z);
                }
            }
            #endregion

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

    private void NormalBehavior()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryCalculate.Hit.point.x >= 0)
        {
            float targetPositionX = TrajectoryCalculate.Hit.point.x + CalculateAdjustment();
            float dir = targetPositionX - transform.position.x;

            lastTargetPositionX = TrajectoryCalculate.Hit.point.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            #region Jump

            if (ball.transform.position.y > transform.position.y + 1)
            {
                if ((ball.transform.position - transform.position).magnitude < 2.5f)
                {
                    if (Mathf.Abs(ball.transform.position.y - transform.position.y) < Preference.Singleton.deadzoneForJump)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
                    }
                    else
                    {
                        direction = new Vector3(direction.x, 0, direction.z);
                    }
                }
            }
            #endregion

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

    private float CalculateAdjustment()
    {
        float adjustment = 0;

        if (Mathf.Abs(lastTargetPositionX - transform.position.x) > .1f)
        {
            if (lastTargetPositionX < transform.position.x)
            {
                adjustment = -.5f;
            }
            else
            {
                if (lastTargetPositionX > transform.position.x)
                {
                    adjustment = .5f;
                }
            }
        }

        return adjustment;
    }

    private void HardBehavior()
    {
        direction = Vector3.zero;

        if (StateMachine.currentState is GameState && TrajectoryCalculate.Hit.point.x >= 0)
        {
            float targetPositionX = TrajectoryCalculate.Hit.point.x + -.5f;

            float dir = targetPositionX - transform.position.x;

            if (Mathf.Abs(dir) > Preference.Singleton.deadzoneForMove)
            {
                direction = new Vector3(dir, direction.y, direction.z);
            }

            #region Jump

            if (ball.transform.position.y > transform.position.y + 1)
            {
                if ((ball.transform.position - transform.position).magnitude < 2.5f)
                {
                    if (Mathf.Abs(ball.transform.position.y - transform.position.y) < Preference.Singleton.deadzoneForJump)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
                    }
                    else
                    {
                        direction = new Vector3(direction.x, 0, direction.z);
                    }
                }
            }
            #endregion

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

    private void OnCollisionEnter(Collision collision)
    {
        if (StateMachine.currentState is GameState)
        {
            if ((1 << collision.gameObject.layer & ballLayer) != 0)
            {
                if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
                {
                    rigidBody.AddForce((Vector3.left + Vector3.up) * Preference.Singleton.pushForce);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TrajectoryCalculate.Hit.point, 0.1f);

    }


}
