using System;

[Serializable]
public class Match
{
    public void Initialise(int roundCount)
    {
        round = 0;
        playerScore = new Score(roundCount);
        enemyScore = new Score(roundCount);
    }

    public int round;
    public Score playerScore;
    public Score enemyScore;

    private PlayerType lastLuser = PlayerType.None;

    public RoundResult SetScore(PlayerType luser)
    {
        RoundResult roundResult = RoundResult.None;
        bool endMatch;

        if (luser == lastLuser)
        {
            switch (luser)
            {
                case PlayerType.None:
                    roundResult = RoundResult.None;
                    break;

                case PlayerType.Local:
                    playerScore.ScoreReduction(out endMatch);

                    if (endMatch)
                    {
                        roundResult = RoundResult.LoseGame;
                    }
                    else
                    {
                        roundResult = RoundResult.Losing;
                    }
                    break;

                case PlayerType.Rival:
                    enemyScore.ScoreReduction(out endMatch);

                    if (endMatch)
                    {
                        roundResult = RoundResult.WinningGame;
                    }
                    else
                    {
                        roundResult = RoundResult.Victory;
                    }

                    break;
            }
        }
        else
        {
            switch (luser)
            {
                case PlayerType.None:
                    roundResult = RoundResult.FeedLoss;
                    break;
                case PlayerType.Local:
                    roundResult = RoundResult.PlayerFeedLoss;
                    break;
                case PlayerType.Rival:
                    roundResult = RoundResult.RivalFeedLoss;
                    break;
            }

        }

        lastLuser = luser;

        return roundResult;

    }

    [Serializable]
    public class Round
    {
        public int roundNumber = 0;
    }

    [Serializable]
    public class Score
    {
        public Score(int score)
        {
            this.score = score;
        }

        public PlayerType playerType;
        public int score { get; private set; }
        public void ScoreReduction(out bool endScore)
        {
            endScore = false;
            score--;
            if (score < 1)
            {
                endScore = true;
            }
        }
    }

}
