using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WinScreenUI : MonoBehaviour
{
    FightManager fm;

    int xpReward;
    int xpPerAlly;
    int goldReward;
    [SerializeField] List<ItemSO> itemRewards;
    [SerializeField] TextMeshProUGUI xpEarnedText;


    [System.Serializable]
    public class CrewStats
    {
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI characterClass;
        public TextMeshProUGUI levelText;
        public Image characterImage;
        public Slider xpSlider;
        public TextMeshProUGUI xpText;
        public GameObject lvlUpText;
        
    }
    [Space(10)]
    [SerializeField] CrewStats[] crewStats;
    
    [Space(10)]
    [SerializeField] Transform itemRewardPanel;
    [SerializeField] GameObject itemPrefab;

    [Space(10)]
    [SerializeField] TextMeshProUGUI goldEarnedText;


    private void OnEnable()
    {
        fm = FightManager.Instance;
        xpReward = fm.xpReward;
        fm.xpReward = 0;
        goldReward = fm.goldReward;
        fm.goldReward = 0;
        Inventory.Instance.gold += goldReward;
        itemRewards = new List<ItemSO>();
        foreach (ItemSO item in fm.itemRewards)
        {
            itemRewards.Add(item);
        }
        fm.itemRewards.Clear();
        UpdateValues();
    }

    public void UpdateValues()
    {
        ItemRewards();
        AllocateXP();
        xpEarnedText.text = xpReward.ToString();
        goldEarnedText.text = goldReward.ToString();
        for (int i = 0; i < crewStats.Length; i++)
        {
            UnitData unit = fm.friendlyCharacters[i].GetComponent<UnitData>();
            crewStats[i].characterImage.sprite = unit.characterPortrait;
            crewStats[i].characterName.text = unit.characterName;
            crewStats[i].characterClass.text = fm.friendlyCharacters[i].transform.parent.name;
            crewStats[i].levelText.text = "Lvl: " + unit.level;
            crewStats[i].xpSlider.value = (float)unit.curXp / unit.xpToLevel;
            crewStats[i].xpText.text = unit.curXp + "/" + unit.xpToLevel;
        }
    }

    void ItemRewards()
    {
        foreach (Transform child in itemRewardPanel)
        {
            Destroy(child.gameObject);
        }
        SpawnItemUI();
    /*    foreach (ItemSO item in itemRewards)
        {
            SpawnItemUI();
        }*/
    }

    void SpawnItemUI()
    {
        for (int i = 0; i < itemRewards.Count; i++)
        {
            GameObject tempItemPrefab = Instantiate(itemPrefab);
            tempItemPrefab.transform.SetParent(itemRewardPanel);
            ItemRewardUI tempItemRewardUI = tempItemPrefab.GetComponent<ItemRewardUI>();
            tempItemRewardUI.Init(itemRewards[i].itemSprite, itemRewards[i].itemName);
            Inventory.Instance.AddToInventory(itemRewards[i]);
        }
    }

    void AllocateXP()
    {
        int alliesAlive = 0;
        for(int i = 0; i < fm.friendlyCharacters.Count; i++)
        {
            if(fm.friendlyCharacters[i].GetComponent<UnitData>().curHp > 0)
            {
                alliesAlive++;
            }
        }
        xpPerAlly = xpReward / alliesAlive;
        foreach(CharacterControl character in fm.friendlyCharacters)
        {
            UnitData unit = character.GetComponent<UnitData>();
            if (unit.curHp > 0)
            {
                unit.curXp += xpPerAlly;
                if(unit.curXp >= unit.xpToLevel)
                {
                    unit.LevelUp();

                }
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(RandomEncounter.Instance.EndFight());

        }
    }
}
