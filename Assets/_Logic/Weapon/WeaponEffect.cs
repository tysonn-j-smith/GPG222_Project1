using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    [Header("Weapon Effect Settings")]
    [SerializeField] private ParticleSystem muzzleEffect;

    public void PlayMuzzleEffect()
    {
        if(muzzleEffect == null)
        {
            return;
        }

        muzzleEffect.Play();
    }
}
