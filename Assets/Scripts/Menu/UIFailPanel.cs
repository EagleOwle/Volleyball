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

    public void ShowMessage(bool endGame, PlayerType luser)
    {
        EventSystem.current.SetSelectedGameObject(closePanelBtn.gameObject);

        messageText.text = FallMessage(endGame, luser);

        if (endGame == true)
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

    private string FallMessage(bool endGame, PlayerType luser)
    {
        string luserMessage = "End Round";

        switch (luser)
        {
            case PlayerType.None:
                break;
            case PlayerType.Local:
                if (endGame)
                {
                    luserMessage = "You Fail";
                }
                else
                {
                    luserMessage = "Fail";
                }
                break;
            case PlayerType.Rival:
                if (endGame)
                {
                    luserMessage = "You Win";
                }
                else
                {
                    luserMessage = "Win";
                }
                break;
            default:
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
