using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
	public NPCDialogue dialogueData;
	public GameObject dialoguePanel;
	public TMP_Text dialogueText, nameText;
	public Image portraitImage;

	private int dialogueIndex;
	private bool isTyping, isDialogueActive;

	public bool CanInteract()
	{
		return !isDialogueActive;
	}

	public void Interact()
	{
		if(dialogueData == nul (PauseController && !isDialogueActive))
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
		
		nameText.SetText(dialogueData.name);
		portraitImage.sprite = dialogueData.npcPortrait;

		dialoguePanel.SetActive(true);

		StartCoroutine(TypeLine());
	}

	void NexLine()
	{
		if(isTyping)
		{
			StopAllCoroutines();
			dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
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
		dialogueText.SetText("");

		foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
		{
			dilaogueText.text += letter;
			yield return new WaitForSecondes(dialogueData.typingSpeed);
		}
		isTyping = false;

		if(dialogueData.autoprogessLines.Length > dialogueIndex && dialogueData.autoprogessLines[dialogueIndex])
		{
			yield return new WaitForSecondes(dialogueData.autoprogessDeLay);
			NexLine();
		}

	}
	public void EndDialogue()
	{
		StopAllCoroutines();
		isDialogueActive = false;
		dialogueText.SetText("");
		dialoguePanel.SetActive(false);
		
	}

}
