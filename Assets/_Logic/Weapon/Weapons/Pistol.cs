using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        Vector3 dir = BulletSpread().normalized;
        Ray ray = new Ray(firePoint.position, dir);

        if (Physics.Raycast(ray, out RaycastHit hit, weaponRange, damageableMask))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawRay(firePoint.position, dir * weaponRange , Color.red);
        }
    }
}
