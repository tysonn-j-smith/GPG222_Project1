using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    private PlayerScoreManager scoreManager;

    [SerializeField] private TMP_Text scoreBoardTestText;

    private void Awake()
    {
        Instance = this;

        scoreManager = GetComponent<PlayerScoreManager>();
    }

    public void RegisterPlayer(GameObject player, string newName)
    {
        //Reg player to all systems on join.
        if(scoreManager != null)
        {
            scoreManager.AddPlayer(player, newName);
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
        if (scoreBoardTestText != null)
        {
            scoreBoardTestText.text = "";

            foreach(PlayerInfoHandler player in scoreManager.players.Values)
            {
                scoreBoardTestText.text += player.GetName() + " : " + player.GetScore() + "\n";
            }
        }
    }
}