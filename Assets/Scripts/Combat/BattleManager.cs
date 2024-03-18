using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
public class BattleManager : MonoBehaviour
{
    public CharacterControl currentCharacter;
    public List<CharacterControl> friendlyCharacters = new List<CharacterControl>();
    public List<CharacterControl> enemyCharacters = new List<CharacterControl>();

    public GameObject characterSelectedGraphic;

    public static BattleManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
   //     friendlyCharacters = FindObjectsOfType<CharacterControl>().ToList().FindAll(x => x.characterData.characterTeam == CharacterTeam.Friendly);
    //    enemyCharacters = FindObjectsOfType<CharacterControl>().ToList().FindAll(x => x.characterData.characterTeam == CharacterTeam.Enemy);
    }
    /*
    public void SelectCharacter(CharacterData newCharacter)
    {
        newCharacter.SelectCharacter();
        SetTargetGraphicPosition(currentCharacter);
    }

    public void SelectCharacterTarget(CharacterData target)
    {
        if(currentCharacter != null)
        {
            if (currentCharacter.characterData.IsReadyForAction)
            {
                currentCharacter.characterData._target = target;
                SetTargetGraphicPosition(currentCharacter);
            }
        }
    }

    public void SetTargetGraphicPosition(CharacterControl characterControl)
    {
        if(characterControl == null) { return; }
        if(characterControl.characterData._target == null)
        {
            //Deactivate Selection Graphic 
            SelectionGraphicStatus(false);
        }
        else
        {
            SelectionGraphicStatus(true);
            var characterTarget = characterControl.characterData._target._characterControl;
            characterSelectedGraphic.transform.position = characterTarget.selectedGrpahicPosition.position;
        }
    }

    void SelectionGraphicStatus(bool status)
    {
        characterSelectedGraphic.SetActive(status);
    }

    public void DoBasicAttackOnTarget()
    {
        if (currentCharacter.characterData.IsReadyForAction)
        {
            if (currentCharacter.characterData.characterTeam == CharacterTeam.Friendly)
            {
                if(currentCharacter.characterData != null)
                {
                    Debug.Log(currentCharacter.characterData.characterName + " Used their basic ATTACK");
                    if (currentCharacter.characterData.CanQueueAttack)
                    {
                        currentCharacter.attackQueue = StartCoroutine(currentCharacter.characterData.QueueAttack(currentCharacter.characterData.basicAttack));
                    }
                }
            }
        }
    }

    public void DoBurstAttackOnTarget()
    {
        if (currentCharacter.characterData.IsReadyForAction && currentCharacter.characterData.IsBurstReady)
        {
            if (currentCharacter.characterData.characterTeam == CharacterTeam.Friendly)
            {
                if (currentCharacter.characterData != null)
                {
                    Debug.Log(currentCharacter.characterData.characterName + " Used their BURST ATTACK");
                    if (currentCharacter.characterData.CanQueueAttack)
                    {
                        currentCharacter.attackQueue = StartCoroutine(currentCharacter.characterData.QueueAttack(currentCharacter.characterData.burstAttack));
                        currentCharacter.characterData.currentBurstPoints = 0;
                        currentCharacter.characterData.charUI.UpdateBurstBar(currentCharacter.characterData.currentBurstPoints);
                    }
                }
            }
        }
    }

    public CharacterControl RandomFriendlyCharacter
    {
        get
        {
            return friendlyCharacters[Random.Range(0, friendlyCharacters.Count)];
        }
    }

    public void CheckMatchStatus()
    {
        if (FriendlyCharacterIsAlive && !EnemyCharacterIsAlive)
        {
            Debug.Log("Fight Won!");
            StopAllCharacter();
        }
        if (!FriendlyCharacterIsAlive && EnemyCharacterIsAlive)
        {
            Debug.Log("Fight Lost!");
            StopAllCharacter();
        }
    }

    void StopAllCharacter()
    {
        for (int i = 0; i < friendlyCharacters.Count; i++)
        {
            friendlyCharacters[i].StopAll();
        }
        for (int i = 0; i < enemyCharacters.Count; i++)
        {
            enemyCharacters[i].StopAll();
        }
    }
    public bool FriendlyCharacterIsAlive
    {
        get
        {
            bool friendlyIsAlive = false;
            for (int i = 0; i < friendlyCharacters.Count; i++)
            {
                if (friendlyCharacters[i].characterData.IsAlive)
                {
                    friendlyIsAlive = true;
                }
            }
            return friendlyIsAlive;
        }
    }
    public bool EnemyCharacterIsAlive
    {
        get
        {
            bool enemyIsAlive = false;
            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                if (enemyCharacters[i].characterData.IsAlive)
                {
                    enemyIsAlive = true;
                }
            }
            return enemyIsAlive;
        }
    }
}
*/