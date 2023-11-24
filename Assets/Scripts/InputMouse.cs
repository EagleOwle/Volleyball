using UnityEngine;
using System.Collections;

public class InputMouse: MonoBehaviour
{
    private Vector2 lastPosition;
    private Vector2 direction;

    private void OnEnable()
    {
        lastPosition = Input.mousePosition;
    }

    private void Update()
    {
        if (Application.isFocused)
        {
            direction = (Input.mousePosition - (Vector3)lastPosition);

            for (int i = 0; i < Preference.Singleton.player.Length; i++)
            {
                if (Mathf.Abs(direction.x) < Preference.Singleton.player[1].MouseMoveSence)
                {
                    direction.x = 0;
                }

                if (Mathf.Abs(direction.y) < Preference.Singleton.player[1].MouseJumpSence)
                {
                    direction.y = 0;
                }

                InputHandler.Instance.OnInputDirection(direction, i);
            }
        }

        lastPosition = Input.mousePosition;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(lastPosition, Input.mousePosition);
        Gizmos.DrawSphere(Input.mousePosition, 1);

    }

}
