using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapTest : MonoBehaviour
{
    public ItemSO item;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddToInventory(item);
            Destroy(this);
        }
    }

}
