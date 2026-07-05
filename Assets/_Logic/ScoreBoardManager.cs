using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] private Transform boardContainer;
    [SerializeField] private GameObject inputPrefab;

    public void PopulateScoreBoard(IEnumerable<PlayerInfoHandler> players)
    {
        ClearBoard();

        foreach (PlayerInfoHandler info in players)
        {
            AddInput(info);
        }
    }

    private void ClearBoard()
    {
        foreach (Transform child in boardContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddInput(PlayerInfoHandler info)
    {
        GameObject obj = Instantiate(inputPrefab, boardContainer);
        UpdateInputText(obj, info);
    }

    private void UpdateInputText(GameObject obj, PlayerInfoHandler info)
    {
        TMP_Text inputText = obj.GetComponentInChildren<TMP_Text>();
        if(inputText != null)
        {
            inputText.text = $"{info.GetName()} || Score: {info.GetScore()}";
        }
    }
}
