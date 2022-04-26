using System;

[Serializable]
public class Match
{
    public void Initialise()
    {
        round = 0;
        playerScore = new Score(15);
        enemyScore = new Score(15);
    }

    public int round;
    public Score playerScore;
    public Score enemyScore;
    public Action ActionSetScore;

    public void SetScore(PlayerType luser)
    {
        switch (luser)
        {
            case PlayerType.None:
                break;
            case PlayerType.Local:
                playerScore.score--;
                break;
            case PlayerType.Rival:
                enemyScore.score--;
                break;
        }

        ActionSetScore?.Invoke();
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
        public int score;
    }
}
