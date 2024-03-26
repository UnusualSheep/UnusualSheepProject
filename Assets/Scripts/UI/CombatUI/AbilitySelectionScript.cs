using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectionScript : MonoBehaviour
{
    public List<Button> abilityButtonsList = new List<Button>();
    public Transform selectionImage;
    Transform selection;
    public int selectionIndex;
    [SerializeField] GameObject actionWindow;


    void Start()
    {
        selection = Instantiate(selectionImage);
        selection.SetParent(gameObject.transform);
        selectionIndex = 0;
        SelectorPosition();
        selection.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        SelectorPosition();
    }

    


    void SelectorPosition()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (selectionIndex + 1 > abilityButtonsList.Count - 1)
            {
                selectionIndex = 0;
            }
            else
            {
                selectionIndex++;
            }
            abilityButtonsList[selectionIndex].onClick.Invoke();
            selection.transform.position = abilityButtonsList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (selectionIndex - 1 < 0)
            {
                selectionIndex = abilityButtonsList.Count - 1;
            }
            else
            {
                selectionIndex--;
            }
            abilityButtonsList[selectionIndex].onClick.Invoke();
            selection.transform.position = abilityButtonsList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectionIndex + 4 > abilityButtonsList.Count - 1)
            {
                selectionIndex = 0;
            }
            else
            {
                selectionIndex = selectionIndex + 4;
            }
            abilityButtonsList[selectionIndex].onClick.Invoke();
            selection.transform.position = abilityButtonsList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectionIndex - 4 < 0)
            {
                selectionIndex = abilityButtonsList.Count - 1;
            }
            else
            {
                selectionIndex = selectionIndex - 4;
            }
            abilityButtonsList[selectionIndex].onClick.Invoke();
            selection.transform.position = abilityButtonsList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (abilityButtonsList[selectionIndex].IsInteractable())
            {
                abilityButtonsList[selectionIndex].onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            actionWindow.SetActive(true);
            transform.gameObject.SetActive(false);
        }
        else
        {
            selection.transform.position = abilityButtonsList[selectionIndex].transform.position;
        }
    }
}
