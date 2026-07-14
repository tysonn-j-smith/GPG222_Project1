using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    [Header("NetworkUI Settings")]
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_Text playerCountText;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake()
    {
        /*startHostButton.onClick.AddListener(() =>
        {
            //RelayManager.Instance.StartHostWithRelay(4, "udp");
        });

        startClientButton.onClick.AddListener(() =>
        {
            //RelayManager.Instance.StartClientWithRelay(4, "udp");
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });*/
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
