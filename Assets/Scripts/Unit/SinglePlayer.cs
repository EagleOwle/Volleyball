using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SinglePlayer : Unit
{
    [SerializeField] protected LayerMask ballLayer = 0;

    private UnitMotion unitMotion;
    private Vector3 lastMoveDirection;
    [SerializeField] private bool jumpOnButton = true;

    private void OnEnable()
    {
        if (InputHandler.Instance == null)
        {
            Debug.LogError("Input handler is null");
            return;
        }

        InputHandler.Instance.ActionSetSwipeDirection += OnSwipe;
        InputHandler.Instance.ActionOnInputButton += OnInputButton;
        StateMachine.actionChangeState += OnChangeGameState;
        unitMotion = GetComponent<UnitMotion>();
    }


    public override void Initialise()
    {
        lastMoveDirection = Vector3.zero;
        jumpOnButton = false;
    }

    private void OnSwipe(Vector3 direction)
    {
        if (jumpOnButton)
        {
            direction.y = lastMoveDirection.y;
        }

        lastMoveDirection = direction.normalized;
        unitMotion.MoveDirection = lastMoveDirection;
    }

    private void OnChangeGameState(State next, State last)
    {
        if (next is GameState)
        {
            Initialise();
        }

        if (next is PauseState)
        {
        }

        if (next is FallState)
        {
        }
    }

    private float PushForce()
    {
        switch (Game.Instance.scene.difficultEnum)
        {
            case ScenePreference.GameDifficult.Easy:
                return Preference.Singleton.pushForce * 2;

            case ScenePreference.GameDifficult.Normal:
                return Preference.Singleton.pushForce;

            case ScenePreference.GameDifficult.Hard:
                return 0;

            default:
                return Preference.Singleton.pushForce;
        }
    }

    private void OnInputButton(InputHandler.InputButton button, InputHandler.InputDirection type)
    {
        switch (button)
        {
            case InputHandler.InputButton.left:
                if (type == InputHandler.InputDirection.up)
                {
                    lastMoveDirection = new Vector2(0, lastMoveDirection.y);
                }
                else
                {
                    lastMoveDirection = new Vector2(-1, lastMoveDirection.y);
                }
                break;

            case InputHandler.InputButton.right:
                if (type == InputHandler.InputDirection.up)
                {
                    lastMoveDirection = new Vector2(0, lastMoveDirection.y);
                }
                else
                {
                    lastMoveDirection = new Vector2(1, lastMoveDirection.y);
                }
                break;

            case InputHandler.InputButton.jump:
                if (type == InputHandler.InputDirection.up)
                {
                    lastMoveDirection = new Vector2(lastMoveDirection.x, 0);
                }
                else
                {
                    lastMoveDirection = new Vector2(lastMoveDirection.x, 1);
                }
                break;
        }

        unitMotion.MoveDirection = lastMoveDirection;
    }

    private void OnDisable()
    {
        StateMachine.actionChangeState -= OnChangeGameState;

        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetSwipeDirection -= OnSwipe;
        InputHandler.Instance.ActionOnInputButton -= OnInputButton;
    }

    private void OnDestroy()
    {
        StateMachine.actionChangeState -= OnChangeGameState;

        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetSwipeDirection -= OnSwipe;
        InputHandler.Instance.ActionOnInputButton -= OnInputButton;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (StateMachine.currentState is GameState)
        {
            if ((1 << collision.collider.gameObject.layer & ballLayer) != 0)
            {
                Rigidbody rigidbody = collision.collider.gameObject.GetComponentInParent<Rigidbody>();
                rigidbody.AddForce((Vector3.right + Vector3.up) * PushForce(), ForceMode.Impulse);
            }
        }
    }
}
