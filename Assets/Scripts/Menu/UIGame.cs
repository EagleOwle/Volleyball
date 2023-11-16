using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
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
    private static UIGame _instance;

    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject inputButtonPanel, inputJoystickPanel;
    [SerializeField] private UIFailPanel failPanel;
    [SerializeField] private Text hitCountText, scorePlayer1Text, scorePlayer2Text;
    [SerializeField] private UIGameMessage gameMessage;
    
    [Space()]
    [SerializeField] private LocalizedString BallDraw;
    [SerializeField] private LocalizedString Serves;
    [SerializeField] private LocalizedString Winner;

    public void Start()
    {
        StateMachine.actionChangeState += OnChangeGameState;
        pauseBtn.onClick.AddListener(OnButtonPause);
        Game.Instance.actionUnitHit += ShowBallHitCount;
    }

    //private void ShowUIInputPanel()
    //{
    //    switch (Preference.Singleton.player[0].inputType)
    //    {
    //        case InputType.button:
    //            inputJoystickPanel.SetActive(false);
    //            inputButtonPanel.SetActive(true);
    //            break;
    //        case InputType.joystick:
    //            inputJoystickPanel.SetActive(true);
    //            inputButtonPanel.SetActive(false);
    //            break;
    //    }
    //}

    public void StartRound()
    {
        Game.Instance.actionRoundFail += OnFail;
        ShowBallHitCount(PlayerSide.None, 0);
        failPanel.gameObject.SetActive(false);
        ShowScore();

        if (Game.Instance.Luser == null)
        {
            gameMessage.ShowLocalizedMessage(BallDraw);
        }
        else
        {
            gameMessage.ShowLocalizedMessage(Serves, Game.Instance.Luser.name);
        }
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
        scorePlayer1Text.text = Game.Instance.match.GetScore(PlayerSide.Left).ToString();
        scorePlayer2Text.text = Game.Instance.match.GetScore(PlayerSide.Right).ToString();
    }

    private void ShowBallHitCount(PlayerSide playerType, int hitCount)
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

        if(roundResult == RoundResult.EndMatch)
        {
            gameMessage.ShowLocalizedMessage(Winner, Game.Instance.match.LastWinner().name);
        }
    }

}
