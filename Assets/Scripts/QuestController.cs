using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<QuestProgress> activateQuests = new();
    private QuestUI questUI;

    public List<string> handingQuestIDs = new();
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
         
        questUI = FindAnyObjectByType<QuestUI>();
        InventoryController.Instance.OnInventoryChanged += CheckInventoryForQuests;
    }

    public void AcceptQuest(Quest quest)
    {
        if (IsQuestActive(quest.questID)) return;
     

        activateQuests.Add(new QuestProgress(quest));

        CheckInventoryForQuests();
        questUI.UpdateQuestUI();
    }

    public bool IsQuestActive(string questID) => activateQuests.Exists(q => q.quest.questID == questID);

    public void CheckInventoryForQuests()
    {
        Dictionary<int, int> itemCounts = InventoryController.Instance.GetItemCounts();

        foreach(QuestProgress quest in activateQuests)
        {
            foreach(QuestObjective questObjective in quest.objectives)
            {
                if(questObjective.type != ObjectiveType.CollectItem) continue;
                if (!int.TryParse(questObjective.objectiveID, out int itemID)) continue;

                int newAmount = itemCounts.TryGetValue(itemID, out int count) ? Mathf.Min(count, questObjective.requiredAmount) : 0;

                if(questObjective.currentAmount != newAmount)
                {
                    questObjective.currentAmount = newAmount;
                    
                }
            }


        }

        questUI.UpdateQuestUI();
    }

    public bool IsQuestCompleted(string questID)
    {
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        return quest != null && quest.objectives.TrueForAll(o => o.IsCompleted);
    }

    public void HandInQuest(string questID)
    {
        if(RemoveRequiredItemsForQuest(questID))
        {
            
            
            return;


        }

        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        if(quest!= null )
        {
            handingQuestIDs.Add(questID);
            activateQuests.Remove(quest);
            questUI.UpdateQuestUI();
        }

    }

    public bool IsQuestHandedIn(string questID)
    {
        return handingQuestIDs.Contains(questID);
    }

    public bool RemoveRequiredItemsForQuest(string questID)
    {
        QuestProgress quest = activateQuests.Find(q => q.QuestID == questID);
        if (quest == null) return false;

        Dictionary<int, int> requiredItems = new();

        foreach(QuestObjective objective in quest.objectives)
        {
            if (objective.type == ObjectiveType.CollectItem && int.TryParse(objective.objectiveID, out int itemID))
            {
                requiredItems[itemID] = objective.requiredAmount;
            }                

        }

        Dictionary<int, int> itemCounts = InventoryController.Instance.GetItemCounts();
        foreach(var item in requiredItems)
        {
            if (itemCounts.GetValueOrDefault(item.Key) < item.Value)
            {
                return false;
            }
        }

        foreach(var itemRequirement in requiredItems)
        {
            InventoryController.Instance.RemoveItemFromInventory(itemRequirement.Key, itemRequirement.Value);
        }
        return true;

    }



}
