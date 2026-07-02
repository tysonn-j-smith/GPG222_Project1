using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;

    public void ShootCall()
    {
        if(currentWeapon == null)
        {
            Debug.LogWarning("No Weapon found!");
            return;
        }

        currentWeapon?.TryShoot();
    }

    public void ReloadCall()
    {
        if (currentWeapon == null)
        {
            Debug.LogWarning("No Weapon found!");
            return;
        }

        currentWeapon?.TryReload();
    }
}
