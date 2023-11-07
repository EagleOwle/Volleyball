using UnityEngine;
using UnityEngine.UI;
using System;

public class UIPause : UIPanel
{
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _ressumBtn;
    [SerializeField] private Button _prefBtn;
    [SerializeField] private GameObject preferencePanel;

    private void OnEnable()
    {
        StateMachine.SetState<PauseState>();
        HidePreferencePanel();
    }

    public override void Init()
    {
        base.Init();
        _restartBtn.onClick.AddListener(OnButtonRestart);
        _ressumBtn.onClick.AddListener(OnButtonRessum);
        _prefBtn.onClick.AddListener(ShowPreferencePanel);
    }

    private void OnButtonRessum()
    {
        UIHud.Instance.OnChangePanel(UIPanelName.Timer);
        gameObject.SetActive(false);
    }

    private void OnButtonRestart()
    {
        SceneLoader.Instance.LoadMenu();
    }

    private void ShowPreferencePanel()
    {
        preferencePanel.SetActive(true);
    }

    public void HidePreferencePanel()
    {
        preferencePanel.SetActive(false);
    }

}
