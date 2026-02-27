using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour , IDamageble
{
    private Slider healthSlider;
    private Entity_VFX entityVFX;
    private Entity entity;
    private Entity_Stats stats;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;

    [Header("Knockback Details")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;
    // Heavy Damage
    [SerializeField] private float heavyDamageThreshold = .3f;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthSlider = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();

        currentHealth = stats.GetMaxHealth();
        UpdateHealthSlider();
    }
    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType elementType, Transform damageDealer)
    {
        if (isDead) return false;
        if (AttackEvaded()) return false;

        Entity_Stats attackerStats = damageDealer.GetComponent<Entity_Stats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0;

        float mitigation = stats.GetArmorMitigation(armorReduction);
        float physicalDamageTaken = (1 - mitigation) * damage;

        float elementalResistance = stats.GetElementalResistance(elementType);
        float elementalDamageTaken = elementalDamage * (1 - elementalResistance);

        HandleKnockback(damageDealer, physicalDamageTaken);
        ReduceHP(physicalDamageTaken + elementalDamageTaken);
        return true;
    }

    private void HandleKnockback(Transform damageDealer, float finalDamage)
    {
        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer);
        float duration = CalculateDuration(finalDamage);

        entity?.ReciveKnockback(knockback, duration);
    }

    private bool AttackEvaded() => Random.Range(0, 100) < stats.GetEvasion();
   

    public void ReduceHP(float damage)
    {
        entityVFX?.PlayOnDamageVFX();
        currentHealth -= damage;
        UpdateHealthSlider();

        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealthSlider() => healthSlider.value = currentHealth / stats.GetMaxHealth();

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x *= direction;

        return knockback;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshold;
}
