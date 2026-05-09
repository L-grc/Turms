using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;


public class QuestUI : MonoBehaviour
{

    public Transform questListContent;
    public GameObject questEntryPrefab;
    public GameObject objectivetextPrefab;

    public Quest textQuest;
    public int testQuestAmount;
    private List<QuestProgress> testQuests = new();
    
    void Start()
    {
        for(int i = 0; i < testQuestAmount; i++ )
        {
            testQuests.Add(new QuestProgress(textQuest));
        }

        UdateQuestUI();
    }

    public void UdateQuestUI()
    {
        foreach(Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }
        foreach(var quest in testQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContent);
            TMP_Text questNameText = entry.transform.Find("QuestNameText").GetComponent<TMP_Text>();
            Transform objectiveList = entry.transform.Find("ObjectiveList");

            questNameText.text = quest.quest.name;

            foreach(var objetcive in quest.objectives)
            {
                GameObject objTextGO = Instantiate(objectivetextPrefab, objectiveList);
                TMP_Text objText = objTextGO.GetComponent<TMP_Text>();
                objText.text = $"{objetcive.description} ({ objetcive.currentAmount}/{ objetcive.requiredAmount})";
            }
        }
    }
}
