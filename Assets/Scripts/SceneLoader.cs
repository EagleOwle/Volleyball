using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum SceneType
    {
        InMenu,
        InGame
    }

    public static SceneLoader Instance;
    public static Action OnSceneLoadStart;
    public static Action OnSceneLoadComplete;
    public static SceneType currentSceneType;

    [SerializeField] private int _menuSceneIndex = 0;
    [SerializeField] private int _levelSceneIndex = 1;

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

    public void LoadLevel(int _levelSceneIndex)
    {
        Load(_levelSceneIndex, SceneType.InGame);
    }

    public void LoadLevel()
    {
        Load(_levelSceneIndex, SceneType.InGame);
    }

    public void LoadMenu()
    {
        Load(_menuSceneIndex, SceneType.InMenu);
    }

    private void Load(int index, SceneType type)
    {
        OnSceneLoadStart?.Invoke();
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.completed += AsyncLoadComplete;
        currentSceneType = type;
    }

    private void AsyncLoadComplete(AsyncOperation operation)
    {
        operation.completed -= AsyncLoadComplete;
        OnSceneLoadComplete?.Invoke();
    }


}
