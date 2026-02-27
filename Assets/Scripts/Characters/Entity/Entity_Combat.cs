using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    public Collider2D[] targetColliders;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] LayerMask whatIsTarget;

    [Header("Status Effect Variables")]
    [SerializeField] private float effectDuration = 3f;
    [SerializeField] private float iceEffectSlowMulptiplier = 0.3f;



    private void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
    }
    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null) continue;

            float elementalDamage = entityStats.GetElementalDamage(out ElementType elementType, 0.6f);
            float damage = entityStats.GetPhyiscalDamage(out bool isCrit);
            bool targetGotHit = damageble.TakeDamage(damage, elementalDamage, elementType, transform);
            if (elementType != ElementType.None)
            {
                ApplyStatusEffect(target.transform, elementType);
            }

            if (targetGotHit)
            {
                entityVFX.UpdateOnHitColor(elementType);
                entityVFX.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }

    public void ApplyStatusEffect(Transform target, ElementType elementType)
    {
        Entity_StatusHandler entity_StatusHandler = target.GetComponent<Entity_StatusHandler>();
        if (entity_StatusHandler == null) return;

        if (elementType == ElementType.Ice && entity_StatusHandler.CanBeApplied(ElementType.Ice))
            entity_StatusHandler.ApplyIceEffect(effectDuration, iceEffectSlowMulptiplier);

        if (elementType == ElementType.Fire && entity_StatusHandler.CanBeApplied(ElementType.Fire))
        {
            float fireDamage = entityStats.offenseStat.fireDamage.GetValue();
            entity_StatusHandler.ApplyFireEffect(effectDuration, fireDamage);
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
