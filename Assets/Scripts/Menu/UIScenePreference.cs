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

    private ScenePreference.Scene[] scenes;
    private ScenePreference.Scene currentScene;
    private List<GameObject> buttons = new List<GameObject>();

    private void Awake()
    {
        startButton.onClick.AddListener(OnButtonStart);
        scenes = ScenePreference.Singleton.scenes;
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

    private void SetCurrentScene(int index)
    {
        currentScene = scenes[index];
        nameSceneText.text = currentScene.name;
        previewImage.sprite = currentScene.sprite;
        descriptionText.text = currentScene.description;
    }

}
