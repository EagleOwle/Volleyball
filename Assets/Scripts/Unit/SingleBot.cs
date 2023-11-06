using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SingleBot : Unit
{
    [SerializeField] protected LayerMask ballLayer = 0;
    
    private Ball _ball;

    private UnitMotion unitMotion;
    private Vector3 direction;
    private float defaultPositionX;
    private float nextHeightForJamp;
    public ScenePreference.GameDifficult debugCurrentDifficult;

    private void Awake()
    {
        unitMotion = GetComponent<UnitMotion>();

    }

    private void Start()
    {
        defaultPositionX = transform.position.x;
        _ball = GameObject.FindObjectOfType<Ball>();
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

    private void OnChangeGameState(State next, State last)
    {
        if (next is GameState)
        {
            Initialise();
        }
    }

    private void Update()
    {
        debugCurrentDifficult = Game.Instance.scene.difficultEnum;
        nextHeightForJamp = NextHeightForJump();
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

        if (StateMachine.currentState is GameState && _ball.TrajectoryHit.point.x >= 0)
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

            if (_ball.transform.position.x > -0.5f && unitMotion.OnGround)
            {
                if (_ball.transform.position.y > transform.position.y + 1)
                {
                    if (Mathf.Abs(_ball.transform.position.y - transform.position.y) < nextHeightForJamp)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
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

        if (StateMachine.currentState is GameState && _ball.TrajectoryHit.point.x >= 0)
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

            if (_ball.transform.position.x > -0.5f && unitMotion.OnGround)
            {
                if (_ball.transform.position.y > transform.position.y + 1)
                {
                    if ((_ball.transform.position - transform.position).magnitude < nextHeightForJamp)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
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

        if (StateMachine.currentState is GameState && _ball.TrajectoryHit.point.x >= 0)
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
            if (_ball.transform.position.x > -0.5f && unitMotion.OnGround)
            {
                if (_ball.transform.position.y > transform.position.y + 1)
                {
                    if ((_ball.transform.position - transform.position).magnitude < nextHeightForJamp)
                    {
                        direction = new Vector3(direction.x, 1, direction.z);
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

    private float NextHeightForJump()
    {
        if (nextHeightForJamp == 0)
        {
            switch (_ball.CurrentPlayerSide)
            {
                case PlayerType.None:
                    nextHeightForJamp = 2;// Random.Range(2, 5);
                    break;
                case PlayerType.Local:
                    nextHeightForJamp = 0;
                    break;
                case PlayerType.Rival:
                    nextHeightForJamp = 2;// Random.Range(0, 5);
                    break;
            }
        }

        return nextHeightForJamp;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (StateMachine.currentState is GameState)
        {
            if ((1 << collision.gameObject.layer & ballLayer) != 0)
            {
                Rigidbody rigidbody = collision.collider.gameObject.GetComponentInParent<Rigidbody>();
                rigidbody.AddForce((Vector3.left + Vector3.up) * PushForce(), ForceMode.Impulse);
            }
        }
    }

    private float PushForce()
    {
        switch (Game.Instance.scene.difficultEnum)
        {
            case ScenePreference.GameDifficult.Easy:
                return 0;

            case ScenePreference.GameDifficult.Normal:
                return Preference.Singleton.pushForce;

            case ScenePreference.GameDifficult.Hard:
                return Preference.Singleton.pushForce * 2;

            default:
                return Preference.Singleton.pushForce;
        }
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