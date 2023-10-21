using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : CanvasGroupUI
{
    public static UILoadingScreen Instance;

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
        SceneLoader.OnSceneLoadStart += InstantlyShow;
        SceneLoader.OnSceneLoadComplete += Hide;
        Invoke(nameof(Hide), Time.deltaTime);
    }

    public void InstantlyShow()
    {
        gameObject.SetActive(true);
        CanvasGroup.alpha = 1;
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadStart -= Show;
        SceneLoader.OnSceneLoadComplete -= Hide;
    }

}
