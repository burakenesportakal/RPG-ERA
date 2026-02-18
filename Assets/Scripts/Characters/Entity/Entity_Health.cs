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
    public virtual bool TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return false;
        if (AttackEvaded()) return false;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        entity?.ReciveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVFX();
        ReduceHP(damage);
        return true;
    }

    private bool AttackEvaded() => Random.Range(0, 100) < stats.GetEvasion();
   

    protected virtual void ReduceHP(float damage)
    {
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
