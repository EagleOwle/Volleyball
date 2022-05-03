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
        SceneLoader.OnSceneLoadStart += Show;
        SceneLoader.OnSceneLoadComplete += Hide;
        Invoke(nameof(Hide), Time.deltaTime);
    }

    public override void Show()
    {
        base.Show();
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        while (true)
        {
            //Play Animation

            yield return null;
        }
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadStart -= Show;
        SceneLoader.OnSceneLoadComplete -= Hide;
    }

}
