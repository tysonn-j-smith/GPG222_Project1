using UnityEngine;
using Unity.Netcode;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] private Weapon currentWeapon;

    public void ShootCall()
    {
        if (!IsOwner) 
        {
            return;
        }

        if (currentWeapon == null)
        {
            Debug.LogWarning("No Weapon found!");
            return;
        }

        ShootServerRpc();
    }

    public void ReloadCall()
    {
        if (!IsOwner)
        {
            return;
        }

        if (currentWeapon == null)
        {
            Debug.LogWarning("No Weapon found!");
            return;
        }

        ReloadServerRpc();
    }

    #region RPC
    [Rpc(SendTo.Server)]
    private void ShootServerRpc()
    {
        if(currentWeapon == null)
        {
            return;
        }

        currentWeapon.TryShootServer();
    }

    [Rpc(SendTo.Server)]
    private void ReloadServerRpc()
    {
        if (currentWeapon == null)
        {
            return;
        }

        currentWeapon.TryReloadServer();
    }
    #endregion
}
