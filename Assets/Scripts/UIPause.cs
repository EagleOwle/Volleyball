using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : UIPanel
{
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _ressumBtn;

    private void OnEnable()
    {
        StateMachine.SetState<PauseState>();
    }

    public override void Init()
    {
        base.Init();
        _restartBtn.onClick.AddListener(OnButtonRestart);
        _ressumBtn.onClick.AddListener(OnButtonRessum);
    }

    private void OnButtonRessum()
    {
        UIHud.OnChangePanel(UIPanelName.Game);
    }

    private void OnButtonRestart()
    {
        SceneManager.LoadScene(0);
    }
    
}
