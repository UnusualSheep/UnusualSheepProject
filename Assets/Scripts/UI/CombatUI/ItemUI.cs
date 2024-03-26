using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{

    public TextMeshProUGUI itemText;
    public int itemIndex = 0;
    AbilityUITimeManager abilityUI;
    bool isSelected = false;

    public void Init(string itemName)
    {
        itemText.text = itemName;
        abilityUI = FindObjectOfType<AbilityUITimeManager>();
    }
    public void DeselectUI()
    {
        isSelected = false;
    }

    public void Clicked()
    {
        UnitData data = FightManager.Instance.currentUnit;
        Inventory inventory = Inventory.Instance;
        UIManager.Instance.DeselectOtherItemsUI(this);
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (itemIndex.Equals(i))
            {
                if (isSelected)
                {
                    Debug.Log("Selected");
                    FightManager.Instance.DoAbility(data.charControl.abilities[i]);
                }
                else
                {
                    UIManager.Instance.SetItemUI(inventory.items[i].item.name, inventory.items[i].item.description);
                    isSelected = true;
                }
                break;
            }
        }
    }

}
