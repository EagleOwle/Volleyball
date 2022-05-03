using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFallPanel : MonoBehaviour
{
    [SerializeField] private Button closePanelBtn;
    [SerializeField] private Text buttonText;
    [SerializeField] private Text messageText;
    [SerializeField] private string winGameMessage = "You Win";
    [SerializeField] private string fallGameMessage = "You Fall";
    [SerializeField] private string continueGameMessage = "Tup To Continue";
    [SerializeField] private string endGameMessage = "Tup To End";

    public void ShowMessage(bool endGame, PlayerType luser)
    {
        EventSystem.current.SetSelectedGameObject(closePanelBtn.gameObject);

        messageText.text = FallMessage(endGame, luser);

        if (endGame == true)
        {
            buttonText.text = endGameMessage;
            closePanelBtn.onClick.RemoveAllListeners();
            closePanelBtn.onClick.AddListener(Game.Instance.EndGame);
        }
        else
        {
            buttonText.text = continueGameMessage;
            closePanelBtn.onClick.RemoveAllListeners();
            closePanelBtn.onClick.AddListener(Game.Instance.StartRound);
            closePanelBtn.onClick.AddListener(Hide);
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
                    luserMessage = "You Fall";
                }
                else
                {
                    luserMessage = "Fall";
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

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
