using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Shotgun Settings")]
    [SerializeField] private int shotCount = 5;
    [SerializeField] private float shotDelay = 0.001f;

    private WeaponEffect effect;

    protected override void Start()
    {
        base.Start();
        effect = GetComponent<WeaponEffect>();
    }

    public override void Shoot()
    {
        StopAllCoroutines();
        StartCoroutine(BurstRoutine());
    }

    private IEnumerator BurstRoutine()
    {
        effect?.PlayMuzzleEffect();

        for (int i = 0; i < shotCount; i++)
        {
            Vector3 dir = BulletSpread();
            Ray ray = new Ray(firePoint.position, dir);

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

            yield return new WaitForSeconds(shotDelay);
        }

        currentAmmo--;
    }

}
