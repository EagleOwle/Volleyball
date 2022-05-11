using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPreferenceMenu : UIPanel
{
    [SerializeField] private Button returnBtn;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Text musicValueText;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Text sfxValueText;
    [SerializeField] private Toggle vibraToggle;
    [SerializeField] private Text vibraValueText;

    public override void Init()
    {
        base.Init();

        returnBtn.onClick.AddListener(OnButtonReturn);

        musicSlider.onValueChanged.AddListener(OnMusicValueChange);
        musicSlider.value = Preference.Singleton.musicValue;
        musicValueText.text = (musicSlider.value * 100).ToString("F0");

        sfxSlider.onValueChanged.AddListener(OnSfxValueChange);
        sfxSlider.value = Preference.Singleton.sfxValue;
        sfxValueText.text = (sfxSlider.value * 100).ToString("F0");

        vibraToggle.onValueChanged.AddListener(OnVibraChange);
        vibraToggle.isOn = Preference.Singleton.onVibration;
        vibraValueText.text = "Vibration is" + vibraToggle.isOn;
    }

    private void OnMusicValueChange(float value)
    {
        Preference.Singleton.musicValue = value;
        musicValueText.text = (musicSlider.value * 100).ToString("F0");
    }

    private void OnSfxValueChange(float value)
    {
        Preference.Singleton.sfxValue = value;
        sfxValueText.text = (sfxSlider.value * 100).ToString("F0");
    }

    private void OnVibraChange(bool value)
    {
        Preference.Singleton.onVibration = value;
        vibraValueText.text = "Vibration is " + vibraToggle.isOn;
    }

    private void OnButtonReturn()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Main);
    }

}
