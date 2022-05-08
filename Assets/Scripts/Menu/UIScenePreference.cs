using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScenePreference : UIPanel
{
    [SerializeField] private Transform buttonContent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Image previewImage;
    [SerializeField] private Text nameSceneText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Slider roundCountSlider;
    [SerializeField] private Text roundCountText;

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
        UIHud.Singletone.OnChangePanel(UIPanelName.Main);
    }

    private void OnEnable()
    {
        ClearButtons();

        for (int i = 0; i < scenes.Length; i++)
        {
            GameObject tmp = Instantiate(buttonPrefab, buttonContent);
            tmp.GetComponentInChildren<Text>().text = scenes[i].name;
            int index = i;
            tmp.GetComponent<Button>().onClick.AddListener(() => SetCurrentScene(index));
            tmp.SetActive(true);
            buttons.Add(tmp);
        }

        SetCurrentScene(0);
    }

    private void OnButtonStart()
    {
        ScenePreference.Singleton.SetScene(currentScene);
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

    private void OnChangeRoundCountValue(float value)
    {
        //Debug.Log("OnChangeRoundCountValue " + value);
        currentScene.rounds = (int)value;
        roundCountText.text = value.ToString();
    }

    private void SetCurrentScene(int index)
    {
        currentScene = scenes[index];
        currentScene.arrayIndex = index;
        nameSceneText.text = currentScene.name;
        previewImage.sprite = currentScene.sprite;
        descriptionText.text = currentScene.description;

        roundCountSlider.onValueChanged.RemoveAllListeners();
        roundCountSlider.minValue = 1;
        roundCountSlider.maxValue = 15;
        roundCountSlider.value = currentScene.rounds;
        roundCountSlider.onValueChanged.AddListener(OnChangeRoundCountValue);
        roundCountText.text = roundCountSlider.value.ToString();
    }

}
