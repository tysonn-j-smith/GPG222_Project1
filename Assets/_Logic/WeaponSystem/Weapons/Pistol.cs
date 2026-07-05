using UnityEngine;

public class Pistol : Weapon
{
    private WeaponEffect effect;

    protected override void Start()
    {
        base.Start();
        effect = GetComponent<WeaponEffect>();
    }

    public override void ShootServer()
    {
        Vector3 dir = BulletSpread();
        Ray ray = new Ray(firePoint.position, dir);

        effect?.PlayMuzzleEffectServerRpc();

        if (Physics.Raycast(ray, out RaycastHit hit, weaponRange, damageableMask))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red);

            HitBox hitbox = hit.collider.GetComponent<HitBox>();
            hitbox?.ReceiveHit(weaponDamage, transform.root.gameObject);

            effect?.PlayImpactEffectServerRpc(hit.point);
        }
        else
        {
            Debug.DrawRay(firePoint.position, dir * weaponRange , Color.red);
        }

        currentAmmo--;
    }
}
