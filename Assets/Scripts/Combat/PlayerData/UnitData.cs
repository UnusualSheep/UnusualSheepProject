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

    [Space(10)]
    public CharacterUIData charUI;
    public CharacterControl charControl;
    public Sprite characterPortrait;
    [Space(10)]

    public GameObject damageFloatText;




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
        target.TakeDamage(charControl.selectedAttack.abValue);
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
    public void EnemyAttack()
    {
        List<CharacterControl> targetList = new List<CharacterControl>();
     targetList.Clear();
        foreach (CharacterControl character in FightManager.Instance.friendlyCharacters)
        {
            if (character.GetComponent<CombatStateMachine>().currentState != character.GetComponent<CombatStateMachine>().KO_State)
            {
                targetList.Add(character);
                Debug.Log("character: " + character.name + "! State: " + character.GetComponent<CombatStateMachine>().currentState);
            }

        }
        if(targetList.Count <= 0)
        {
            return;
        }
        int allyToTarget = Random.Range(0, targetList.Count);
        charControl._target = targetList[allyToTarget];
            //FightManager.Instance.friendlyCharacters[allyToTarget];
        Debug.Log(characterName + " is attacking: " + charControl._target.GetComponent<UnitData>().characterName);
        charControl.selectedAttack = charControl.abilities[Random.Range(0, charControl.abilities.Length)];
        while(curMp < charControl.selectedAttack.mpCost)
        {
            charControl.selectedAttack = charControl.abilities[Random.Range(0, charControl.abilities.Length)];
        }
            //charControl.basicAttack;
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


