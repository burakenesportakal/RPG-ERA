using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_Stats : MonoBehaviour
{
    [SerializeField] private Stat maxHealth;
    [SerializeField] private Stat_Major majorStats;
    [SerializeField] private Stat_Offense offenseStat;
    [SerializeField] private Stat_Defense defenseStat;


    public float GetPhyiscalDamage(out bool isCrit)
    {
        float baseDamage = offenseStat.damage.GetValue();
        float bonusDamage = majorStats.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offenseStat.critChance.GetValue();
        float bonusCritChance = majorStats.agility.GetValue() * .3f;
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offenseStat.critPower.GetValue();
        float bonusCritPower = majorStats.strength.GetValue() * .5f;
        float critPowerMultiplier = (baseCritPower + bonusCritPower)/100;

        isCrit = Random.Range(0,100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage* critPowerMultiplier : totalBaseDamage;
        return finalDamage;
    }

    public float GetMaxHealth()
    {
        float baseHP = maxHealth.GetValue();
        float bonusHP = majorStats.vitality.GetValue() * 5; //each agility point give 5 HP

        return (baseHP + bonusHP);
    }

    public float GetEvasion()
    {
        float baseEvasion = defenseStat.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * .5f; //each agility point give 0.5 evosion
        float totatEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 40;
        float finalEvasion = Mathf.Clamp(totatEvasion, 0 , evasionCap);

        return finalEvasion;
    }
}
