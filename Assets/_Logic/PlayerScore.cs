using System;
using System.Collections.Generic;

[Serializable]
public class PlayerScore
{
    public string playerName;
    public List<int> scores = new();

    public void AddScore(int x)
    {
        scores.Add(x);
    }

    public int GetHighestScore()
    {
        if(scores.Count == 0)
        {
            return 0;
        }

        scores.Sort((a, b) => b.CompareTo(a));

        return scores[0];
    }

    public List<int> GetSortedScores()
    {
        List<int> sorted = new List<int>(scores);
        sorted.Sort((a, b) => b.CompareTo(a));

        return sorted;
    }
}
