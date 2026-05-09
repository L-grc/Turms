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
       
        if (dialogueData == null || (PauseController.IsGamePause && !isDialogueActive))return;

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
		dialogueUI.showDialogueUI(true);
		
		PauseController.SetPause(true);


        
		DisplayCurrentLine();
    }

	void NexLine()
	{
		if(isTyping)
		{
			StopAllCoroutines();
			dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;

		}

		dialogueUI.ClearChoices();

		if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
		{
			EndDialogue();
			return;
        }

		foreach(DialogueChoice dialogueChoices in dialogueData.choices)
		{
			if(dialogueChoices.dialogueIndex == dialogueIndex)
			{
				DisplayChoices(dialogueChoices);
                return;
            }
        }


        if(++dialogueIndex < dialogueData.dialogueLines.Length)
		{
			DisplayCurrentLine();
         
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

	void DisplayChoices(DialogueChoice choice)
	{
		for (int i = 0; i < choice.choices.Length; i++)
		{

			int nextIndex = choice.nextDialogueIndexes[i];
			dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));

		}
           
    }

	void ChooseOption(int nextIndex)
	{
		dialogueIndex = nextIndex;
		dialogueUI.ClearChoices();
		DisplayCurrentLine();
    }

	void DisplayCurrentLine()
	{
		StopAllCoroutines();
		StartCoroutine(TypeLine());

    }

    public void EndDialogue()
	{
		StopAllCoroutines();
		isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.showDialogueUI(false);
		PauseController.SetPause(false);
    }

}
