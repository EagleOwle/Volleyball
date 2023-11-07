using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIPanel
{
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _preferenceBtn;
    [SerializeField] private Button _quitBtn;

    public override void Init()
    {
        base.Init();
        _startGameBtn.onClick.AddListener(OnButtonStart);
        _preferenceBtn.onClick.AddListener(OnButtonPreference);
        _quitBtn.onClick.AddListener(OnButtonQuit);
    }

    private void OnButtonStart()
    {
        //SceneLoader.Instance.LoadLevel();
        UIHud.Instance.OnChangePanel(UIPanelName.ScenePreference);
    }

    private void OnButtonPreference()
    {
        UIHud.Instance.OnChangePanel(UIPanelName.Preference);
    }

    private void OnButtonConnect()
    {
        UIHud.Instance.OnChangePanel(UIPanelName.Connect);
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
