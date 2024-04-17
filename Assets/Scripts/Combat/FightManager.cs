using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public UnitData currentUnit;
    public bool unitAttacked = false;

    public GameObject enemySelectionCamera;
    public GameObject allySelectionCamera;
    public GameObject characterSelectedGraphic;
    public UnitData _targetData;

    public List<CharacterControl> friendlyCharacters = new List<CharacterControl>();
    public List<CharacterControl> enemyCharacters = new List<CharacterControl>();
    public List<CharacterControl> deadEnemies = new List<CharacterControl>();
    [SerializeField] GameObject nameHolder;
    [SerializeField] GameObject rowHolder;

    [SerializeField] GameObject winCamera;
    [SerializeField] GameObject ui;
    public GameObject winScreen;
    public List<ItemSO> itemRewards;

    public int xpReward = 0;
    public int goldReward = 0;
    private void Awake()
    {
        Instance = this;
        currentUnit = null;
        itemRewards = new List<ItemSO>();
        PopulateTeams();
        SpawnUI();
    }

    public void DoBasicAttack()
    {
        if (currentUnit == null || currentUnit.GetComponent<CombatStateMachine>().currentState == null) { return; }
        currentUnit.charControl.selectedAttack = currentUnit.charControl.basicAttack;
        SelectTarget();
    }
    public void DoBurstAttack()
    {
        if (currentUnit == null || currentUnit.GetComponent<CombatStateMachine>().currentState == null) { return; }
        currentUnit.charControl.selectedAttack = currentUnit.charControl.burstAttack;
        SelectTarget();
    }

    public void DoAbility(AbiltySO abiltySO)
    {
        currentUnit.charControl.selectedAttack = abiltySO;
        SelectTarget();
    }
    public void UseItem(ItemSO itemSO)
    {
        currentUnit.charControl.selectedItem = itemSO;
        SelectItemTarget();
    }

    public void SelectItemTarget()
    {
        bool targetSelected = false;
        if (currentUnit == null || currentUnit.GetComponent<CombatStateMachine>().currentState == null) { return; }
        if (currentUnit.charControl.selectedItem.itemTarget == AbilityTarget.Enemy)
        {
            enemySelectionCamera.SetActive(true);
            if (!targetSelected)
            {
                SetTargetGraphicPosition(enemyCharacters[0]);
            }
        }
        else if (currentUnit.charControl.selectedItem.itemTarget == AbilityTarget.Ally || currentUnit.charControl.selectedItem.itemTarget == AbilityTarget.KOAllies)
        {
            allySelectionCamera.SetActive(true);
        }
    }

    public void SelectTarget()
    {
        bool targetSelected = false;
        if (currentUnit == null || currentUnit.GetComponent<CombatStateMachine>().currentState == null) { return; }
        if (currentUnit.charControl.selectedAttack.abilityTarget == AbilityTarget.Enemy)
        {
            enemySelectionCamera.SetActive(true);
            if (!targetSelected)
            {
                SetTargetGraphicPosition(enemyCharacters[0]);
            }
        }
        else if (currentUnit.charControl.selectedAttack.abilityTarget == AbilityTarget.Ally || currentUnit.charControl.selectedAttack.abilityTarget == AbilityTarget.KOAllies)
        {
            allySelectionCamera.SetActive(true);
        }
    }

    public void CheckMatchStatus()
    {        
        if (!PlayerDefeated && EnemiesDefeated)
        {
            GlobalTimeManager.Instance.globalTimeScale = 0;
            foreach(CharacterControl c in deadEnemies)
            {
                Destroy(c.transform.parent.gameObject);
            }
            StartCoroutine(WinState());
        }
        if (PlayerDefeated && !EnemiesDefeated)
        {
            GlobalTimeManager.Instance.globalTimeScale = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

    }

    public bool PlayerDefeated
    {
        get
        {
            bool friendlyIsAlive = true;
            for (int i = 0; i < friendlyCharacters.Count; i++)
            {
                if (Instance.friendlyCharacters[i].GetComponent<UnitData>().curHp > 0)
                {
                    friendlyIsAlive = false;
                }
            }
            return friendlyIsAlive;
        }
    }
    public bool EnemiesDefeated
    {
        get
        {
            bool enemyIsAlive = true;
            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                if (Instance.enemyCharacters[i].GetComponent<UnitData>().curHp > 0)
                {
                    enemyIsAlive = false;
                }
            }
            return enemyIsAlive;
        }
    }
    public void SetTargetGraphicPosition(CharacterControl characterControl)
    {
        if (characterControl == null) { SelectionGraphicStatus(false); return; }
        if (characterControl == null)
        {
            //Deactivate Selection Graphic 
            SelectionGraphicStatus(false);
        }
        else
        {
            SelectionGraphicStatus(true);
            var characterTarget = characterControl;
            characterSelectedGraphic.transform.position = characterTarget.selectedGrpahicPosition.position;
        }
    }

    void SelectionGraphicStatus(bool status)
    {
        characterSelectedGraphic.SetActive(status);
    }

    public void PopulateTeams()
    {
  //      friendlyCharacters.Clear();
        enemyCharacters.Clear();
        deadEnemies.Clear();
   //     friendlyCharacters = FindObjectsOfType<CharacterControl>().ToList().FindAll(x => x.characterTeam == CharacterTeam.Friendly);
        enemyCharacters = FindObjectsOfType<CharacterControl>().ToList().FindAll(x => x.characterTeam == CharacterTeam.Enemy);
    }
    public void ApplyBonuses()
    {
        int leaderIndex = 0;
        for (int i = 0; i < friendlyCharacters.Count; i++)
        {
            if (friendlyCharacters[i].GetComponent<UnitData>().isLeader)
            {
                leaderIndex = i;
            }
        }
        UnitData leaderUnit = friendlyCharacters[leaderIndex].GetComponent<UnitData>();
        foreach(var character in friendlyCharacters)
        {
            character.GetComponent<UnitData>().GetLeaderBonus(leaderUnit.leaderBuff, leaderUnit.leaderDebuff, leaderUnit.buffPercent, leaderUnit.debuffPercent);
        }
    }
    public void SpawnUI()
    {
        foreach (Transform child in nameHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in rowHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var character in friendlyCharacters)
        {
            CharacterUIData player = character.GetComponent<UnitData>().charUI;
            UIManager.Instance.SpawnRow(out player.physicUI, player.unitData);
            player.Init();
            character.GetComponent<UnitData>().currentSpeed = 0;

        }
    }

    public void DestroyEnemies()
    {
        foreach(CharacterControl characterControl in enemyCharacters)
        {
            Destroy(characterControl.gameObject);
        }
    }

    IEnumerator WinState()
    {
        winCamera.SetActive(true);
        ui.SetActive(false);
        CombatStateMachine[] csms = FindObjectsOfType<CombatStateMachine>();
        foreach (CombatStateMachine csm in csms)
        {
            if (csm.GetComponent<UnitData>().curHp > 0)
            {
                csm.WinState();
            }
        }
        yield return new WaitForSeconds(1f);
        winCamera.SetActive(false);
        ui.SetActive(true);
        winScreen.SetActive(true);
        foreach (CombatStateMachine csm in csms)
        {
            if (csm.GetComponent<UnitData>().curHp <= 0)
            {
                csm.GetComponent<UnitData>().curHp = 1;
            }
        }

            yield break;
    }

}
