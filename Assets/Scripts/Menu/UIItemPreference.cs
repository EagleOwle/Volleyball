using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class UIItemPreference : MonoBehaviour
{
    public Text nameItem;
    public Slider slider;
    public Text valueSlider;
    public UnityEvent<float> actionChangeValue;

    public void GetPreferenceValue(float value)
    {
        slider.maxValue = value * 2;
        slider.value = value;
    }

    public void OnSliderValueChange()
    {
        valueSlider.text = slider.value.ToString();
        actionChangeValue.Invoke(slider.value);
    }

    private void OnValidate()
    {
        gameObject.name = nameItem.text;
    }

}
