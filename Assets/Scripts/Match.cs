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

    private PlayerSide lastLuser = PlayerSide.None;

    public RoundResult SetScore(PlayerSide luser)
    {
        RoundResult roundResult = RoundResult.None;
        bool endMatch;

        if (luser == lastLuser)
        {
            switch (luser)
            {
                case PlayerSide.None:
                    roundResult = RoundResult.None;
                    break;

                case PlayerSide.Left:
                    playerScore.ScoreReduction(out endMatch);

                    if (endMatch)
                    {
                        roundResult = RoundResult.EndMatch;
                    }
                    //else
                    //{
                    //    roundResult = RoundResult.Losing;
                    //}
                    break;

                case PlayerSide.Right:
                    enemyScore.ScoreReduction(out endMatch);

                    if (endMatch)
                    {
                        roundResult = RoundResult.EndMatch;
                    }
                    //else
                    //{
                    //    roundResult = RoundResult.Victory;
                    //}

                    break;
            }
        }
        else
        {
            switch (luser)
            {
                case PlayerSide.None:
                    roundResult = RoundResult.FeedLoss;
                    break;
                case PlayerSide.Left:
                    roundResult = RoundResult.Player1FeedLoss;
                    break;
                case PlayerSide.Right:
                    roundResult = RoundResult.Player2FeedLoss;
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

        public PlayerSide playerType;
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
