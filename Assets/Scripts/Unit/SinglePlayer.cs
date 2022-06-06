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
        if(InputHandler.Instance == null)
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

    private void OnChangeGameState(State state)
    {
        if (state is GameState)
        {
            Initialise();
        }

        if (state is PauseState)
        {
        }

        if (state is FallState)
        {
        }
    }

    private void OnInputButton(InputHandler.InputButton button, InputHandler.InputType type)
    {
        switch (button)
        {
            case InputHandler.InputButton.left:
                if (type == InputHandler.InputType.up)
                {
                    lastMoveDirection = new Vector2(0, lastMoveDirection.y);
                }
                else
                {
                    lastMoveDirection = new Vector2(-1, lastMoveDirection.y);
                }
                break;

            case InputHandler.InputButton.right:
                if (type == InputHandler.InputType.up)
                {
                    lastMoveDirection = new Vector2(0, lastMoveDirection.y);
                }
                else
                {
                    lastMoveDirection = new Vector2(1, lastMoveDirection.y);
                }
                break;

            case InputHandler.InputButton.jump:
                if (type == InputHandler.InputType.up)
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
                if (collision.collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
                {
                    rigidBody.AddForce((Vector3.right + Vector3.up) * Preference.Singleton.pushForce);
                }
            }
        }
    }

    
}
