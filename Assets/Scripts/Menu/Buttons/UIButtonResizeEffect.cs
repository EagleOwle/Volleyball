using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonResizeEffect : UIButton
{
    [SerializeField] private Vector2 defaultSize, maxSize;
    [SerializeField] private float sizeMultiplier = 1.5f;

    private void Awake()
    {
        defaultSize = (transform as RectTransform).sizeDelta;
        maxSize = defaultSize * sizeMultiplier;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        (transform as RectTransform).sizeDelta = maxSize;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        (transform as RectTransform).sizeDelta = defaultSize;
    }

    private void OnEnable()
    {
        (transform as RectTransform).sizeDelta = defaultSize;
    }

}
