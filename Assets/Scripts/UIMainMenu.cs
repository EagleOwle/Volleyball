using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    private static UIMainMenu _instance;
    public static UIMainMenu Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIMainMenu>();
            }

            return _instance;
        }
    }

    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _preferenceBtn;
    [SerializeField] private Button _quitBtn;

    private void Start()
    {
        _startGameBtn.onClick.AddListener(OnButtonStart);
        _preferenceBtn.onClick.AddListener(OnButtonPreference);
        _quitBtn.onClick.AddListener(OnButtonQuit);
    }

    private void OnButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    //public async void SpinAndDisableButton()
    //{
    //    while (ani.isPlaying == true)
    //    {
    //        await Task.Yield();
    //    }

    //    SceneManager.LoadScene(1);
    //}

    private void OnButtonPreference()
    {

    }

    private void OnButtonQuit()
    {
        QuitApplication();
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit();
#elif UNITY_IOS
         Application.Quit();
#else
         Application.Quit();
#endif
    }

}
