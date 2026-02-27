using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    [SerializeField] public Stat maxHealth;
    [SerializeField] public Stat_Major majorStats;
    [SerializeField] public Stat_Offense offenseStat;
    [SerializeField] public Stat_Defense defenseStat;


    public float GetPhyiscalDamage(out bool isCrit, float scaleFactor = 1)
    {
        float baseDamage = offenseStat.damage.GetValue();
        float bonusDamage = majorStats.strength.GetValue(); //each strength point gives 1 extra damage
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offenseStat.critChance.GetValue();
        float bonusCritChance = majorStats.agility.GetValue() * .3f; //each agility point gives .3 extra crit chance
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offenseStat.critPower.GetValue();
        float bonusCritPower = majorStats.strength.GetValue() * .5f; //each strength point gives .5 extra crit power
        float critPowerMultiplier = (baseCritPower + bonusCritPower) / 100;

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPowerMultiplier : totalBaseDamage;
        return finalDamage * scaleFactor;
    }

    public float GetMaxHealth()
    {
        float baseHP = maxHealth.GetValue();
        float bonusHP = majorStats.vitality.GetValue() * 5; //each agility point give 5 HP

        float finalMaxHP = baseHP + bonusHP;

        return finalMaxHP;
    }

    public float GetEvasion()
    {
        float baseEvasion = defenseStat.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * .5f; //each agility point give 0.5 evosion
        float totatEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 40;
        float finalEvasion = Mathf.Clamp(totatEvasion, 0, evasionCap);

        return finalEvasion;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defenseStat.armor.GetValue();
        float bonusArmor = majorStats.vitality.GetValue(); //each vitalitiy point give 1 armor
        float finalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp01(1 - armorReduction);
        float activeArmor = finalArmor * reductionMultiplier;

        float mitigation = activeArmor / (100 + activeArmor);
        float mitigationCap = .85f;

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        float finalReduction = offenseStat.armorReduction.GetValue();

        return finalReduction;
    }

    public float GetElementalDamage(out ElementType elementType, float scaleFactor = 1)
    {
        float fireDamage = offenseStat.fireDamage.GetValue();
        float iceDamage = offenseStat.iceDamage.GetValue();
        float lightningDamage = offenseStat.lightningDamage.GetValue();

        float bonusElementalDamage = majorStats.intelligence.GetValue(); //each intelligence give 1 point extra elemental damage

        float highestElementalDamage = fireDamage;
        elementType = ElementType.Fire;
        if (iceDamage > highestElementalDamage)
        {
            highestElementalDamage = iceDamage;
            elementType = ElementType.Ice;
        }
        if (lightningDamage > highestElementalDamage)
        {
            highestElementalDamage = lightningDamage;
            elementType = ElementType.Lightning;
        }

        if (highestElementalDamage <= 0)
        {
            elementType = ElementType.None;
            return 0;
        }

        float bonusFireDamage = (fireDamage == highestElementalDamage) ? 0 : fireDamage * .5f;
        float bonusIceDamage = (iceDamage == highestElementalDamage) ? 0 : iceDamage * .5f;
        float bonusLightningDamage = (lightningDamage == highestElementalDamage) ? 0 : lightningDamage * 0.5f;

        float weakerElementsDamage = bonusIceDamage + bonusFireDamage + bonusLightningDamage;
        float finalElementalDamage = highestElementalDamage + bonusElementalDamage + weakerElementsDamage;

        return finalElementalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType elementType)
    {
        float baseResistance = 0;
        float bonusResistance = majorStats.intelligence.GetValue() * .5f;//each intelligence gives .5 extra elemental resistance

        switch (elementType)
        {
            case ElementType.Fire:
                baseResistance = defenseStat.fireResistance.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defenseStat.iceResistance.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defenseStat.lightningResistance.GetValue();
                break;
        }

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = .75f;
        float finalResistance = Mathf.Clamp(resistance, 0, resistanceCap) / 100;
        return finalResistance;
    }
}
