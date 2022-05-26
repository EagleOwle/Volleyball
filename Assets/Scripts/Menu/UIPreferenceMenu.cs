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
    [Space]
    [SerializeField] private Text lowQualityText;
    [SerializeField] private Text normalQualityText;
    [SerializeField] private Text highQualityText;
    [SerializeField] private Slider qualitySlider;

    public override void Init()
    {
        base.Init();

        returnBtn.onClick.AddListener(OnButtonReturn);

        musicSlider.onValueChanged.AddListener(OnMusicValueChange);
        musicSlider.value = Preference.Singleton.MusicValue;
        musicValueText.text = (musicSlider.value * 100).ToString("F0");

        sfxSlider.onValueChanged.AddListener(OnSfxValueChange);
        sfxSlider.value = Preference.Singleton.SfxValue;
        sfxValueText.text = (sfxSlider.value * 100).ToString("F0");

        vibraToggle.onValueChanged.AddListener(OnVibraChange);
        vibraToggle.isOn = Preference.Singleton.Vibration;
        vibraValueText.text = "Vibration is" + vibraToggle.isOn;

        qualitySlider.onValueChanged.AddListener(OnQualityChange);
        int value = QualitySettings.GetQualityLevel();
        qualitySlider.value = value;
        OnQualityChange(value);
    }

    private void OnMusicValueChange(float value)
    {
        Preference.Singleton.MusicValue = value;
        musicValueText.text = (musicSlider.value * 100).ToString("F0");
    }

    private void OnSfxValueChange(float value)
    {
        Preference.Singleton.SfxValue = value;
        sfxValueText.text = (sfxSlider.value * 100).ToString("F0");
    }

    private void OnVibraChange(bool value)
    {
        Preference.Singleton.Vibration = value;
        vibraValueText.text = "Vibration is " + vibraToggle.isOn;
    }

    private void OnButtonReturn()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Main);
    }

    private void OnQualityChange(float value)
    {
        switch (value)
        {
            case 0:
                lowQualityText.fontSize = 50;
                normalQualityText.fontSize = 30;
                highQualityText.fontSize = 30;
                break;

            case 1:
                lowQualityText.fontSize = 30;
                normalQualityText.fontSize = 50;
                highQualityText.fontSize = 30;
                break;

            case 2:
                lowQualityText.fontSize = 30;
                normalQualityText.fontSize = 30;
                highQualityText.fontSize = 50;
                break;
                
        }

        QualitySettings.SetQualityLevel((int)value);
    }

}
