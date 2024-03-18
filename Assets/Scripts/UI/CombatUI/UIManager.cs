using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public Transform rowHolder;
    public Transform nameHolder;
    public Image portrait;

    [Header("UI Prefabs")]
    public GameObject rowPrefab;
    public GameObject namePrefab;

    [Header("First Layer UI")]
    public RowUI defaultRowUI;
    public OnClickGeneric firstOnClick;

    public GameObject actionWindow;
    public Button burstButton;
    public GameObject abilityWindow;
    [Header("Abbility Window")]
    public Transform abilityUIHolder;
    public GameObject abilityUIPrefab;
    public TextMeshProUGUI mpRequiredText;
    [Header("Abbility Texts")]
    [SerializeField] GameObject abilityTextWindow;
    public TextMeshProUGUI abilityText;
    [SerializeField] GameObject enemyAbilityTextWindow;
    public TextMeshProUGUI enemyAbilityText;

    public static int currentUICount = 1 ;

    public AbilitySelectionScript abilitySelection;
    private void Awake()
    {
        Instance = this;
    }

    public void SpawnRow(out RowUI processedUI, UnitData passedData)
    {
        //instantiate row in row holder
        GameObject tempRow = Instantiate(rowPrefab);
        tempRow.transform.SetParent(rowHolder, false);
        RowUI rowTempInfo = tempRow.GetComponent<RowUI>();

        //instantiate name in name holder
        GameObject tempName = Instantiate(namePrefab);
        tempName.transform.SetParent(nameHolder, false);
        TextMeshProUGUI nameTMPro = tempName.GetComponent<TextMeshProUGUI>();
        OnClickGeneric onClickEvent = tempName.GetComponent<OnClickGeneric>();

        tempRow.name = "Character: " + tempRow.transform.childCount;

        rowTempInfo.characterNameText = nameTMPro;
        processedUI = rowTempInfo;
    }

    public void FillAbilityWindow()
    {
        CleanAbilityWindow();
        abilitySelection.abilityButtonsList = new List<Button>();
        abilitySelection.selectionIndex = 0;
        CharacterControl charControl = FightManager.Instance.currentUnit.charControl;
        for (int i = 0; i < charControl.abilities.Length; i++)
        {
            if (FightManager.Instance.currentUnit.level >= charControl.abilities[i].levelRequired)
            {
                GameObject tempAbilityPrefab = Instantiate(abilityUIPrefab);
                tempAbilityPrefab.transform.SetParent(abilityUIHolder);

                AbilityUI tempAbilityUI = tempAbilityPrefab.GetComponent<AbilityUI>();
                tempAbilityUI.abilityIndex = i;
                tempAbilityUI.Init(charControl.abilities[i].abilityName);
                abilitySelection.abilityButtonsList.Add(tempAbilityPrefab.GetComponent<Button>());
            }
        }
  //      abilitySelection.abilityButtonsList[0].onClick.Invoke();
    }
    public void EnableActionPanel()
    {
        FightManager fm = FightManager.Instance;
        if(fm.currentUnit == null) { return; }
        actionWindow.SetActive(true);
        portrait.sprite = fm.currentUnit.characterPortrait;
        if (fm.currentUnit.currentBurstPoints >= fm.currentUnit.burstPointLimit)
        {
            burstButton.interactable = true;
        }
        else
        {
            burstButton.interactable = false;
        }
    }

    public void DeselectOtherAbilitiesUI(AbilityUI thisAbilityUI)
    {
        foreach (var item in FindObjectsOfType<AbilityUI>())
        {
            if(item != thisAbilityUI)
            {
                item.DeselectUI();
            }
        }
    }
    public void DisplayAbilityWindow()
    {
        abilityTextWindow.SetActive(true);
    }
    public void DisplayEnemyAbilityWindow()
    {
        enemyAbilityTextWindow.SetActive(true);
    }
    public void DisableAbilityWindows()
    {
        abilityTextWindow.SetActive(false);
        enemyAbilityTextWindow.SetActive(false);
    }

    public void SetMpRequiredUI(int abilityMp, int charCurMp)
    {
        mpRequiredText.text = abilityMp + " / " + charCurMp;
    }

    public void SetAbilityText(string abilityName, CharacterTeam team)
    {
        switch (team)
        {
            case CharacterTeam.Friendly:
                abilityText.text = abilityName;
                break;
            case CharacterTeam.Enemy:
                enemyAbilityText.text = abilityName;
                break;
        }
    }
    void CleanAbilityWindow()
    {
        foreach (Transform item in abilityUIHolder)
        {
            Destroy(item.gameObject);
        }
    }
}
