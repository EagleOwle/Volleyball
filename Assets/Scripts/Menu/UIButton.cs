using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Color _defaultColor,  _enterColor, _exitColor, _downColor;
    [SerializeField] private Text _text;
    [SerializeField] private AudioClip clickClip;

    private void OnEnable()
    {
        if (_text != null)
        {
            _text.color = _defaultColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //_text.color = _enterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //_text.color = _defaultColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_text != null)
        {
            _text.color = _defaultColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_text != null)
        {
            _text.color = _downColor;
        }

        AudioController.Instance.PlayClip(clickClip);
    }
}
