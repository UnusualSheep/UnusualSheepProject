using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    MovementStateMachine playerMSM;
    bool chatOpen = false;
    [SerializeField] string interactText;
    [SerializeField] Animator animator;
    [SerializeField] bool leadsToCombat = false;
    [Space(10)]
    [Header("Dialogue variables")]
    [SerializeField] string npcName;
    [SerializeField] Sprite npcImage;
    [SerializeField] string[] dialogueText;
    int dialogueIndex = 0;


    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        playerMSM = FindObjectOfType<MovementStateMachine>();
    }
    public void Interact(Transform interactorTransform)
    {
        transform.LookAt(interactorTransform.position);
        StartDialogue();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetInteractTransform()
    {
        return transform;
    }

    void StartDialogue()
    {
        playerMSM.canMove = false;
        animator.SetTrigger("Talk");
        PlayerInteractUI.Instance.ShowDialoguePanel(dialogueText[dialogueIndex], npcName, npcImage);
        chatOpen = true;
    }
    void StopDialogue()
    {
        PlayerInteractUI.Instance.HideDialoguePanel();
        animator.SetTrigger("Talk");
        playerMSM.canMove = true;
        chatOpen = false;
    }



    private void Update()
    {
        if (chatOpen)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (dialogueIndex < dialogueText.Length - 1)
                {
                    dialogueIndex++;
                    PlayerInteractUI.Instance.ShowDialoguePanel(dialogueText[dialogueIndex], npcName, npcImage);
                }
                else
                {
                    StopDialogue();
                    if (leadsToCombat)
                    {
                        NPCFightStart fightStart = GetComponent<NPCFightStart>();
                        fightStart.readyToDestroy = true;
                        fightStart.StartFight();
                    }
                }
            }
        }
    }
}
