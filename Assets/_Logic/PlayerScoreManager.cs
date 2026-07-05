using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public event Action OnScoresChanged;

    public Dictionary<GameObject, PlayerInfoHandler> players = new();

    private void Start()
    {
        GameMaster.Instance.UpdateScoreBoard();
    }

    public void AddPlayer(GameObject player)
    {
        PlayerInfoHandler info = player.GetComponent<PlayerInfoHandler>();

        if(info == null)
        {
            return;
        }

        info.SetName();
        info.OnKillConfirmed += HandleKill;

        if (!players.ContainsKey(player))
        {
            players.Add(player, info);
        }

        Debug.Log($"{info.GetName()} joined the game and is signed up to Score Manager.");
    }

    public void RemovePlayer(GameObject player)
    {
        PlayerInfoHandler info = player.GetComponent<PlayerInfoHandler>();

        if(info == null)
        {
            return;
        }

        info.OnKillConfirmed -= HandleKill;

        if (players.ContainsKey(player))
        {
            players.Remove(player);
        }
    }

    private void HandleKill(PlayerInfoHandler player)
    {
        if(player == null)
        {
            return;
        }

        OnScoresChanged?.Invoke();
    }
}