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
    [SerializeField] private GameObject inputButtonPanel, inputJoystickPanel;
    [SerializeField] private UIFailPanel failPanel;
    [SerializeField] private Text hitCountText;
    [SerializeField] private Text scorePlayerText, scoreEnemyText;

    public void Start()
    {
        StateMachine.actionChangeState += OnChangeGameState;
        pauseBtn.onClick.AddListener(OnButtonPause);
        Game.Instance.actionUnitHit += ShowBallHitCount;
    }

    private void OnEnable()
    {
        switch (Preference.Singleton.inputType)
        {
            case InputType.button:
                inputJoystickPanel.SetActive(false);
                inputButtonPanel.SetActive(true);
                break;
            case InputType.joystick:
                inputJoystickPanel.SetActive(true);
                inputButtonPanel.SetActive(false);
                break;
        }
        
    }

    public void StartRound()
    {
        Game.Instance.actionRoundFail += OnFail;
        ShowBallHitCount(PlayerType.None, 0);
        failPanel.gameObject.SetActive(false);
        ShowScore();
    }

    private void OnChangeGameState(State next, State last)
    {
        if (next is GameState)
        {
            pauseBtn.gameObject.SetActive(true);
        }

        if (next is PauseState)
        {
            pauseBtn.gameObject.SetActive(false);
        }

        if (next is FallState)
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


    private void OnDestroy()
    {
        StateMachine.actionChangeState -= OnChangeGameState;
    }

    private void OnButtonPause()
    {
        UIHud.Instance.OnChangePanel(UIPanelName.Pause);
    }

    private void OnFail(RoundResult roundResult)
    {
        Game.Instance.actionRoundFail -= OnFail;

        ShowScore();
        failPanel.ShowMessage(roundResult);
        failPanel.gameObject.SetActive(true);
    }

}
