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

    public void OnDrag(PointerEventData value)
    {
        joystick.rectTransform.position = value.position;
        joystick.rectTransform.localPosition = Vector2.ClampMagnitude(joystick.rectTransform.localPosition, radius);

        percent.x = (100 / radius) * joystick.rectTransform.localPosition.x;
        percent.y = (100 / radius) * joystick.rectTransform.localPosition.y;

        InputHandler.Instance.OnJoysticInput(percent * 0.01f);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        joystick.rectTransform.anchoredPosition = Vector3.zero;
        InputHandler.Instance.OnJoysticInput(Vector2.zero);
    }
}
