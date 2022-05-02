using System;

[Serializable]
public class Match
{
    public void Initialise()
    {
        round = 0;
        playerScore = new Score(5);
        enemyScore = new Score(5);
    }

    public int round;
    public Score playerScore;
    public Score enemyScore;

    public bool SetScore(PlayerType luser)
    {
        bool endScore = false;

        switch (luser)
        {
            case PlayerType.None:
                break;
            case PlayerType.Local:
                playerScore.ScoreReduction(out endScore);
                break;
            case PlayerType.Rival:
                enemyScore.ScoreReduction(out endScore);
                break;
        }

        return endScore;

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
