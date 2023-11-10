using System.Collections;
using UnityEngine;

public class InputSwipe : MonoBehaviour
{
    [SerializeField] private LayerMask uiButtonMask;

    private bool onSwipe;
    private Vector2 tupPosition;

    private Vector3 SwipeDirection
    {
        set
        {
            if (swipeDirection != value)
            {
                swipeDirection = value;
                //InputHandler.Instance.OnInputDirection(swipeDirection);
            }
        }
    }
    private Vector3 swipeDirection;

    private Vector2 percent;

    private void SetTupPosition()
    {
        tupPosition = (Vector2)Input.mousePosition;
    }

    private void GetSwipe()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (GetUIObject(Input.mousePosition) == null)
            {
                tupPosition = (Vector2)Input.mousePosition;
                InvokeRepeating(nameof(SetTupPosition), 0.1f, 0.1f);
                onSwipe = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SwipeBreak();
        }

        if (onSwipe)
        {
            Vector3 direction = (Input.mousePosition - (Vector3)tupPosition);

            direction.x = Mathf.Clamp(direction.x, -100, 100);
            direction.y = Mathf.Clamp(direction.y, -100, 100);

            percent.x = Screen.width / 100 * Preference.Singleton.player[0].joysticDeadZone;
            percent.y = Screen.height / 100 * Preference.Singleton.player[0].joysticDeadZone;

            if (Mathf.Abs(direction.x) < percent.x)
            {
                direction.x = 0;
            }
            if (Mathf.Abs(direction.y) < percent.y)
            {
                direction.y = 0;
            }

            SwipeDirection = direction;
        }
    }

    private void SwipeBreak()
    {
        onSwipe = false;
        SwipeDirection = Vector3.zero;
        tupPosition = Vector3.zero;
        CancelInvoke();
    }

    private GameObject GetUIObject(Vector3 touchScreenPositin)
    {
        GameObject touchObject = null;

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchScreenPositin);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchWorldPosition, Camera.main.transform.forward, 100, uiButtonMask);

        if (hitInformation.collider != null)
        {
            touchObject = hitInformation.transform.gameObject;
        }

        return touchObject;
    }

    private void OnDrawGizmos()
    {
        if (onSwipe)
        {
            Gizmos.color = Color.blue;

            Vector3 startPosition = tupPosition;
            startPosition.z = Camera.main.nearClipPlane;

            Vector3 endPosition = tupPosition + (Vector2)swipeDirection;
            endPosition.z = Camera.main.nearClipPlane;

            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(startPosition), Camera.main.ScreenToWorldPoint(endPosition));
        }
    }

}