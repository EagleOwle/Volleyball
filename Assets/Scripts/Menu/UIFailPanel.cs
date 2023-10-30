using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using UnityEngine.Localization;

public class UIFailPanel : MonoBehaviour
{
    [SerializeField] private Button closePanelBtn;
    [SerializeField] private Text buttonText;
    //[SerializeField] private Text messageText;

    [SerializeField] private LocalizedString None;
    [SerializeField] private LocalizedString FeedLoss;
    [SerializeField] private LocalizedString PlayerFeedLoss;
    [SerializeField] private LocalizedString RivalFeedLoss;
    [SerializeField] private LocalizedString Victory;
    [SerializeField] private LocalizedString Losing;
    [SerializeField] private LocalizedString WinningGame;
    [SerializeField] private LocalizedString LoseGame;
    [SerializeField] private LocalizeStringEvent stringEvent;


    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(closePanelBtn.gameObject);
        ShowMessage(RoundResult.None); // Пример вызова с RoundResult.None
    }

    public void ShowMessage(RoundResult roundResult)
    {
        stringEvent.StringReference = GetLocalizedString(roundResult);
        closePanelBtn.onClick.RemoveAllListeners();

        if (roundResult == RoundResult.WinningGame || roundResult == RoundResult.LoseGame)
        {
            SetButtonEndGame(); // Вызываем метод для кнопки завершения игры
        }
        else
        {
            SetButtonStartRound(); // Вызываем метод для кнопки начала раунда
        }
    }

    private LocalizedString GetLocalizedString(RoundResult value)
    {
        switch (value)
        {
            case RoundResult.None:
                return None;
            case RoundResult.FeedLoss:
                return FeedLoss;
            case RoundResult.PlayerFeedLoss:
                return PlayerFeedLoss;
            case RoundResult.RivalFeedLoss:
                return RivalFeedLoss;
            case RoundResult.Victory:
                return Victory;
            case RoundResult.Losing:
                return Losing;
            case RoundResult.WinningGame:
                return WinningGame;
            case RoundResult.LoseGame:
                return LoseGame;
        }

        return null;
    }
    private void SetButtonEndGame()
    {
        closePanelBtn.onClick.AddListener(Game.Instance.EndGame);
    }

    private void SetButtonStartRound()
    {
        closePanelBtn.onClick.AddListener(Game.Instance.RestartRound);
        closePanelBtn.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // Метод, который преобразует localeId в код языка
    private string GetLanguageCodeForLocale(int localeId)
    {
        // Реализуйте логику преобразования, если требуется
        if (localeId == 0)
        {
            return "ru";
        }
        else if (localeId == 2)
        {
            return "zh";
        }
        else
        {
            return "en";
        }
    }
}
