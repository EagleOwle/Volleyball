using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : CanvasGroupUI
{
    private static UILoadingScreen instance;
    public static UILoadingScreen Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UILoadingScreen>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
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
