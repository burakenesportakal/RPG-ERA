using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entityVFX;
    public Collider2D[] targetColliders;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] LayerMask whatIsTarget;

    [Header("Combat Details")]
    [SerializeField] private float attackDamage = 10;

    private void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
    }
    public void PerformAttack()
    {
        foreach(var target in GetDetectedColliders())
        {
            IDamageble damageble = target.GetComponent<IDamageble>();
            damageble?.TakeDamage(attackDamage, transform);
            entityVFX.CreateOnHitVFX(target.transform);
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
