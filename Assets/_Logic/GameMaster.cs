using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    private PlayerScoreManager scoreManager;
    private ScoreBoardManager boardManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        scoreManager = GetComponent<PlayerScoreManager>();
        boardManager = GetComponent<ScoreBoardManager>();
    }

    private void Start()
    {
        scoreManager.OnScoresChanged += UpdateScoreBoard;
        UpdateScoreBoard();
    }

    private void OnDestroy()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoresChanged -= UpdateScoreBoard;
        }
    }

    public void RegisterPlayer(GameObject player, string newName)
    {
        //Reg player to all systems on join.
        if(scoreManager != null)
        {
            scoreManager.AddPlayer(player);
        }
    }

    public void UnregisterPlayer(GameObject player)
    {
        if(scoreManager != null)
        {
            scoreManager.RemovePlayer(player);
        }
    }

    public void UpdateScoreBoard()
    {
        if(scoreManager == null ||  boardManager == null)
        {
            return;
        }

        boardManager.PopulateScoreBoard(scoreManager.players.Values);
    }
}