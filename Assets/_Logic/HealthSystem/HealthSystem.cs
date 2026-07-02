using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth;

    private HitBox[] hitBoxes;

    private int currentHealth = 0;

    private void Awake()
    {
        hitBoxes = GetComponentsInChildren<HitBox>();

        foreach (var hitBox in hitBoxes)
        {
            hitBox.OnHit += HandleHit;
        }
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void OnDestroy()
    {
        foreach (var hitBox in hitBoxes)
        {
            hitBox.OnHit -= HandleHit;
        }
    }

    private void HandleHit(int dmg, bool crit)
    {
        int totalDmg;
        if(crit)
        {
            totalDmg = Mathf.RoundToInt(dmg * 2f);
        }
        else
        {
            totalDmg = dmg;
        }

        currentHealth -= totalDmg;
        Debug.Log($"Dealt {totalDmg} damage to {gameObject.name}");

        if(currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        gameObject.SetActive(false);
    }
}
