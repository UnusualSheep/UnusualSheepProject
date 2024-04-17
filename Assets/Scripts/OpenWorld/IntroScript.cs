using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    MovementStateMachine playerMSM;
    int dialogueIndex = 0;

    [System.Serializable]
    public class Dialogue
    {
        public Sprite charImage;
        public string charName;
        public string dialogue;
    }

    [SerializeField] Dialogue[] dialogues;


    private void Start()
    {
        playerMSM = FindObjectOfType<MovementStateMachine>();
        playerMSM.canMove = false;
        ShowDialogue(dialogues[dialogueIndex].dialogue, dialogues[dialogueIndex].charName, dialogues[dialogueIndex].charImage);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            if (dialogueIndex < dialogues.Length - 1)
            {
                dialogueIndex++;
                ShowDialogue(dialogues[dialogueIndex].dialogue, dialogues[dialogueIndex].charName, dialogues[dialogueIndex].charImage);
            }
            else
            {
                StopDialogue();
            }
        }
    }

    void ShowDialogue(string dialogue, string name, Sprite image)
    {
        PlayerInteractUI.Instance.ShowDialoguePanel(dialogue,name,image);
    }

    void StopDialogue()
    {
        PlayerInteractUI.Instance.HideDialoguePanel();
        playerMSM.canMove = true;
    }
}
