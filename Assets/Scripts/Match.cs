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

    public RoundResult SetScore(PlayerSide luser)
    {
        if (playerScore.ScoreReduction())
        {
            return  RoundResult.EndMatch;
        }
        else
        {
            return  RoundResult.EndRound;
        }
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

        public bool ScoreReduction()
        {
            score--;
            if (score < 1)
            {
                return  true;
            }
            return false;
        }

    }

}
