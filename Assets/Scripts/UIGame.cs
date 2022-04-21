using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIPanel
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private GameObject _fallPanel;

    private void OnEnable()
    {
        StateMachine.SetState<GameState>();
        Game.Instance.OnRoundFall += OnFall;
        _fallPanel.SetActive(false);
        _pauseBtn.onClick.AddListener(OnButtonPause);
    }

    private void OnDisable()
    {
        if(Game.Instance!=null)  Game.Instance.OnRoundFall -= OnFall;
        _pauseBtn.onClick.RemoveListener(OnButtonPause);
    }

    private void OnButtonPause()
    {
        UIHud.OnChangePanel(UIPanelName.Pause);
    }

    private void OnFall()
    {
        _fallPanel.SetActive(true);
    }
}
