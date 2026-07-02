using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [Header("HitBox Settings")]
    [SerializeField] private bool critical = false;

    public event Action<int, bool> OnHit;

    public void ReceiveHit(int dmg)
    {
        OnHit?.Invoke(dmg, critical);
    }
}
