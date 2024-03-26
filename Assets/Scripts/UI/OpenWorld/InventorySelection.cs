using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelection : MonoBehaviour
{

    public List<Button> itemButtonList = new List<Button>();
    public Transform selectionImage;
    public Transform selection;
    public int selectionIndex;
    [SerializeField] PauseScreen pauseMenu;
    public static InventorySelection Instance;

    void Start()
    {
        Instance = this;
        SelectorPosition();
        selection.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnEnable()
    {
        SpawnSelector();
    }

    private void Update()
    {
        if (selection != null)
        {
            SelectorPosition();
        }
    }

    void SelectorPosition()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (selectionIndex + 1 > itemButtonList.Count - 1)
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex = 0;
            }
            else
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex++;
            }
            itemButtonList[selectionIndex].onClick.Invoke();
            selection.transform.position = itemButtonList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (selectionIndex - 1 < 0)
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex = itemButtonList.Count - 1;
            }
            else
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex--;
            }
            itemButtonList[selectionIndex].onClick.Invoke();
            selection.transform.position = itemButtonList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectionIndex + 2 > itemButtonList.Count - 1)
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex = 0;
            }
            else
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex = selectionIndex + 2;
            }
            itemButtonList[selectionIndex].onClick.Invoke();
            selection.transform.position = itemButtonList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectionIndex - 2 < 0)
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex = itemButtonList.Count - 1;
            }
            else
            {
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
                selectionIndex = selectionIndex - 2;
            }
            itemButtonList[selectionIndex].onClick.Invoke();
            selection.transform.position = itemButtonList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (itemButtonList[selectionIndex].IsInteractable())
            {

                itemButtonList[selectionIndex].onClick.Invoke();
                itemButtonList[selectionIndex].GetComponent<InventoryItemUI>().DeselectUI();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.isMainPage = true;
            Destroy(selection.gameObject);
            pauseMenu.SpawnSelector();
            transform.gameObject.SetActive(false);
        }
        else
        {
            selection.transform.position = itemButtonList[selectionIndex].transform.position;
        }
    }

    public void SpawnSelector()
    {
        selectionIndex = 0;
        selection = Instantiate(selectionImage);
        selection.SetParent(gameObject.transform);
    }
}
