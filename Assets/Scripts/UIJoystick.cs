using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float radius = 600;
    [SerializeField] private Image joystick;

    private Vector2 percent;
    private Vector2 result;

    public void OnDrag(PointerEventData value)
    {
        joystick.rectTransform.position = value.position;
        joystick.rectTransform.localPosition = Vector2.ClampMagnitude(joystick.rectTransform.localPosition, radius);

        percent.x = (100 / radius) * joystick.rectTransform.localPosition.x;
        percent.y = (100 / radius) * joystick.rectTransform.localPosition.y;

        result = percent * 0.01f;

        if (Mathf.Abs(result.x) < Preference.Singleton.player[0].JoysticDeadZone)
        {
            result.x = 0;
        }
        if (Mathf.Abs(result.y) < Preference.Singleton.player[0].JoysticDeadZone)
        {
            result.y = 0;
        }

       // InputHandler.Instance.OnInputDirection(result);
        
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        joystick.rectTransform.anchoredPosition = Vector3.zero;
       // InputHandler.Instance.OnInputDirection(Vector2.zero);
    }
}
