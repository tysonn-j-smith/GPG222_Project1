using System.Collections;
using Unity.VisualScripting;
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

    protected float lastFireTime = 0;

    protected bool isReloading = false;

    protected Vector3 bulletDir;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
    }

    protected virtual void Update()
    {
        if(lastFireTime > 0f)
        {
            lastFireTime -= Time.deltaTime;
        }
    }

    public void TryShoot()
    {
        if(!CanShoot())
        {
            if(currentAmmo <= 0 && !isReloading)
            {
                TryReload();
            }

            return;
        }

        currentAmmo--;
        lastFireTime = fireRate;

        Shoot();

        if(currentAmmo <= 0)
        {
            TryReload();
        }
    }

    public void TryReload()
    {
        if(isReloading)
        {
            return;
        }

        StartCoroutine(ReloadRoutine());
    }

    protected bool CanShoot()
    {
        return !isReloading && lastFireTime <= 0f && currentAmmo > 0;
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

    public abstract void Shoot();
}
