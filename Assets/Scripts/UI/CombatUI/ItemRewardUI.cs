using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemRewardUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemName;



    public void Init(Sprite _itemSprite, string _itemName)
    {
        itemIcon.sprite = _itemSprite;
        itemName.text = _itemName;
    }
}
