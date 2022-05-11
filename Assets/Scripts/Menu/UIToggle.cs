using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private AudioClip clickClip;


    private void Start()
    {
        toggle.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(bool value)
    {
        AudioController.Instance.PlayClip(clickClip);
    }


}
