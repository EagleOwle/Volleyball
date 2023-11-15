using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class UILoadingScreen : CanvasGroupUI
{
    public static UILoadingScreen Instance;

    [SerializeField] private GameObject inputPlayer1, inputPlayer2;
    [SerializeField] private float showFirstLoad = 3;
    [SerializeField] private LocalizeStringEvent namePlayer1Localize;
    [SerializeField] private LocalizeStringEvent namePlayer2Localize;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        inputPlayer1.SetActive(false);
        inputPlayer2.SetActive(false);
        CanvasGroup.alpha = 1;
        SceneLoader.OnSceneLoadStart += InstantlyShow;
        SceneLoader.OnSceneLoadComplete += Hide;
        Invoke(nameof(Hide), showFirstLoad);
    }

    public void InstantlyShow(int sceneIndex)
    {
        gameObject.SetActive(true);
        CanvasGroup.alpha = 1;

        namePlayer1Localize.StringReference = Preference.Singleton.player[0].nameLocalizedString;
        namePlayer2Localize.StringReference = Preference.Singleton.player[1].nameLocalizedString;

        if (sceneIndex != 0)
        {
            inputPlayer1.SetActive(true);
            inputPlayer2.SetActive(true);
        }
        else
        {
            inputPlayer1.SetActive(false);
            inputPlayer2.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadStart -= InstantlyShow;
        SceneLoader.OnSceneLoadComplete -= Hide;
    }

}
