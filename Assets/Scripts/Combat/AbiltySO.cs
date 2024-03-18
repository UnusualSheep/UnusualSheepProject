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

    public AbilityType abilityType = AbilityType.Melee;
    public AbilityOutput abilityOutput = AbilityOutput.Damage;
    public AbilityTarget abilityTarget = AbilityTarget.Enemy;

    public GameObject attackParticle;
}

public enum AbilityType
{
    Ranged,
    Melee,
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