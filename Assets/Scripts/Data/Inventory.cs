using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [System.Serializable]
    public class ItemArray
    {
        public ItemSO item;
        public int quantity;
    }

    [SerializeField]
    public List<ItemArray> items;


    private void Awake()
    {
        Instance = this; 
    }

    public void AddToInventory(ItemSO item)
    {
        foreach (ItemArray itemArray in items)
        {
            if (itemArray.item == item)
            {
                itemArray.quantity++;
                return;
            }
        }
    }


    public void DeselectOtherItemsUI(InventoryItemUI thisItemUI)
    {
        foreach (var item in FindObjectsOfType<InventoryItemUI>())
        {
            if (item != thisItemUI)
            {
                item.DeselectUI();
            }
        }
    }


    public void SelectTarget()
    { 
            Destroy(InventorySelection.Instance.selection.gameObject);
            CrewSelection.Instance.SpawnSelector();
            CrewSelection.Instance.SetSelected(InventoryMenu.Instance.itemArray[InventorySelection.Instance.selectionIndex].item);
    }

}
