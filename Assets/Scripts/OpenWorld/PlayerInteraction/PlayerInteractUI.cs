using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerInteractUI : MonoBehaviour
{
    public static PlayerInteractUI Instance;
    [SerializeField] GameObject containerGameObject;
    [SerializeField] PlayerInteract playerInteract;
    [SerializeField] TextMeshProUGUI interactText;
    [Space (10)]
    [Header ("Interact")]
    [SerializeField] GameObject interactPanel;
    [SerializeField] TextMeshProUGUI interactPanelText;
    [SerializeField] Image interactPanelImage;
    [Space(10)]
    [Header("Dialogue")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialoguePanelText;
    [SerializeField] TextMeshProUGUI dialogueNamePanelText;
    [SerializeField] Image dialoguePanelImage;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(IInteractable iInteractable)
    {
        containerGameObject.SetActive(true);
        interactText.text = iInteractable.GetInteractText();
    }

    public void Hide()
    {
        containerGameObject.SetActive(false);
    }

    public void ShowInteractPanel(string _interactText, Sprite _interactImage)
    {
        interactPanel.SetActive(true);
        interactPanelText.text = _interactText;
        interactPanelImage.sprite = _interactImage;
    }

    public void HideInteractPanel()
    {
        interactPanel.SetActive(false);
    }

    public void ShowDialoguePanel(string _dialogueText, string _dialgueNameText, Sprite _dialogueImage)
    {
        dialoguePanel.SetActive(true);
        dialoguePanelText.text = _dialogueText;
        dialogueNamePanelText.text = _dialgueNameText;
        dialoguePanelImage.sprite = _dialogueImage;
    }

    public void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }
}
