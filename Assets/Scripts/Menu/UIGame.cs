using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    private static UIGame _instance;
    public static UIGame Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIGame>();
            }

            return _instance;
        }
    }

    [SerializeField] private Button pauseBtn;
    [SerializeField] private UIFailPanel failPanel;
    [SerializeField] private Text hitCountText;
    [SerializeField] private Text roundText;
    [SerializeField] private Text scorePlayerText, scoreEnemyText;

    //private Ball ball;

    public void Start()
    {
        StateMachine.actionChangeState += OnChangeGameState;
        pauseBtn.onClick.AddListener(OnButtonPause);
    }

    public void StartRound(Ball ball)
    {
        StartRound();
    }

    public void StartRound()
    {
        Game.Instance.actionRoundFail += OnFail;
        ShowBallHitCount(PlayerType.None, 0);
        failPanel.gameObject.SetActive(false);
        roundText.text = "Round \n" + Game.Instance.match.round.ToString();
        ShowScore();
    }

    private void OnChangeGameState(State value)
    {
        if (value is GameState)
        {
            pauseBtn.gameObject.SetActive(true);
        }

        if (value is PauseState)
        {
            pauseBtn.gameObject.SetActive(false);
        }

        if (value is FallState)
        {
            pauseBtn.gameObject.SetActive(false);
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
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    private void OnDestroy()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    public void OnButtonJumpDown()
    {
        InputHandler.Instance.OnButtonJumpDown();
    }

    public void OnButtonJumpUp()
    {
        InputHandler.Instance.OnButtonJumpUp();
    }

    public void OnButtonLeftDown()
    {
        InputHandler.Instance.OnButtonLeftDown();
    }

    public void OnButtonLeftUp()
    {
        InputHandler.Instance.OnButtonLeftUp();
    }

    public void OnButtonRightDown()
    {
        InputHandler.Instance.OnButtonRightDown();
    }

    public void OnButtonRightUp()
    {
        InputHandler.Instance.OnButtonRightUp();
    }

    private void OnButtonPause()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Pause);
    }

    private void OnFail(RoundResult roundResult)
    {
        //if (ball != null)
        //{
        //    ball.actionUnitHit -= ShowBallHitCount;
        //}

        Game.Instance.actionRoundFail -= OnFail;

        ShowScore();
        failPanel.ShowMessage(roundResult);
        failPanel.gameObject.SetActive(true);
    }

}
