using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override bool TakeDamage(float damage, Transform damageDealer)
    {
        if (!base.TakeDamage(damage, damageDealer)) return false;
        
        if(damageDealer.CompareTag("Player"))
            enemy.TryEnterBattleState(damageDealer);

        return true;
    }
}
