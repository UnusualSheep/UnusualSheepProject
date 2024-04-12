using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] ItemSO item;
    MovementStateMachine msm;
    [SerializeField] bool isLocked;
    [SerializeField] Sprite lockSprite;

    private void Awake()
    {
        msm = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementStateMachine>();
        animator = GetComponent<Animator>();
        
    }
    public string GetInteractText()
    {
        return "Open Chest";
    }

    public void Interact(Transform interactorTransform)
    {
        if (isLocked)
        {
            foreach(Inventory.ItemArray item in Inventory.Instance.items)
            {
                if(item.item.isKey)
                {
                    if(item.quantity > 0)
                    {
                        item.quantity--;
                        isLocked = false;
                        StartCoroutine(OpenChest());
                        return;
                    }
                }
            }
            StartCoroutine(LockedChest());
        }
        else
        {
            StartCoroutine(OpenChest());
        }
    }

    public Transform GetInteractTransform()
    {
        return transform;
    }

    IEnumerator OpenChest()
    {
        msm.GetComponentInChildren<Animator>().SetTrigger("OpenChest");
        yield return new WaitForSeconds(0.25f);
        animator.SetTrigger("Interact");
        Inventory.Instance.AddToInventory(item);
        msm.canMove = false;
        PlayerInteractUI.Instance.Hide();
        PlayerInteractUI.Instance.ShowInteractPanel("Obtained " + item.itemName + "!", item.itemSprite);
        item = null; 
        yield return new WaitForSeconds(1.5f);
        PlayerInteractUI.Instance.HideInteractPanel();
        yield return new WaitForSeconds(0.1f);
        msm.canMove = true;
        Destroy(this);
    }

    IEnumerator LockedChest()
    {
        msm.canMove = false;
        PlayerInteractUI.Instance.ShowInteractPanel("Chest Locked", lockSprite);
        yield return new WaitForSeconds(1f);
        PlayerInteractUI.Instance.HideInteractPanel();
        msm.canMove = true;
    }
}
