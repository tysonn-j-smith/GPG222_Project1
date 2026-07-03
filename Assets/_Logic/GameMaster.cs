using NUnit.Framework;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}


public class PlayerScoreManager : MonoBehaviour
{

}