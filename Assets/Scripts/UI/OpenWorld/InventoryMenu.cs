using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu Instance;
    [Header("Item Menu")]
    public Transform itemUIHolder;
    public GameObject itemUIPrefab;
    public Image itemImage;
    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemDescriptionUI;
    public TextMeshProUGUI quantityText;
    public InventorySelection itemSelection;
    public List<Inventory.ItemArray> itemArray = new List<Inventory.ItemArray>();

    void Start()
    {
        Instance = this;
     //   FillItemWindow();
    }

    private void OnEnable()
    {
        FillItemWindow();
    }

    public void FillItemWindow()
    {
        CleanItemWindow();
        itemArray = new List<Inventory.ItemArray>();
        itemSelection.itemButtonList = new List<Button>();
        itemSelection.selectionIndex = 0;

        foreach (Inventory.ItemArray item in Inventory.Instance.items)
        {
            if (item.quantity > 0)
            {
                itemArray.Add(item);
            }
        }

        for (int i = 0; i < itemArray.Count; i++)
        {
            GameObject tempItemPrefab = Instantiate(itemUIPrefab);
            tempItemPrefab.transform.SetParent(itemUIHolder);
            if(itemArray[i].item.itemType == ItemType.InCombat || itemArray[i].item.isKey)
            {
                tempItemPrefab.GetComponent<Button>().interactable = false;
            }

            InventoryItemUI tempItemUI = tempItemPrefab.GetComponent<InventoryItemUI>();
            tempItemUI.itemIndex = i;
            tempItemUI.Init(itemArray[i].item.name);
            itemSelection.itemButtonList.Add(tempItemPrefab.GetComponent<Button>());
        }
        SetItemUI(itemArray[itemSelection.selectionIndex].item.name, 
                  itemArray[itemSelection.selectionIndex].item.description,
                  itemArray[itemSelection.selectionIndex].item.itemSprite,
                  itemArray[itemSelection.selectionIndex].quantity);
    }

    void CleanItemWindow()
    {
        foreach (Transform item in itemUIHolder)
        {
            Destroy(item.gameObject);
        }
        itemArray.Clear();
    }

    public void SetItemUI(string name, string description, Sprite sprite, int quantity)
    {
        itemNameUI.text = name;
        itemDescriptionUI.text = description;
        itemImage.sprite = sprite;
        quantityText.text = quantity.ToString();
    }
}
