using System;

[System.Serializable]
public class Match
{
    public void Initialise(int roundCount, string player1Name, string player2Name)
    {
        playerLeft = new Player(player1Name, PlayerSide.Left, roundCount);
        playerRight = new Player(player2Name, PlayerSide.Right, roundCount);
        lastLuser = null;
    }

    private Player playerLeft;
    private Player playerRight;
    private Player lastLuser;

    Player tmpPlayer;
    RoundResult roundResult;
    bool endMatch;

    public RoundResult SetLuser(PlayerSide luser)
    {
        roundResult = RoundResult.None;
        endMatch = false;
        tmpPlayer = GetPlayer(luser);

        if (tmpPlayer == lastLuser)
        {
            tmpPlayer.ScoreReduction(out endMatch);
            roundResult = RoundResult.Goal;

            if(endMatch)
            {
                roundResult = RoundResult.EndMatch;
            }
        }
        else
        {
            roundResult = RoundResult.EndRound;
        }

        lastLuser = tmpPlayer;
        return roundResult;
    }

    public Player LastLuser()
    {
        if (lastLuser == null)
        {
            return null;
        }
        else
        {
            return lastLuser;
        }
    }

    public Player LastWinner()
    {
        if (lastLuser == null)
        {
            return null;
        }
        else
        {
            if (lastLuser == playerLeft)
            {
                return playerRight;
            }
            else
            {
                return playerLeft;
            }
        }
    }

    public int GetScore(PlayerSide side)
    {
        switch (side)
        {
            case PlayerSide.None:
                return 0;
            case PlayerSide.Left:
                return playerLeft.score;
            case PlayerSide.Right:
                return playerRight.score;
            default:
                return 0;
        }
    }

    private Player GetPlayer(PlayerSide side)
    {
        switch (side)
        {
            case PlayerSide.None:
                return null;

            case PlayerSide.Left:
                return playerLeft;

            case PlayerSide.Right:
                return playerRight;

            default:
                return null;
        }
    }

    [System.Serializable]
    public class Player
    {
        public Player(String name, PlayerSide playerSide, int score)
        {
            this.name = name;
            this.playerSide = playerSide;
            this.score = score;
        }

        public PlayerSide playerSide;
        public string name;

        public int score { get; private set; }

        public void ScoreReduction(out bool endScore)
        {
            endScore = false;
            score--;
            if (score <= 0)
            {
                endScore = true;
            }
        }

    }

}
