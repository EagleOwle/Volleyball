using System;

[Serializable]
public class Match
{
    public Match()
    {
        round = new Round();
    }

    public Round round;
    public int score;
}
