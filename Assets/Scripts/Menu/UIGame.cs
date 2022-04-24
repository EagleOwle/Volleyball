using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIPanel
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private GameObject _fallPanel;
    [SerializeField] private Text hitBallCountText;

    private void OnEnable()
    {
        StateMachine.SetState<GameState>();
        Game.Instance.OnRoundFall += OnFall;
        _fallPanel.SetActive(false);
        _pauseBtn.onClick.AddListener(OnButtonPause);

        Ball ball = GameObject.FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.ActionHitCount += ShowBallHitCount;
            ShowBallHitCount(ball.HitCount);
        }

    }

    private void ShowBallHitCount(int value)
    {
        if (StateMachine.currentState is GameState)
        {
            hitBallCountText.text = value.ToString();
        }
    }

    private void OnDisable()
    {
        if(Game.Instance!=null)  Game.Instance.OnRoundFall -= OnFall;
        _pauseBtn.onClick.RemoveListener(OnButtonPause);

        Ball ball = GameObject.FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.ActionHitCount -= ShowBallHitCount;

        }
    }

    private void OnButtonPause()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Pause);
    }

    private void OnFall()
    {
        _fallPanel.SetActive(true);
    }
}
