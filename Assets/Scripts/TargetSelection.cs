using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    FightManager fm;
    int targetIndex = 0;
    [SerializeField] GameObject actionPanel;
    [SerializeField] GameObject abilityPanel;
    public List<CharacterControl> targetList;

    private void OnEnable()
    {
        fm = FightManager.Instance;
        targetList = new List<CharacterControl>();
        switch (fm.currentUnit.charControl.selectedAttack.abilityTarget)
        {
            case (AbilityTarget.Ally):
                foreach(CharacterControl control in fm.friendlyCharacters)
                {
                    if (control.GetComponent<CombatStateMachine>().currentState != control.GetComponent<CombatStateMachine>().KO_State)
                    {
                        targetList.Add(control);
                    }
                }
                fm.SetTargetGraphicPosition(targetList[0]);
                break;
            case (AbilityTarget.KOAllies):
                foreach (CharacterControl control in fm.friendlyCharacters)
                {
                    if (control.GetComponent<CombatStateMachine>().currentState == control.GetComponent<CombatStateMachine>().KO_State)
                    {
                        targetList.Add(control);
                    }
                    
                    if(targetList.Count < 0)
                    {
                        return;
                    }
                    
                }
                fm.SetTargetGraphicPosition(targetList[0]);
                break;
            case (AbilityTarget.Enemy):
                foreach (CharacterControl control in fm.enemyCharacters)
                {
                    if (control.GetComponent<CombatStateMachine>().currentState != control.GetComponent<CombatStateMachine>().KO_State)
                    {
                        targetList.Add(control);
                    }
                }
                break;
            default:
                break;
        }
        fm.SetTargetGraphicPosition(targetList[0]);
        targetIndex = 0;
        actionPanel.SetActive(false);
        abilityPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        GlobalTimeManager.Instance.globalTimeScale = 0;
        if (this.gameObject == fm.allySelectionCamera)
        {
            ChooseAllyTarget();
        }
        else if (this.gameObject == fm.enemySelectionCamera)
        {
            ChooseEnemyTarget();
        }
    }

    void ChooseAllyTarget()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetIndex = (targetIndex + 1) % targetList.Count;
            fm.SetTargetGraphicPosition(targetList[targetIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (targetIndex - 1 < 0)
            {
                targetIndex = targetList.Count - 1;
            }
            else
            {
                targetIndex--;
            }
            fm.SetTargetGraphicPosition(targetList[targetIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //tell unit to do that attack
            fm.currentUnit.charControl._target = targetList[targetIndex];
            fm.SetTargetGraphicPosition(null);
            this.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (fm.currentUnit.charControl.selectedAttack == fm.currentUnit.charControl.basicAttack
                || fm.currentUnit.charControl.selectedAttack == fm.currentUnit.charControl.burstAttack)
            {
                UIManager.Instance.EnableActionPanel();
            }
            else
            {
                abilityPanel.SetActive(true);
            }
            fm.SetTargetGraphicPosition(null);
            this.gameObject.SetActive(false);
        }
    }
    void ChooseEnemyTarget()
    {
        Debug.Log(targetIndex);
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetIndex = (targetIndex + 1) % fm.enemyCharacters.Count;
            fm.SetTargetGraphicPosition(fm.enemyCharacters[targetIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (targetIndex - 1 < 0)
            {
                targetIndex = fm.enemyCharacters.Count - 1;
            }
            else
            {
                targetIndex--;
            }
            fm.SetTargetGraphicPosition(fm.enemyCharacters[targetIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //tell unit to do that attack
            fm.currentUnit.charControl._target = fm.enemyCharacters[targetIndex];
            fm.SetTargetGraphicPosition(null);
            this.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (fm.currentUnit.charControl.selectedAttack == fm.currentUnit.charControl.basicAttack
                || fm.currentUnit.charControl.selectedAttack == fm.currentUnit.charControl.burstAttack)
            {
                UIManager.Instance.EnableActionPanel();
            }
            else
            {
                abilityPanel.SetActive(true);
            }
            fm.SetTargetGraphicPosition(null);
            this.gameObject.SetActive(false);
        }
    }
}