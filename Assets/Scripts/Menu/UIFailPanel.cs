using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFailPanel : MonoBehaviour
{
    [SerializeField] private Button closePanelBtn;
    [SerializeField] private Text buttonText;
    [SerializeField] private Text messageText;

    private int localeId;
    private LocalizationData localizationData; // Объявляем объект LocalizationData

    private void Start()
    {
        localeId = LocalSelector.Instance.CurrentLocaleId;
        EventSystem.current.SetSelectedGameObject(closePanelBtn.gameObject);
        ShowMessage(RoundResult.None); // Пример вызова с RoundResult.None
    }

    public void ShowMessage(RoundResult roundResult)
    {
        localizationData = new LocalizationData(GetLanguageCodeForLocale(localeId)); // Создаем объект LocalizationData
        messageText.text = localizationData.GetMessageForRoundResult(roundResult); // Используем метод локализации
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

    private void SetButtonEndGame()
    {
        buttonText.text = localizationData.GetButtonEndGameText(LocalizationData.ButtonText.SetButtonEndGame); // Используем метод локализации
        closePanelBtn.onClick.AddListener(Game.Instance.EndGame);
    }

    private void SetButtonStartRound()
    {
        buttonText.text = localizationData.GetButtonEndGameText(LocalizationData.ButtonText.SetButtonStartRound); // Используем метод локализации
        closePanelBtn.onClick.AddListener(Game.Instance.RestartRound);
        closePanelBtn.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        buttonText.text = localizationData.GetButtonEndGameText(LocalizationData.ButtonText.Hide); // Используем метод локализации
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
