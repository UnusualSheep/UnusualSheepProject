using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrewMenu : MonoBehaviour
{
    FightManager fm;
    public static CrewMenu Instance;

    [System.Serializable]
    public class CrewPanel
    {
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI characterClass;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI mpText;
        public Image characterImage;
    }

    [SerializeField] CrewPanel[] crewPanels;

    private void Start()
    {
        Instance = this; 
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        fm = FightManager.Instance;
        UpdateValues();
    }

    public void UpdateValues()
    {
        for (int i = 0; i < crewPanels.Length; i++)
        {
            UnitData unit = fm.friendlyCharacters[i].GetComponent<UnitData>();
            crewPanels[i].characterImage.sprite = unit.characterPortrait;
            crewPanels[i].characterName.text = unit.characterName;
            crewPanels[i].characterClass.text = fm.friendlyCharacters[i].transform.parent.name;
            crewPanels[i].levelText.text = "Lvl: " + unit.level;
            crewPanels[i].hpText.text = "HP: " + unit.curHp + "/" + unit.maxHp;
            crewPanels[i].mpText.text = "MP: " + unit.curMp + "/" + unit.maxMp;
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public List<GameObject> GetCrewImages()
    {
        List<GameObject> crewImages = new List<GameObject>();
        foreach (CrewPanel go in crewPanels)
        {
            crewImages.Add(go.characterImage.gameObject);
        }
        return crewImages;
    }
}
