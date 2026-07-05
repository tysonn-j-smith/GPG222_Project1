using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    [Header("NetworkUI Settings")]
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TMP_Text playerCountText;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    private void Update()
    {
        UpdatePlayerCountUI();
    }

    private void UpdatePlayerCountUI()
    {
        if (playerCountText != null)
        {
            playerCountText.text = "Players: " + playersNum.Value.ToString();
        }

        if (!IsServer)
        {
            return;
        }

        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
