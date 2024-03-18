using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public TextMeshProUGUI abilityText;
    public int abilityIndex = 0;
    AbilityUITimeManager abilityUI;
    bool isSelected = false;

    public void Init(string abilityName)
    {
        abilityText.text = abilityName;
        abilityUI = FindObjectOfType<AbilityUITimeManager>();
    }
    public void DeselectUI()
    {
        isSelected = false;
    }
    
    public void Clicked()
    {
        UnitData data = FightManager.Instance.currentUnit;
        UIManager.Instance.DeselectOtherAbilitiesUI(this);
        for (int i = 0; i < data.charControl.abilities.Length; i++)
        {
            if(abilityIndex.Equals(i))
            {
                if (isSelected)
                {
                    Debug.Log("Selected");
                    FightManager.Instance.DoAbility(data.charControl.abilities[i]);
                }
                else
                {
                    UIManager.Instance.SetMpRequiredUI(data.charControl.abilities[i].mpCost, data.curMp);
                    isSelected = true;
                }
                    break;
            }
        }
    }
    
}
