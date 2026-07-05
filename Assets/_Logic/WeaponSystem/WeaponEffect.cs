using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class WeaponEffect : NetworkBehaviour
{
    [Header("Weapon Effect Settings")]
    [SerializeField] private Transform firePoint;
    private ParticleSystem muzzleEffect;
    private ParticleSystem impactEffect;

    private void Start()
    {
        GameObject muzzle = ObjectPooler.Instance.GetFromPool("muzzleEffect", firePoint.position, Quaternion.identity);
        muzzle.transform.parent = firePoint.transform;
        muzzleEffect = muzzle.GetComponent<ParticleSystem>();

        GameObject impact = ObjectPooler.Instance.GetFromPool("impactEffect", transform.position, Quaternion.identity);
        impactEffect = impact.GetComponent<ParticleSystem>();

        if(muzzleEffect == null || impactEffect == null)
        {
            return;
        }
    }

    [Rpc(SendTo.Everyone)]
    public void PlayMuzzleEffectServerRpc()
    {
        if(muzzleEffect == null)
        {
            return;
        }

        muzzleEffect.Play();
    }

    [Rpc(SendTo.Everyone)]
    public void PlayImpactEffectServerRpc(Vector3 hitPoint)
    {
        if(impactEffect == null)
        {
            return;
        }

        StartCoroutine(ImpactEffectRoutine(hitPoint));
    }

    private IEnumerator ImpactEffectRoutine(Vector3 hitPoint)
    {
        impactEffect.transform.position = hitPoint;

        yield return null;

        impactEffect.Play();
    }
}
