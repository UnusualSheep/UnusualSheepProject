using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite npcSprite;
    [SerializeField] string interactText;
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void Interact(Transform interactorTransform)
    {
        Debug.Log("interact");
        transform.LookAt(interactorTransform.position);
        animator.SetTrigger("Talk");
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetInteractTransform()
    {
        return transform;
    }
}
