using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entity_VFX;
    private Entity_Stats entity_Stats;
    private Entity_Health entity_Health;
    private ElementType currenElement = ElementType.None;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entity_VFX = GetComponent<Entity_VFX>();
        entity_Stats = GetComponent<Entity_Stats>();
        entity_Health = GetComponent<Entity_Health>();
    }
    public void ApplyIceEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entity_Stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(IceEffectCoroutine(reducedDuration,slowMultiplier));
    }

    public void ApplyFireEffect(float duration, float fireDamage)
    {
        float fireResistance = entity_Stats.GetElementalResistance(ElementType.Fire);
        float reducedFireDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(FireEffectCoroutine(duration, reducedFireDamage));
    }
    private IEnumerator IceEffectCoroutine(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);

        currenElement = ElementType.Ice;
        entity_VFX.PlayOnStatusEffectVFX(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);
        currenElement = ElementType.None;
    }

    private IEnumerator FireEffectCoroutine(float duration, float totalDamage)
    {
        currenElement = ElementType.Fire;
        entity_VFX.PlayOnStatusEffectVFX(duration, ElementType.Fire);

        int ticksPerSecond = 2;
        int tickCount = Mathf.RoundToInt(ticksPerSecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for(int i = 0; i<tickCount; i++)
        {
            entity_Health.ReduceHP(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
        currenElement = ElementType.None;
    }
    public bool CanBeApplied(ElementType elementType)
    {
        return currenElement == ElementType.None;
    } 
}
