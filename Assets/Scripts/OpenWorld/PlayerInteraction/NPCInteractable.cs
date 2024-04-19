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
    [SerializeField] IntroScript.Dialogue[] dialogues;
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
        PlayerInteractUI.Instance.ShowDialoguePanel(dialogues[dialogueIndex].dialogue, dialogues[dialogueIndex].charName, dialogues[dialogueIndex].charImage);
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
                if (dialogueIndex < dialogues.Length - 1)
                {
                    dialogueIndex++;
                    playerMSM.canMove = false;
                    PlayerInteractUI.Instance.ShowDialoguePanel(dialogues[dialogueIndex].dialogue, dialogues[dialogueIndex].charName, dialogues[dialogueIndex].charImage);
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
