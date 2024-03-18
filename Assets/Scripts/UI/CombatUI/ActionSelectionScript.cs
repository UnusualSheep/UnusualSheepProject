using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionSelectionScript : MonoBehaviour
{
    public Button[] actionButtons;
    public Transform selectionImage;
    Transform selection;
    public int selectionIndex;
    public static ActionSelectionScript Instance;

    private void Awake()
    {
        Instance = this;
    }
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectionIndex = (selectionIndex + 1) % actionButtons.Length;
            selection.transform.position = actionButtons[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectionIndex - 1 < 0)
            {
                selectionIndex = actionButtons.Length - 1;
            }
            else
            {
                selectionIndex--;
            }
            selection.transform.localPosition = actionButtons[selectionIndex].transform.localPosition;
        }

        selection.transform.position = actionButtons[selectionIndex].transform.position;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(selectionIndex);
            actionButtons[selectionIndex].onClick.Invoke();
        }
    }
}
