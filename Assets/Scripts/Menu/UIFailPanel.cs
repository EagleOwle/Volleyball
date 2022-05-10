using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFailPanel : MonoBehaviour
{
    [SerializeField] private Button closePanelBtn;
    [SerializeField] private Text buttonText;
    [SerializeField] private Text messageText;
    [SerializeField] private string winGameMessage = "You Win";
    [SerializeField] private string failGameMessage = "You Fail";
    [SerializeField] private string continueGameMessage = "Tup To Continue";
    [SerializeField] private string endGameMessage = "Tap To End";

    public void ShowMessage(RoundResult roundResult)
    {
        EventSystem.current.SetSelectedGameObject(closePanelBtn.gameObject);

        messageText.text =  FallMessage(roundResult);

        if (roundResult == RoundResult.WinningGame || roundResult == RoundResult.LoseGame)
        {
            closePanelBtn.onClick.RemoveAllListeners();
            Invoke(nameof(SetButtonEndGame), 1);
        }
        else
        {
            closePanelBtn.onClick.RemoveAllListeners();
            Invoke(nameof(SetButtonStartRound), 1);
        }
    }

    private string FallMessage(RoundResult roundResult)
    {
        string luserMessage = "Errore";

        switch (roundResult)
        {
            case RoundResult.None:
                luserMessage = "None";
                break;
            case RoundResult.FeedLoss:
                luserMessage = "Feed Loss";
                break;
            case RoundResult.PlayerFeedLoss:
                luserMessage = "Rival Attacks";// "Rival serving the ball";
                break;
            case RoundResult.RivalFeedLoss:
                luserMessage = "You Attacks";  //"You serving the ball";
                break;
            case RoundResult.Victory:
                luserMessage = "Victory";
                break;
            case RoundResult.Losing:
                luserMessage = "Losing";
                break;
            case RoundResult.WinningGame:
                luserMessage = "You Winning Games";
                break;
            case RoundResult.LoseGame:
                luserMessage = "You Lose Game";
                break;
        }

        return luserMessage;
    }

    private void SetButtonEndGame()
    {
        buttonText.text = endGameMessage;
        closePanelBtn.onClick.AddListener(Game.Instance.EndGame);
    }

    private void SetButtonStartRound()
    {
        buttonText.text = continueGameMessage;
        closePanelBtn.onClick.AddListener(Game.Instance.StartRound);
        closePanelBtn.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        buttonText.text = "Wait...";
        gameObject.SetActive(false);
    }
}
