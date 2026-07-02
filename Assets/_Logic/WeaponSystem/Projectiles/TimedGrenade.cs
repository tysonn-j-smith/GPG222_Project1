using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TimedGrenade : Projectile
{
    [Header("Timed Grenade Settings")]
    [SerializeField] private float fuseTimer = 1f;
    [SerializeField] private float explosionRaidus = 1f;
    [SerializeField] private LayerMask coverMask;

    private float timer = 0f;

    private void OnEnable()
    {
        timer = fuseTimer + Random.Range(.5f, 1.5f);
    }

    private void OnDisable()
    {
        timer = 0f;
    }

    private void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0f)
        {
            Detonate();
        }
    }

    public override void Detonate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRaidus, damageableMask);
        
        foreach(Collider hit in hits)
        {
            Vector3 target = hit.bounds.center;
            Vector3 dir = target - transform.position;
            float distance = dir.magnitude;

            if(Physics.Raycast(transform.position, dir.normalized, out RaycastHit rayHit, distance, coverMask | damageableMask))
            {
                if(rayHit.collider != hit)
                {
                    continue;
                }
            }

            HitBox hitbox = hit.gameObject.GetComponent<HitBox>();
            hitbox?.ReceiveHit(projectileDamage);
        }

        //Add Object Pooling.
        gameObject.SetActive(false);
    }
}
