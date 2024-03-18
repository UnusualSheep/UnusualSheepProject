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
   /* [Space(10)]
    
    public CharacterState characterState;
    public CharacterTeam characterTeam;
    CharacterState savedState;
    [Space(10)]
    public UnityEvent onAttack;
    public UnityEvent onAttackQueue;
    public UnityEvent onWasAttacked;
    public UnityEvent onJustReady;

    UnityEvent onCharacterDied = new UnityEvent();

    [Space(16)]
    public CharacterData _target;

    [HideInInspector]
    public CharacterControl _characterControl;


    public void Init()
    {
        if (characterTeam == CharacterTeam.Friendly)
        {
            charUI.characterData = this;
            charUI.Init(maxHp, curHp,maxMp,curMp,characterName, speedLimit, burstPointLimit);
            onJustReady.AddListener(OnReadyDefault);
        }
        onAttack.AddListener(CharacterAttackDefault);
        onWasAttacked.AddListener(CharacterAttackedDefault);
        onAttackQueue.AddListener(OnAttackQueueDefault);
        onCharacterDied.AddListener(OnDeathDefault);

        characterState = CharacterState.Idle;
    }
    
    public IEnumerator QueueAttack(AbilityData ability)
    {
        //Check charcter isn't KO'd or already trying to attack
        if (!IsAlive || characterState == CharacterState.Finished || _target == null || _target.characterState == CharacterState.KO)
        {
            _characterControl.ClearAttackQueue();
            yield break;
        }
        if(ability.mpCost > curMp)
        {
            _characterControl.ClearAttackQueue();
            yield break;
        }
        else
        {
            curMp -= ability.mpCost;
        }
        // Set character's state
        characterState = CharacterState.TryingToAttack; 
        onAttackQueue.Invoke();
        //Wait for target to be attackable before acting
        yield return new WaitUntil(() => _target.CanBeAttacked);
        // if target died while waiting for this attack to be exicuted break the loop
        if (!_target.IsAlive) { yield break; }

        if (!IsAlive || characterState == CharacterState.Finished || _target == null)
        {
            _characterControl.ClearAttackQueue();
            yield break;
        }

        _characterControl. cachedTarget = _target._characterControl;
        _characterControl.lastAbilityUsed = ability;

        if (ability != burstAttack)
        {
            _characterControl.PlayAttackAnimation();
        }
        else
        {
            _characterControl.PlayBurstAnimation();
        }
        yield return new WaitUntil(() => characterState == CharacterState.Attacking);    

        UIManager.Instance.SetAbilityText(ability.abilityName, characterTeam);
        onAttack.Invoke();
        Debug.Log("Attacked with " + ability.abilityName + " to " + _target.characterName);

        //Temp attack
        switch (ability.abilityOutput)
        {
            case AbilityOutput.Damage:
                _target.TakeDamage(ability.abValue);
                break;
            case AbilityOutput.Heal:
                _target.Heal(ability.abValue);
                break;
        }
        if (characterTeam == CharacterTeam.Friendly)
        {
            IncreaseBurst(5);
        }
        _characterControl.ClearAttackQueue();

    }


    public void Heal(int healAmount)
    {
        curHp = Mathf.Clamp(curHp + healAmount, 0, maxHp);
        charUI.UpdateHpBar(curHp, maxHp);
    }
    public void TakeDamage(int damageAmount)
    {
        curHp -= damageAmount;
        if(curHp <= 0)
        {
            curHp = 0;
            characterState = CharacterState.KO;
            onCharacterDied.Invoke();
        }
        //handling Burst
        if(characterTeam == CharacterTeam.Friendly)
        {
            IncreaseBurst(5);
            charUI.UpdateHpBar(curHp, maxHp);
        }
        onWasAttacked.Invoke();
    }

    public bool CanBeAttacked
    {
        get
        {
            return characterState == CharacterState.Idle || characterState == CharacterState.Ready;
        }
    }

    public bool CanQueueAttack
    {
        get
        {
            return characterState == CharacterState.TryingToAttack || _characterControl.attackQueue == null;
        }
    }
    public bool CanAttackTarget
    {
        get
        {
            return _target.characterState == CharacterState.Idle || _target.characterState == CharacterState.Ready;
        }
    }
    public bool IsReadyForAction
    {
        get
        {
            return currentSpeed >= speedLimit;
        }
    }

    public bool IsBurstReady
    {
        get
        {
            return (currentBurstPoints >= burstPointLimit);
        }
    }

    public bool IsAlive
    {
        get { return characterState != CharacterState.KO; }
    }

    void IncreaseBurst(int amount)
    {
        currentBurstPoints += amount;
        currentBurstPoints = Mathf.Clamp(currentBurstPoints, 0, burstPointLimit);
        charUI.UpdateBurstBar(currentBurstPoints);
    }

    void CharacterAttackDefault()
    {
        currentSpeed = 0;
    }

    void CharacterAttackedDefault()
    {
        Debug.Log(characterName + " Was Attacked!");
        if(characterState != CharacterState.KO)
        {
            characterState = savedState;
        }
    }

    void OnReadyDefault()
    {
        SelectCharacter();
    }

    void OnAttackQueueDefault()
    {
        currentSpeed = 0;
        if (characterTeam == CharacterTeam.Friendly)
        {
            charUI.UpdateTimeBar(currentSpeed);
        }
    }

    void OnDeathDefault()

    {
        characterState = CharacterState.KO;
        BattleManager.Instance.CheckMatchStatus();
        characterState = CharacterState.KO;
    }

    public void SelectCharacter()
    {
        if(!IsReadyForAction) { return; }

        UIManager.Instance.actionWindow.SetActive(true);
        UIManager.Instance.abilityWindow.SetActive(false);
        foreach (var item in GameObject.FindObjectsOfType<CharacterControl>())
        {
            if (item.characterData.characterTeam != CharacterTeam.Enemy)
            {
                item.characterData.ResetUIText();
            }
        }
        charUI.physicUI.characterNameText.color = Color.cyan;
        BattleManager.Instance.currentCharacter = _characterControl;
    }
    public void SaveCharacterState()
    {
        savedState = characterState;
    }
    public void ResetUIText()
    {
            charUI.physicUI.characterNameText.color = Color.white;
    }

    public IEnumerator CharacterLoop()
    {

        while (characterState != CharacterState.KO)
        {
            while(currentSpeed < speedLimit)
            {
                if (curHp <= 0)
                {
                    characterState = CharacterState.KO;
                }
                yield return new WaitUntil(()=>characterState != CharacterState.TryingToAttack || characterState != CharacterState.Attacking);

                currentSpeed += (GlobalTimeManager.Instance.globalTimeScale *
                                Time.deltaTime);

                if (characterTeam == CharacterTeam.Friendly)
                {
                    charUI.UpdateTimeBar(currentSpeed);
                }
                if (characterState != CharacterState.Attacked ||characterState != CharacterState.KO)
                {
                    characterState = CharacterState.Idle;
                }
                yield return null;
            }
            //character is ready
            currentSpeed = speedLimit;
            characterState = CharacterState.Ready;
            onJustReady.Invoke();

            yield return new WaitUntil(()=> characterState == CharacterState.Attacking);

            yield return new WaitUntil(() => characterState == CharacterState.Idle);

        }
    }


}

/*
[System.Serializable]
public class CharacterUIData
{
    public RowUI physicUI;
    public int placeInUI = 1;

    [HideInInspector]
    public CharacterData characterData;

    public void Init(int maxHp, int curHp, int maxMp, int curMp, string charName, float speedLimit, float burstLimit)
    {
        placeInUI = UIManager.currentUICount;
        if(placeInUI == 1)
        {
            //do not spawn another row and use first row
            physicUI = UIManager.Instance.defaultRowUI;
            UIManager.Instance.firstOnClick.characterHolder = characterData;
        }
        else
        {
            //row spawnning
            UIManager.Instance.SpawnRow(out physicUI, characterData);

        }
        //HP Slider Setup
        physicUI.hpSlider.maxValue = maxHp;
        UpdateHpBar(curHp, maxHp);
        //MP Slider Setup
        physicUI.mpSlider.maxValue = maxMp;
        UpdateMpBar(curMp);
        //Character Info Setup
        physicUI.characterNameText.text = charName;

        //Burst and Time Setup
        physicUI.burstSlider.maxValue = burstLimit;
        physicUI.burstSlider.value = 0f;
        physicUI.timeSlider.maxValue = speedLimit;
        physicUI.timeSlider.value = 0f;

        UIManager.currentUICount++;
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

public enum CharacterTeam
{
    Friendly,
    Enemy
}

public enum CharacterState
{
    Loading,
    Idle,
    Ready,
    Attacked,
    Attacking,
    TryingToAttack,
    KO,
    Finished,
}
*/