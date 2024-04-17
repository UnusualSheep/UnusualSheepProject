using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CharacterData
{
    public string characterName = "Character Name";
    [Space(10)]
    public AbilityData basicAttack;
    public AbilityData burstAttack;
    public List<AbilityData> characterAbilities;
    [Space(10)]
    public CharacterUIData charUI;
    [Space(10)]
    public int maxHp = 100;
    public int curHp = 100;
    [Space(10)]
    public int maxMp = 100;
    public int curMp = 100;
    [Space(10)]
    public float burstPointLimit = 100;
    public float currentBurstPoints = 100;
    [Space(10)]
    public float speedLimit = 10;
    public float currentSpeed = 10;
}
