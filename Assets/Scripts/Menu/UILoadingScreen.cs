using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : CanvasGroupUI
{
    public static UILoadingScreen Instance;

    [SerializeField] private GameObject inputPlayer1, inputPlayer2;
    [SerializeField]private float showFirstLoad = 3;

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
        SceneLoader.OnSceneLoadStart += InstantlyShow;
        SceneLoader.OnSceneLoadComplete += Hide;
        Invoke(nameof(Hide), showFirstLoad);
    }

    public void InstantlyShow(int sceneIndex)
    {
        gameObject.SetActive(true);
        CanvasGroup.alpha = 1;

        if(sceneIndex != 0)
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
