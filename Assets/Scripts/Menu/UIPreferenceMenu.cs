using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPreferenceMenu : UIPanel
{
    private enum PreferenceType { noneType, soundType, inputType, graphicType }

    
    [SerializeField] private Button soundButton, inputButton, graphicsButton, returnButton;
    [SerializeField] private GameObject soundPanel, inputPanel, graphicsPanel, languagePanel;
    [Space()]
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
    [Space]
    [SerializeField] private Text inputButtonText;
    [SerializeField] private Text inputJoystickText;
    [SerializeField] private Slider inputTypeSlider;

    public override void Init()
    {
        base.Init();

        soundButton.onClick.AddListener(delegate { ShowPreferenceType(PreferenceType.soundType); });
        inputButton.onClick.AddListener(delegate { ShowPreferenceType(PreferenceType.inputType); });
        graphicsButton.onClick.AddListener(delegate { ShowPreferenceType(PreferenceType.graphicType); });
        returnButton.onClick.AddListener(OnButtonReturn);

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

        inputTypeSlider.onValueChanged.AddListener(OnInputTypeChange);
        value = (int)Preference.Singleton.inputType;
        inputTypeSlider.value = value;
        OnInputTypeChange(value);
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

    private void OnInputTypeChange(float value)
    {
        switch (value)
        {
            case 0:
                inputButtonText.fontSize = 50;
                inputJoystickText.fontSize = 30;
                break;

            case 1:
                inputButtonText.fontSize = 30;
                inputJoystickText.fontSize = 50;
                break;

        }

        Preference.Singleton.inputType = (InputType)value;
    }

    private void ShowPreferenceType(PreferenceType  type)
    {
        switch (type)
        {
            case PreferenceType.noneType:
                soundPanel.SetActive(false);
                inputPanel.SetActive(false);
                graphicsPanel.SetActive(false);
                languagePanel.SetActive(true);
                break;
            case PreferenceType.soundType:
                soundPanel.SetActive(true);
                inputPanel.SetActive(false);
                graphicsPanel.SetActive(false);
                languagePanel.SetActive(true);
                break;
            case PreferenceType.inputType:
                soundPanel.SetActive(false);
                inputPanel.SetActive(true);
                graphicsPanel.SetActive(false);
                languagePanel.SetActive(true);
                break;
            case PreferenceType.graphicType:
                soundPanel.SetActive(false);
                inputPanel.SetActive(false);
                graphicsPanel.SetActive(true);
                languagePanel.SetActive(true);
                break;
        }
    }

}
