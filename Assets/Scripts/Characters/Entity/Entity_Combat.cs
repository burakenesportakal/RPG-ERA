using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entityVFX;
    private Entity_Stats stats;
    public Collider2D[] targetColliders;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] LayerMask whatIsTarget;
    private void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }
    public void PerformAttack()
    {
        foreach(var target in GetDetectedColliders())
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null) continue;

            bool isCrit;
            float damage = stats.GetPhyiscalDamage(out isCrit);
            bool targetGotHit = damageble.TakeDamage(damage, transform);
            if (targetGotHit)
                entityVFX.CreateOnHitVFX(target.transform,isCrit);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return targetColliders = Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
