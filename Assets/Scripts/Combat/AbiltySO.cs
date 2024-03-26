using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities")]
public class AbiltySO : ScriptableObject
{

    public string abilityName = "AbilityOne";
    public int levelRequired = 0;

    public int abValue = 10;
    public int mpCost = 1;

    public AbilityRange abilityRange = AbilityRange.Melee;
    public AbilityType abilityType = AbilityType.Physical;
    public AbilityOutput abilityOutput = AbilityOutput.Damage;
    public AbilityTarget abilityTarget = AbilityTarget.Enemy;

    public GameObject attackParticle;
}

public enum AbilityRange
{
    Ranged,
    Melee,
}

public enum AbilityType
{
    Physical,
    Magic
}

public enum AbilityTarget
{
    Ally,
    AllAllies,
    KOAllies,
    Enemy,
    AllEnemies,
}

public enum AbilityOutput
{
    Damage,
    Heal
}