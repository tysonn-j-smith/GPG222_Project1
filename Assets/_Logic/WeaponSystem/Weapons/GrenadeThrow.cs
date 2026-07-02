using UnityEngine;

public class GrenadeThrow : Weapon
{
    [Header("Grenade Throw Settings")]
    [SerializeField] private GameObject grenadeObj;
    [SerializeField] private float throwForce = 10f;

    public override void Shoot()
    {
        GameObject obj = Instantiate(grenadeObj, firePoint.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(firePoint.forward * throwForce, ForceMode.Impulse);
            rb.AddRelativeTorque(Vector3.right * 6f, ForceMode.Force);
        }

        currentAmmo--;
    }
}
