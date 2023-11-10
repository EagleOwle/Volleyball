using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SinglePlayer : Unit
{
    private Vector3 lastMoveDirection;

    public override void Initialise(int playerIndex)
    {
        base.Initialise(playerIndex);
        if (InputHandler.Instance == null)
        {
            Debug.LogError("Input handler is null");
            return;
        }

        InputHandler.Instance.ActionSetSwipeDirection += OnSwipe;
        if (playerIndex == 0)
        {
            InputHandler.Instance.ActionOnInputForPlayer0 += OnInputButton;
        }

        if (playerIndex == 1)
        {
            InputHandler.Instance.ActionOnInputForPlayer1 += OnInputButton;
        }
    }

    protected override void ClearValue()
    {
        lastMoveDirection = Vector3.zero;
    }

    private void OnSwipe(Vector3 direction)
    {
        lastMoveDirection = direction.normalized;
        unitMotion.MoveDirection = lastMoveDirection;
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

    protected override void OnDisable()
    {
        base.OnDisable();

        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetSwipeDirection -= OnSwipe;
        InputHandler.Instance.ActionOnInputForPlayer0 -= OnInputButton;
        InputHandler.Instance.ActionOnInputForPlayer1 -= OnInputButton;
    }

    protected override void OnDestroy()
    {
        base.OnDisable();

        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetSwipeDirection -= OnSwipe;
        InputHandler.Instance.ActionOnInputForPlayer0 -= OnInputButton;
        InputHandler.Instance.ActionOnInputForPlayer1 -= OnInputButton;
    }

}
