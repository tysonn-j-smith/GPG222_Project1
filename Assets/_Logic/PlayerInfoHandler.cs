using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour
{
    private int currentScore = 0;
    private string playerName = string.Empty;

    private void OnDestroy()
    {
        currentScore = 0;
        playerName = string.Empty;
    }

    public void SetName(string newName)
    {
        playerName = newName;
    }

    public string GetName()
    {
        return playerName;
    }

    public void IncreaseScore()
    {
        currentScore++;
    }

    public int GetScore()
    {
        return currentScore;
    }
}
