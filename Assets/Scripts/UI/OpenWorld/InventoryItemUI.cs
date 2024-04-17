using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemUI : MonoBehaviour
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
    public void SelectUI()
    {
        isSelected = true;
    }

    public void Clicked()
    {
        Inventory inventory = Inventory.Instance;
        Inventory.Instance.DeselectOtherItemsUI(this);
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (itemIndex.Equals(i))
            {
                if (isSelected)
                {
                    inventory.SelectTarget();
                }
                else
                {
                    InventoryMenu.Instance.SetItemUI(InventoryMenu.Instance.itemArray[i].item.name,
                                                     InventoryMenu.Instance.itemArray[i].item.description,
                                                     InventoryMenu.Instance.itemArray[i].item.itemSprite,
                                                     InventoryMenu.Instance.itemArray[i].quantity);
                    isSelected = true;
                }
                break;
            }
        }
    }



}
