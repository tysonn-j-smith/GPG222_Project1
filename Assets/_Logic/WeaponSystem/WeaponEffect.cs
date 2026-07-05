using System.Collections;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
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

    public void PlayMuzzleEffect()
    {
        if(muzzleEffect == null)
        {
            return;
        }

        muzzleEffect.Play();
    }

    public void PlayImpactEffect(RaycastHit hit)
    {
        if(impactEffect == null)
        {
            return;
        }

        StartCoroutine(ImpactEffectRoutine(hit));
    }

    private IEnumerator ImpactEffectRoutine(RaycastHit hit)
    {
        impactEffect.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

        yield return null;

        impactEffect.Play();
    }
}
