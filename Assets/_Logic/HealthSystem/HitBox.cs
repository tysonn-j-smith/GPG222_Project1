using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [Header("HitBox Settings")]
    [SerializeField] private bool critical = false;

    public event Action<int, bool, GameObject> OnHit;

    public void ReceiveHit(int dmg, GameObject attacker)
    {
        OnHit?.Invoke(dmg, critical, attacker);
    }
}
