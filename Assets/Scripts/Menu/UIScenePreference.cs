using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class UIScenePreference : UIPanel
{
    [SerializeField] private Transform buttonContent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Image previewImage;
    //[SerializeField] private Text nameSceneText;
    //[SerializeField] private Text descriptionText;
    [SerializeField] private LocalizeStringEvent nameSceneLocalize;
    [SerializeField] private LocalizeStringEvent descriptionLocalize;
    [SerializeField] private Button startButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Slider roundCountSlider;
    [SerializeField] private Text roundCountText;
    [SerializeField] private Dropdown dropdown;

    private ScenePreference.Scene[] scenes;
    private ScenePreference.Scene currentScene;
    private List<GameObject> buttons = new List<GameObject>();

    private void Awake()
    {
        startButton.onClick.AddListener(OnButtonStart);
        returnButton.onClick.AddListener(OnButtonReturn);
       
        scenes = ScenePreference.Singleton.scenes;
    }

    private void OnButtonReturn()
    {
        UIHud.Instance.OnChangePanel(UIPanelName.Main);
    }

    private void OnEnable()
    {
        ClearButtons();

        for (int i = 0; i < scenes.Length; i++)
        {
            GameObject tmp = Instantiate(buttonPrefab, buttonContent);
            tmp.GetComponent<UIChangeSceneButton>().localizeString.StringReference = scenes[i].nameString;
            int index = i;
            tmp.GetComponent<Button>().onClick.AddListener(() => SetCurrentScene(index));
            tmp.SetActive(true);
            buttons.Add(tmp);
        }

        SetCurrentScene(0);
    }

    private void OnButtonStart()
    {
        ScenePreference.Singleton.GameScene = currentScene;
        SceneLoader.Instance.LoadLevel(currentScene.buildIndex);
    }

    private void ClearButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }

        buttons.Clear();
    }

    private void OnChangeDropdownValue(int value)
    {
        currentScene.difficult = value;
    }

    private void OnChangeRoundCountValue(float value)
    {
        currentScene.matchPreference.Rounds = (int)value;
        roundCountText.text = value.ToString();
    }

    private void SetCurrentScene(int index)
    {
        currentScene = scenes[index];
        currentScene.arrayIndex = index;
        previewImage.sprite = currentScene.sprite;

        nameSceneLocalize.StringReference = currentScene.nameString;
        descriptionLocalize.StringReference = currentScene.descriptionString;

        for (int i = 0; i < buttons.Count; i++)
        {
            if(i == index)
            {
                buttons[i].GetComponent<UIChangeSceneButton>().SetActiveSprite = true;
            }
            else
            {
                buttons[i].GetComponent<UIChangeSceneButton>().SetActiveSprite = false;
            }
        }

        roundCountSlider.onValueChanged.RemoveAllListeners();
        roundCountSlider.minValue = 1;
        roundCountSlider.maxValue = 15;
        roundCountSlider.value = currentScene.matchPreference.Rounds;
        roundCountSlider.onValueChanged.AddListener(OnChangeRoundCountValue);
        roundCountText.text = roundCountSlider.value.ToString();

        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.value = (int)currentScene.difficultEnum;
        dropdown.onValueChanged.AddListener(OnChangeDropdownValue);

    }

}
