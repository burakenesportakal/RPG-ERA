using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public Collider2D[] targetColliders;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] LayerMask whatIsTarget;

    [Header("Combat Details")]
    [SerializeField] private float attackDamage = 10;

    public void PerformAttack()
    {
        foreach(var target in GetDetectedColliders())
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>();

            targetHealth?.TakeDamage(attackDamage, transform);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return targetColliders = Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
