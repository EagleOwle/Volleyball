using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioClip clickClip;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(float value)
    {
        AudioController.Instance.PlayClip(clickClip, false);
    }

}
