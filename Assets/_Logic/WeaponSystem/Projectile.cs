using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] protected int projectileDamage = 1;
    [SerializeField] protected LayerMask damageableMask;

    public abstract void Detonate();
}