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
  
	private enum QuestState { NotStarted, InProgress, Completed }
	private QuestState questState = QuestState.NotStarted;
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
        SyncQuestStat();

		if(questState == QuestState.NotStarted)
		{
			dialogueIndex = 0;
        }
		else if (questState == QuestState.InProgress)
		{
			dialogueIndex = dialogueData.questInProgressIndex;
        }
		else if (questState == QuestState.Completed)
		{
			dialogueIndex = dialogueData.questCompletedIndex;
        }

        isDialogueActive = true;
		

		dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
		dialogueUI.showDialogueUI(true);
		
		PauseController.SetPause(true);


        
		DisplayCurrentLine();
    }

	private void SyncQuestStat()
	{
		if (dialogueData.quest == null) return;

		string questID = dialogueData.quest.questID;
		
		if(QuestController.Instance.IsQuestCompleted(questID) || QuestController.Instance.IsQuestHandedIn(questID))
		{
			questState = QuestState.Completed;
        }
		{
			questState = QuestState.Completed;
        }
        if (QuestController.Instance.IsQuestActive(questID))
		{

			questState = QuestState.InProgress;
        }

		else
		{
			questState = QuestState.NotStarted;
        }
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
			bool givesQuest = choice.givesQuest[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex, givesQuest));

		}
           
    }

	void ChooseOption(int nextIndex, bool givesQuest)
	{
		if(givesQuest)
		{
			QuestController.Instance.AcceptQuest(dialogueData.quest);
			questState = QuestState.InProgress;
        }
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
		if(questState == QuestState.Completed && !QuestController.Instance.IsQuestActive(dialogueData.quest.questID))
		{
			HandleQuestCompletion(dialogueData.quest);
        }

        StopAllCoroutines();
		isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.showDialogueUI(false);
		PauseController.SetPause(false);
    }

	void HandleQuestCompletion(Quest quest)
	{
		QuestController.Instance.HandInQuest(quest.questID);
	}

}
