using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private AudioClip clickClip, enterClip;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
       AudioController.Instance.PlayClip(enterClip);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
         AudioController.Instance.PlayClip(clickClip);
    }
}
