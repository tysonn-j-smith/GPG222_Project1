using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 0.5f;
    [SerializeField] protected float bulletSpread = 0.03f;
    [SerializeField] protected float reloadDelay = .25f;
    [SerializeField] protected float weaponRange = 250f;
    [SerializeField] protected int weaponDamage = 1;
    [SerializeField] protected int maxAmmo = 1;
    [SerializeField] protected LayerMask damageableMask;

    protected int currentAmmo = 0;

    protected float nextFireTime;

    protected bool isReloading = false;

    protected Vector3 bulletDir;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        nextFireTime = 0f;
    }

    public void TryShoot()
    {
        if(!CanShoot())
        {
            return;
        }

        TryShootServer();
    }

    public void TryReload()
    {
        TryReloadServer();
    }

    protected bool CanShoot()
    {
        return !isReloading && Time.time >= nextFireTime && currentAmmo > 0;
    }

    protected IEnumerator ReloadRoutine()
    {
        isReloading = true;

        currentAmmo = 0;

        yield return new WaitForSeconds(reloadDelay);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    protected Vector3 BulletSpread()
    {
        bulletDir = firePoint.forward;

        bulletDir += firePoint.right * Random.Range(-bulletSpread, bulletSpread);
        bulletDir += firePoint.up * Random.Range(-bulletSpread, bulletSpread);

        return bulletDir;
    }

    #region RPC
    public void TryShootServer()
    {
        if (!CanShoot())
        {
            return;
        }

        currentAmmo--;
        nextFireTime = Time.time + fireRate;

        ShootServer();

        if(currentAmmo <= 0)
        {
            TryReloadServer();
        }
    }

    public void TryReloadServer()
    {
        if(isReloading)
        {
            return;
        }

        StartCoroutine(ReloadRoutine());
    }
    #endregion

    public abstract void ShootServer();
}
