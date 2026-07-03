using System;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour
{
    private int currentScore = 0;
    private string playerName = string.Empty;

    public event Action<PlayerInfoHandler> OnKillConfirmed;

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

    public int GetScore()
    {
        return currentScore;
    }

    public void ConfirmKill()
    {
        currentScore++;
        OnKillConfirmed?.Invoke(this);
    }
}
