using UnityEngine;


[RequireComponent(typeof(UnitMotion))]
public class SinglePlayer : Unit
{
    private UnitMotion unitMotion;
    private Vector3 lastMoveDirection;
    [SerializeField] private bool jumpOnButton = true;

    private void OnEnable()
    {
        InputHandler.Instance.ActionSetSwipeDirection += OnSwipe;
        InputHandler.Instance.ActionOnInputButton += OnInputButton;
        unitMotion = GetComponent<UnitMotion>();
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
        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetSwipeDirection -= OnSwipe;
        InputHandler.Instance.ActionOnInputButton -= OnInputButton;
    }

    private void OnDestroy()
    {
        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetSwipeDirection -= OnSwipe;
        InputHandler.Instance.ActionOnInputButton -= OnInputButton;
    }

}
