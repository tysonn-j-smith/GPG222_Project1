using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    private PlayerScoreManager scoreManager;

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
            scoreManager.
        }
    }

}


public class PlayerScoreManager : MonoBehaviour
{
    public void AddPlayer(GameObject player, string newName)
    {
        PlayerInfoHandler pc = player.GetComponent<PlayerInfoHandler>();
        if(pc != null)
        {
            pc.SetName(newName);
        }
    }

    public void UpdateScore(GameObject player)
    {
        PlayerInfoHandler pc = player.GetComponent<PlayerInfoHandler>();
        if(pc != null)
        {
            pc.IncreaseScore();

            Debug.Log(pc.GetScore());
        }
    }
}