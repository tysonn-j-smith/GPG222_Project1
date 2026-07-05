using System;
using UnityEngine;
using Unity.Netcode;

public class PlayerInfoHandler : NetworkBehaviour
{
    private int currentScore = 0;
    private string playerName = string.Empty;

    public event Action<PlayerInfoHandler> OnKillConfirmed;

    public void SetName()
    {
        playerName = "Player: " + NetworkManager.Singleton.LocalClientId.ToString();
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
