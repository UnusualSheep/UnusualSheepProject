using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UnitData : MonoBehaviour
{
    public string characterName = "Character Name";
    [Space(10)]
    public int level = 1;
    [SerializeField] public int curXp;
    [SerializeField] public int xpToLevel;
    [SerializeField] int goldReward;
    [Space(10)]
    public int maxHp = 100;
    public int curHp = 100;
    [Space(10)]
    public int maxMp = 100;
    public int curMp = 100;
    [Space(10)]
    [Header("Unit Stats")]
    [SerializeField] int strength;
    public float strengthBouns { get; private set; }
    [SerializeField] int constitution;
    public float constitutionBouns { get; private set; }
    [SerializeField] int dexterity;
    public float dexterityBouns { get; private set; }
    [SerializeField] int magic;
    public float magicBouns { get; private set; }
    [SerializeField] int spirit;
    public float spiritBouns { get; private set; }
    [Space(10)]
    [Header("Leader Stats")]
    public LeaderBuff leaderBuff;
    public float buffPercent;
    public LeaderDebuff leaderDebuff;
    public float debuffPercent;
    public bool isLeader = false;
    [Space(10)]
    public float burstPointLimit = 100;
    public float currentBurstPoints = 100;
    [Space(10)]
    public float speedLimit = 10;
    public float speedLimitBouns { get; private set; }
    public float currentSpeed = 10;

    [Space(10)]
    public CharacterUIData charUI;
    public CharacterControl charControl;
    public Sprite characterPortrait;
    [Space(10)]

    public GameObject damageFloatText;

    public enum LeaderBuff
    {
        Strength,
        Constitution,
        Dexterity,
        Magic,
        Spirit,
        Speed
    }

    public enum LeaderDebuff
    {
        Strength,
        Constitution,
        Dexterity,
        Magic,
        Spirit,
        Speed
    }


    private void Awake()
    {
        charControl = GetComponent<CharacterControl>();
        if (charControl.characterTeam == CharacterTeam.Friendly)
        {
            charUI.unitData = this;
        }
    }
    public void IncreaseSpeed()
    {
        currentSpeed += (GlobalTimeManager.Instance.globalTimeScale * Time.deltaTime);
    }
    public void UnitAttacked()
    {
        FightManager.Instance.unitAttacked = true; 
    }
    public void TakeDamage(int damageAmount)
    {
        curHp -= damageAmount;
        if (curHp <= 0)
        {
            curHp = 0;
            if(charControl.characterTeam == CharacterTeam.Enemy)
            {
                FightManager.Instance.deadEnemies.Add(charControl);
                FightManager.Instance.enemyCharacters.Remove(charControl);
                FightManager.Instance.xpReward += curXp;
                FightManager.Instance.goldReward += goldReward;
                ItemRewards itemRewards = GetComponent<ItemRewards>();
                if (itemRewards != null)
                {
                    int rewardsToBeGiven;
                    rewardsToBeGiven = Random.Range(0, itemRewards.itemRewards.Length);
                    for(int i = 0; i < rewardsToBeGiven; i++)
                    {
                        int reward = Random.Range(0, itemRewards.itemRewards.Length);
                        FightManager.Instance.itemRewards.Add(itemRewards.itemRewards[reward]);
                    }
                }
            }
        }
        if(curHp >= maxHp)
        {
            curHp = maxHp;
        }
        //handling Burst
        if (charControl.characterTeam == CharacterTeam.Friendly)
        {
            IncreaseBurst(10);
            charUI.UpdateHpBar(curHp, maxHp);
        }

        if(damageAmount < 0)
        {
            damageFloatText.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            damageFloatText.GetComponentInChildren<TextMeshProUGUI>().text = (damageAmount * -1).ToString();
        }
        else
        {
            damageFloatText.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            damageFloatText.GetComponentInChildren<TextMeshProUGUI>().text = damageAmount.ToString();
         //   GetComponent<CombatStateMachine>().animator.SetTrigger("hit");
        }
        damageFloatText.SetActive(true);
    }
    public void UpdateUI()
    {
        charUI.UpdateTimeBar(currentSpeed);
        charUI.UpdateBurstBar(currentBurstPoints);
        charUI.UpdateHpBar(curHp, maxHp);
        charUI.UpdateMpBar(curMp);
    }
    void IncreaseBurst(int amount)
    {
        currentBurstPoints += amount;
        currentBurstPoints = Mathf.Clamp(currentBurstPoints, 0, burstPointLimit);
        charUI.UpdateBurstBar(currentBurstPoints);
    }
    public void SendDamage()
    {
        UnitData target = charControl._target.GetComponent<UnitData>();
        target.TakeDamage(CalculateDamage(charControl.selectedAttack, target));
        if (charControl.characterTeam == CharacterTeam.Friendly)
        {
            IncreaseBurst(5);
        }
        if (charControl.selectedAttack == charControl.burstAttack)
        {
            currentBurstPoints = 0;
        }
        else
        {
            curMp -= charControl.selectedAttack.mpCost;
        }
    }

    int CalculateDamage(AbiltySO ability, UnitData target)
    {
        float modifier = Random.Range(0.85f, 1f);
        float a = (2 * level + 10) / 5f;
        float d = 0;
        switch (ability.abilityType)
        {
            case AbilityType.Physical:
                switch (ability.abilityRange)
                {
                    case AbilityRange.Melee:
                        d = a * (ability.abValue + ((strength + (strength * strengthBouns / 100)) / (target.constitution + (target.constitution * target.constitutionBouns / 100))) * 2);
                        break;
                    case AbilityRange.Ranged:
                        d = a * (ability.abValue + ((dexterity + (dexterity * dexterityBouns / 100)) / (target.constitution + (target.constitution * target.constitutionBouns / 100))) * 2);
                        break;
                }
                break;

            case AbilityType.Magic:
                switch (ability.abilityOutput)
                {
                    case AbilityOutput.Damage:
                        d = a * (ability.abValue + ((magic + (magic * magicBouns / 100)) / (target.spirit + (target.spirit * target.spiritBouns / 100))) * 2);
                        break;
                    case AbilityOutput.Heal: 
                        d = a * ability.abValue - (magic + (magic * magicBouns / 100)) - 2;
                        break;
                }
                break;
        }
        return Mathf.FloorToInt(d * modifier);
    }
    public void EnemyAttack()
    {
        List<CharacterControl> targetList = new List<CharacterControl>();
     targetList.Clear();
        foreach (CharacterControl character in FightManager.Instance.friendlyCharacters)
        {
            if (character.GetComponent<CombatStateMachine>().currentState != character.GetComponent<CombatStateMachine>().KO_State)
            {
                targetList.Add(character);
            }

        }
        if(targetList.Count <= 0)
        {
            return;
        }
        int allyToTarget = Random.Range(0, targetList.Count);
        charControl._target = targetList[allyToTarget];
            //FightManager.Instance.friendlyCharacters[allyToTarget];
        charControl.selectedAttack = charControl.abilities[Random.Range(0, charControl.abilities.Length)];
        while(curMp < charControl.selectedAttack.mpCost)
        {
            charControl.selectedAttack = charControl.abilities[Random.Range(0, charControl.abilities.Length)];
        }
            //charControl.basicAttack;
    }

    public void GetLeaderBonus(LeaderBuff lb, LeaderDebuff ld, float buff, float debuff)
    {
        strengthBouns = constitutionBouns = dexterityBouns = magicBouns = spiritBouns = speedLimitBouns = 0;
        switch (lb)
        {
            case LeaderBuff.Strength:
                strengthBouns = buff;
                break;
            case LeaderBuff.Constitution:
                constitutionBouns = buff;
                break;
            case LeaderBuff.Dexterity:
                dexterityBouns = buff;
                break;
            case LeaderBuff.Magic:
                magicBouns = buff;
                break;
            case LeaderBuff.Spirit:
                spiritBouns = buff;
                break;
            case LeaderBuff.Speed:
                speedLimitBouns = -buff;
                break;
        }

        switch (ld)
        {
            case LeaderDebuff.Strength:
                strengthBouns = -debuff;
                break;
            case LeaderDebuff.Constitution:
                constitutionBouns = -debuff;
                break;
            case LeaderDebuff.Dexterity:
                dexterityBouns = -debuff;
                break;
            case LeaderDebuff.Magic:
                magicBouns = -debuff;
                break;
            case LeaderDebuff.Spirit:
                spiritBouns = -debuff;
                break;
            case LeaderDebuff.Speed:
                speedLimitBouns = debuff;
                break;
        }
    }


    public void LevelUp()
    {
        level++;
        curXp = curXp - xpToLevel;
        xpToLevel = (int)(xpToLevel * 1.5f);

        //Temp Level up
        maxHp += (maxHp / 10);
        maxMp += (maxMp / 10);
        strength++;
        constitution++;
        dexterity++;
        magic++;
        spirit++;
        
    }

}

