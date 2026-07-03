using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [Header("Rifle Settings")]
    [SerializeField] private int burstAmount = 3;
    [SerializeField] private float burstDelay = 0.25f;

    private bool isFiring = false;

    private WeaponEffect effect;

    protected override void Start()
    {
        base.Start();
        effect = GetComponent<WeaponEffect>();
    }

    public override void Shoot()
    {
        if(isFiring)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(BurstRoutine());
    }

    private IEnumerator BurstRoutine()
    {
        isFiring = true;

        for(int x = 0; x < burstAmount; x++)
        {
            Vector3 dir = BulletSpread();
            Ray ray = new Ray(firePoint.position, dir);

            effect?.PlayMuzzleEffect();

            if (Physics.Raycast(ray, out RaycastHit hit, weaponRange, damageableMask))
            {
                Debug.DrawLine(firePoint.position, hit.point, Color.red);

                HitBox hitbox = hit.collider.GetComponent<HitBox>();
                hitbox?.ReceiveHit(weaponDamage, transform.root.gameObject);

                effect?.PlayImpactEffect(hit);
            }
            else
            {
                Debug.DrawRay(firePoint.position, dir * weaponRange, Color.red);
            }

            currentAmmo--;

            yield return new WaitForSeconds(burstDelay);
        }

        isFiring = false;

        yield break;
    }
}
