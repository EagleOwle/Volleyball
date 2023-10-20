public class LocalizationData
{
    public string LanguageCode { get; private set; }

    // Здесь вы можете добавить все необходимые поля для локализации.
    public string Name { get; private set; }
    public string FeedLoss { get; private set; }
    public string PlayerFeedLoss { get; private set; }
    public string RivalFeedLoss { get; private set; }
    public string Victory { get; private set; }
    public string Losing { get; private set; }
    public string WinningGame { get; private set; }
    public string LoseGame { get; private set; }
    public string ButtonEndGame { get; private set; }
    public string ButtonStartRound { get; private set; }
    public string ButtonHide { get; private set; }

    public enum ButtonText
    {
        SetButtonStartRound,
        SetButtonEndGame,
        Hide
    }

    public LocalizationData(string languageCode)
    {
        LanguageCode = languageCode;

        // Инициализируйте все поля локализации для данной локали.
        if (languageCode == "ru")
        {
            Name = "Ничья";
            FeedLoss = "Передача Мяча";
            PlayerFeedLoss = "Атаки Соперника";
            RivalFeedLoss = "Ваши Атаки";
            Victory = "Победа";
            Losing = "Поражение";
            WinningGame = "Вы Выиграли Игру";
            LoseGame = "Вы Проиграли Игру";
            ButtonEndGame = "Нажмите, чтобы закончить";
            ButtonStartRound = "Нажмите, чтобы продолжить";
            ButtonHide = "Подождите";
        }
        else if (languageCode == "zh")
        {
            Name = "畫";
            FeedLoss = "傳球";
            PlayerFeedLoss = "對手的攻擊";
            RivalFeedLoss = "你的攻擊";
            Victory = "勝利";
            Losing = "打敗";
            WinningGame = "你贏得了比賽";
            LoseGame = "你輸了遊戲";
            ButtonEndGame = "點擊結束";
            ButtonStartRound = "點擊繼續";
            ButtonHide = "等待";
        }
        else if (languageCode == "en")
        {
            Name = "Draw";
            FeedLoss = "Pass Ball";
            PlayerFeedLoss = "Opponent's Attacks";
            RivalFeedLoss = "Your Attacks";
            Victory = "Victory";
            Losing = "Defeat";
            WinningGame = "You Won the Game";
            LoseGame = "You Lost the Game";
            ButtonEndGame = "Tap To End";
            ButtonStartRound = "Tap To Continue";
            ButtonHide = "Wait";
        }
        // Добавьте другие локали и инициализации по мере необходимости.
    }

    public string GetMessageForRoundResult(RoundResult roundResult)
    {
        switch (roundResult)
        {
            case RoundResult.None:
                return Name;
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
            default:
                return "Error";
        }
    }
    public string GetButtonEndGameText(ButtonText buttonText)
    {
        switch (buttonText)
        {
            case ButtonText.SetButtonStartRound:
                return ButtonStartRound;
            case ButtonText.SetButtonEndGame:
                return ButtonEndGame;
            case ButtonText.Hide:
                return ButtonHide;
            default:
                return "Error";
        }
    }
}
