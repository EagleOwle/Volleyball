using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIPanel
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private UIFallPanel fallPanel;
    [SerializeField] private Text hitCountText;
    [SerializeField] private Text roundText;
    [SerializeField] private Text scorePlayerText, scoreEnemyText;

    private void OnEnable()
    {
        StateMachine.SetState<GameState>();
        Game.Instance.ActionRoundFall += OnFall;
        Game.Instance.match.ActionSetScore += ShowScore;
        Invoke(nameof(ShowScore), Time.deltaTime);

        fallPanel.ShowMessage();
        fallPanel.gameObject.SetActive(false);

        pauseBtn.onClick.AddListener(OnButtonPause);

        Ball ball = GameObject.FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.ActionUnitHit += ShowBallHitCount;
            ShowBallHitCount(PlayerType.None, 0);
        }

    }

    private void ShowScore()
    {
        scorePlayerText.text = Game.Instance.match.playerScore.score.ToString();
        scoreEnemyText.text = Game.Instance.match.enemyScore.score.ToString();
    }

    private void ShowBallHitCount(PlayerType playerType, int hitCount)
    {
        if (StateMachine.currentState is GameState)
        {
            hitCountText.text = hitCount.ToString();
        }
    }

    private void OnDisable()
    {
        if (Game.Instance != null)
        {
            Game.Instance.ActionRoundFall -= OnFall;
            Game.Instance.match.ActionSetScore -= ShowScore;
        }

        pauseBtn.onClick.RemoveListener(OnButtonPause);

        Ball ball = GameObject.FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.ActionUnitHit -= ShowBallHitCount;

        }
    }

    private void OnButtonPause()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Pause);
    }

    private void OnFall(bool endMatch)
    {
        fallPanel.ShowMessage(endMatch);
        fallPanel.gameObject.SetActive(true);
    }
}
