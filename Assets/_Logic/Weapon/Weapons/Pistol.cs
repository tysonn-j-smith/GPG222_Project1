using UnityEngine;

public class Pistol : Weapon
{
    private WeaponEffect effect;

    protected override void Start()
    {
        base.Start();
        effect = GetComponent<WeaponEffect>();
    }

    public override void Shoot()
    {
        Vector3 dir = BulletSpread();
        Ray ray = new Ray(firePoint.position, dir);

        if(effect != null)
        {
            effect.PlayMuzzleEffect();
        }

        if (Physics.Raycast(ray, out RaycastHit hit, weaponRange, damageableMask))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red);
            Debug.Log($"Hit {hit.collider.name}");
        }
        else
        {
            Debug.DrawRay(firePoint.position, dir * weaponRange , Color.red);
        }
    }
}
