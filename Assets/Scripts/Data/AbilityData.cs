using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public string abilityName = "AbilityOne";

    public int abValue = 10;
    public int mpCost = 1;

   // public AbilityType abilityType = AbilityType.Melee;
   // public AbilityOutput abilityOutput = AbilityOutput.Damage;

    //ToDo Particle
    public GameObject attackParticle;
}


