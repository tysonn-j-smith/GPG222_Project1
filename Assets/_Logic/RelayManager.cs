using Mono.Cecil.Cil;
using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField relayInput;

    public string relayCode = string.Empty;

    private bool isJoining = false;

    public async void JoinAsHost()
    {
        isJoining = true;
        string joinCode = await StartHostWithRelay(3, "udp");

        if (!string.IsNullOrEmpty(joinCode))
        {
            relayCode = joinCode;
        }
        else
        {
            Debug.LogError("Failed to start as host!");
            isJoining = false;
        }

        Debug.Log(joinCode.ToString());
        isJoining = false;
    }

    public async void JoinAsClient()
    {
        isJoining = true;
        if (relayInput.text == string.Empty)
        {
            Debug.Log("Please input a join code!");
            isJoining = false;
            return;
        }

        string code = relayInput.text.Trim();

        Debug.Log("Joining with join code: " + code);

        bool success = await StartClientWithRelay(code, "udp");
        isJoining = false;

        if (!success)
        {
            Debug.Log("Failed to join as client!");
            isJoining = false;
        }
    }

    public async Task<string> StartHostWithRelay(int maxConnections, string connectionType)
    {
        try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            var allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(AllocationUtils.ToRelayServerData(allocation, connectionType));
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            return NetworkManager.Singleton.StartHost() ? joinCode : null;
        }
        catch(Exception msg)
        {
            Debug.LogWarning(msg);
        }

        return null;
    }

    public async Task<bool> StartClientWithRelay(string joinCode, string connectionType)
    {
        try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            var allocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(AllocationUtils.ToRelayServerData(allocation, connectionType));
            return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
        }
        catch (Exception msg)
        {
            Debug.LogWarning(msg);
        }

        return false;
    }
}
