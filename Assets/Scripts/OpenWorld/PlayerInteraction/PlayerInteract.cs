using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    float interactRange = 2f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IInteractable interactable = GetInteractableObject();
            if(interactable != null)
            {
                interactable.Interact(transform);
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> iInteracableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable npcInteractable))
            {
                iInteracableList.Add(npcInteractable);
            }
        }
        IInteractable closestiInteractable = null;
        foreach (IInteractable iInteractable in iInteracableList)
        {
            if(closestiInteractable == null)
            {
                closestiInteractable = iInteractable;
            }
            else
            {
                if(Vector3.Distance(transform.position, iInteractable.GetInteractTransform().position) <
                   Vector3.Distance(transform.position, closestiInteractable.GetInteractTransform().position))
                {
                    closestiInteractable = iInteractable;
                }
            }
        }
        return closestiInteractable;
    }
}
