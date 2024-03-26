using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public bool isMainPage = false;
    public Button[] pauseButtonList;
    public Transform selectionImage;
    Transform selection;
    public int selectionIndex;

    // Start is called before the first frame update
    void Start()
    {
        SpawnSelector();
        selectionIndex = 0;
        SelectorPosition();
        selection.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMainPage)
            {
                isMainPage = false;
                gameObject.SetActive(false);
            }
        }
        if (isMainPage)
        {
            SelectorPosition();
        }
    }

    public void SpawnSelector()
    {
        selection = Instantiate(selectionImage);
        selection.SetParent(gameObject.transform);
    }

    private void OnEnable()
    {
        isMainPage = true;
    }


    void SelectorPosition()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectionIndex = (selectionIndex + 1) % pauseButtonList.Length;
            selection.transform.position = pauseButtonList[selectionIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectionIndex - 1 < 0)
            {
                selectionIndex = pauseButtonList.Length - 1;
            }
            else
            {
                selectionIndex--;
            }
            selection.transform.localPosition = pauseButtonList[selectionIndex].transform.localPosition;
        }

        selection.transform.position = pauseButtonList[selectionIndex].transform.position;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(selectionIndex);
            if (pauseButtonList[selectionIndex].IsInteractable())
            {
                isMainPage = false;
                Destroy(selection.gameObject);
                pauseButtonList[selectionIndex].onClick.Invoke();
            }
        }
    }
}
