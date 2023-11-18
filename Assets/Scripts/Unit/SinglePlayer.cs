using UnityEngine;

[RequireComponent(typeof(UnitMotion))]
public class SinglePlayer : Unit
{
    private Vector3 lastMoveDirection;

    public override void Initialise(PlayerSide playerSide, string playerName)
    {
        base.Initialise(playerSide, playerName);
        if (InputHandler.Instance == null)
        {
            Debug.LogError("Input handler is null");
            return;
        }
       
        if (playerSide == PlayerSide.Left)
        {
            if (Preference.Singleton.player[0].inputType == InputType.joystick)
            {
                InputHandler.Instance.ActionSetInputDirectionForPlayer0 += OnInputDirection;
            }

            if (Preference.Singleton.player[0].inputType == InputType.button)
            {
                InputHandler.Instance.ActionOnInputForPlayer0 += OnInputButton;
            }
        }

        if (playerSide == PlayerSide.Right)
        {
            if (Preference.Singleton.player[1].inputType == InputType.joystick)
            {
                InputHandler.Instance.ActionSetInputDirectionForPlayer1 += OnInputDirection;
            }

            if (Preference.Singleton.player[1].inputType == InputType.button)
            {
                InputHandler.Instance.ActionOnInputForPlayer1 += OnInputButton;
                
            }
        }
    }

    protected override void ClearValue()
    {
        lastMoveDirection = Vector3.zero;
    }

    private void OnInputDirection(Vector2 direction)
    {
        lastMoveDirection = direction.normalized;
        unitMotion.moveDirection = lastMoveDirection;
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

        unitMotion.moveDirection = lastMoveDirection;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetInputDirectionForPlayer0 -= OnInputDirection;
        InputHandler.Instance.ActionSetInputDirectionForPlayer1 -= OnInputDirection;
        InputHandler.Instance.ActionOnInputForPlayer0 -= OnInputButton;
        InputHandler.Instance.ActionOnInputForPlayer1 -= OnInputButton;
    }

    protected override void OnDestroy()
    {
        base.OnDisable();

        if (InputHandler.Instance == null) return;

        InputHandler.Instance.ActionSetInputDirectionForPlayer0 -= OnInputDirection;
        InputHandler.Instance.ActionSetInputDirectionForPlayer1 -= OnInputDirection;
        InputHandler.Instance.ActionOnInputForPlayer0 -= OnInputButton;
        InputHandler.Instance.ActionOnInputForPlayer1 -= OnInputButton;
    }

}
