using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFallPanel : MonoBehaviour
{
    [SerializeField] private Button closePanelBtn;
    [SerializeField] private Text buttonText;
    [SerializeField] private string continueGameMessage = "Tup To Continue";
    [SerializeField] private string endGameMessage = "Tup To End";

    public void ShowMessage(bool endGame = false)
    {
        if(endGame == true)
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

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
