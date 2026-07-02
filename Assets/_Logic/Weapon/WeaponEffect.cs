using System.Collections;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    [Header("Weapon Effect Settings")]
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private ParticleSystem impactEffect;

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
