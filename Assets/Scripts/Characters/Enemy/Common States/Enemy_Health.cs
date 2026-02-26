using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override bool TakeDamage(float damage, float elementalDamage, ElementType elementType, Transform damageDealer)
    {
        if (!base.TakeDamage(damage, elementalDamage, elementType, damageDealer)) return false;
        
        if(damageDealer.CompareTag("Player"))
            enemy.TryEnterBattleState(damageDealer);

        return true;
    }
}
