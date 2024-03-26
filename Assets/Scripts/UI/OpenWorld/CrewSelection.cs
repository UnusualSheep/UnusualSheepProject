using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewSelection : MonoBehaviour
{
    public static CrewSelection Instance;
    public Button[] crewButtonList;
    public Transform selectionImage;
    Transform selection;
    public int selectionIndex;
   public ItemSO selectedItem;



    void Start()
    {
        Instance = this; 
    }

    public void SpawnSelector()
    {
      //  Destroy(selection.gameObject);
        selection = Instantiate(selectionImage);
        selection.SetParent(gameObject.transform);
    }

    private void Update()
    {
        if(selection != null)
        {
            SelectorPosition();
        }
    }

    void SelectorPosition()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectionIndex = (selectionIndex + 1) % crewButtonList.Length;
            selection.transform.position = crewButtonList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectionIndex - 1 < 0)
            {
                selectionIndex = crewButtonList.Length - 1;
            }
            else
            {
                selectionIndex--;
            }
            selection.transform.localPosition = crewButtonList[selectionIndex].transform.localPosition;
        }

        selection.transform.position = crewButtonList[selectionIndex].transform.position;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(selectionIndex);
            if (crewButtonList[selectionIndex].IsInteractable())
            {
                Destroy(selection.gameObject);
                switch(selectedItem.itemOutput)
                {
                    case ItemOutput.Heal:
                        FightManager.Instance.friendlyCharacters[selectionIndex].GetComponent<UnitData>().TakeDamage(-selectedItem.damage);

                        CrewMenu.Instance.UpdateValues();
                        foreach (Inventory.ItemArray item in Inventory.Instance.items)
                        {
                            if (item.item == selectedItem)
                            {
                                item.quantity--;
                            }
                        }
                        InventoryMenu.Instance.FillItemWindow();
                        InventorySelection.Instance.SpawnSelector();
                        break;
                }
            }
        }
    }


    public void SetSelected(ItemSO item)
    {
        selectedItem = item;
    }
}
