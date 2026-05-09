using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class NPC : MonoBehaviour, IInteractable
{
	public NPCDialogue dialogueData;
	private DialogueController dialogueUI;


    private int dialogueIndex;
	private bool isTyping, isDialogueActive;
  

	private void Start()
	{
		dialogueUI = DialogueController.Instance;
    }

    public bool CanInteract()
	{
		return !isDialogueActive;
	}


    public void Interact()
	{
		if(dialogueData == null || (PauseController && !isDialogueActive))
			return;

		if(isDialogueActive)
		{
			NexLine();
		}
		else
		{
			StartDialogue();
		}
	}

	void StartDialogue()
	{
		isDialogueActive = true;
		dialogueIndex = 0;
		

		dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
		dialogueUI.ShowDialogueUI(true);
		

        StartCoroutine(TypeLine());
	}

	void NexLine()
	{
		if(isTyping)
		{
			StopAllCoroutines();
			dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;

		}
		else if(++dialogueIndex < dialogueData.dialogueLines.Length)
		{
			StartCoroutine(TypeLine());
		}
		else
		{
			EndDialogue();
		}
	}


	IEnumerator TypeLine()
	{
		isTyping = true;
        dialogueUI.SetDialogueText("");

		foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
		{
			
			dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
		}
		isTyping = false;

		if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
		{
			yield return new WaitForSeconds(dialogueData.autoProgressDeLay);
			NexLine();
		}

	}
	public void EndDialogue()
	{
		StopAllCoroutines();
		isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
		
	}

}
