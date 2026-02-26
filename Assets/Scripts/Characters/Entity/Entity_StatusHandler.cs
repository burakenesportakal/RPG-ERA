using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entity_VFX;
    private Entity_Stats entity_Stats;
    private ElementType currenElement = ElementType.None;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entity_VFX = GetComponent<Entity_VFX>();
        entity_Stats = GetComponent<Entity_Stats>();
    }
    public void ApplyIceEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entity_Stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(IceEffectCoroutine(reducedDuration,slowMultiplier));
    }
    private IEnumerator IceEffectCoroutine(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);

        currenElement = ElementType.Ice;
        entity_VFX.PlayOnStatusEffectVFX(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);
        currenElement = ElementType.None;
    }
    public bool CanBeApplied(ElementType elementType)
    {
        return currenElement == ElementType.None;
    } 
}
