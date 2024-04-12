using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items")]
public class ItemSO : ScriptableObject
{
    public string itemName = "Item";
    public Sprite itemSprite = null;
    public bool isKey;
    public int itemValue;
    public int damage;

    public string description = "Oh hey! This is an item yo";

    public ItemType itemType = ItemType.Both;
    public ItemOutput itemOutput = ItemOutput.Heal;
    public AbilityTarget itemTarget = AbilityTarget.Ally;
}


public enum ItemType
{
    InCombat,
    OutsideCombat,
    Both,
}


public enum ItemOutput
{
    Damage,
    Heal,
    Status,
}

public enum StatusType
{

}