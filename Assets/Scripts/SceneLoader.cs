using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum SceneType
    {
        InMenu,
        InGame
    }

    private static int _menuSceneIndex = 0;
    private static int _levelSceneIndex = 1;
    public static Action OnSceneLoadStart;
    public static Action OnSceneLoadComplete;

    public static SceneType currentSceneType;

    public static void LoadLevel()
    {
        Load(_levelSceneIndex, SceneType.InGame);
    }

    public static void LoadMenu()
    {
        Load(_menuSceneIndex, SceneType.InMenu);
    }

    private static void Load(int index, SceneType type)
    {
        OnSceneLoadStart?.Invoke();
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.completed += AsyncLoadComplete;
        currentSceneType = type;
    }

    private static void AsyncLoadComplete(AsyncOperation operation)
    {
        operation.completed -= AsyncLoadComplete;
        OnSceneLoadComplete?.Invoke();
    }

    
}