[System.Serializable]
public class CharacterUIData
{
    public RowUI physicUI;
    public int placeInUI = 1;
    public UnitData unitData;

    public void Init()
    {

        if (unitData.charControl.characterTeam == CharacterTeam.Friendly)
        {
            //HP Slider Setup
            physicUI.hpSlider.maxValue = unitData.maxHp;
            UpdateHpBar(unitData.curHp, unitData.maxHp);
            //MP Slider Setup
            physicUI.mpSlider.maxValue = unitData.maxMp;
            UpdateMpBar(unitData.curMp);
            //Character Info Setup
            physicUI.characterNameText.text = unitData.characterName;
            if(unitData.isLeader)
            {
                physicUI.characterNameText.color = Color.cyan;
            }

            //Burst and Time Setup
            physicUI.burstSlider.maxValue = unitData.burstPointLimit;
            physicUI.burstSlider.value = 0f;
            physicUI.timeSlider.maxValue = unitData.speedLimit;
            physicUI.timeSlider.value = 0f;
            UIManager.currentUICount++;
        }
    }
    public void UpdateTimeBar(float currentProgress)
    {
        physicUI.timeSlider.value = currentProgress;
    }
    public void UpdateBurstBar(float currentProgress)
    {
        physicUI.burstSlider.value = currentProgress;
    }
    public void UpdateHpBar(int currentAmount, int maxAmount)
    {
        physicUI.hpSlider.value = currentAmount;
        physicUI.hpText.text = currentAmount + " / " + maxAmount;
    }
    public void UpdateMpBar(int currentAmount)
    {
        physicUI.mpSlider.value = currentAmount;
        physicUI.mpText.text = currentAmount.ToString();
    }
}


