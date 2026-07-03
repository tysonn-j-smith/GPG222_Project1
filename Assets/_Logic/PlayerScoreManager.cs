using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public Dictionary<GameObject, PlayerInfoHandler> players = new();

    private void Start()
    {
        GameMaster.Instance.UpdateScoreBoard();
    }

    public void AddPlayer(GameObject player, string newName)
    {
        PlayerInfoHandler info = player.GetComponent<PlayerInfoHandler>();

        if(info == null)
        {
            return;
        }

        info.SetName(newName);
        info.OnKillConfirmed += HandleKill;

        if (!players.ContainsKey(player))
        {
            players.Add(player, info);
        }

        Debug.Log($"{info.GetName()} joined the game.");
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
        Debug.Log($"Player {player.GetName()} | Score: {player.GetScore()}");
        GameMaster.Instance.UpdateScoreBoard();
    }
}