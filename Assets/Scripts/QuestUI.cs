using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;


public class QuestUI : MonoBehaviour
{

    public Transform questListContent;
    public GameObject questEntryPrefab;
    public GameObject objectiveTextPrefab;


    void Start()
    {

        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        foreach(Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }
        foreach(var quest in QuestController.Instance.activateQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContent);
            TMP_Text questNameText = entry.transform.Find("QuestNameText").GetComponent<TMP_Text>();
            Transform objectiveList = entry.transform.Find("ObjectiveList");

            questNameText.text = quest.quest.name;

            foreach(var objetcive in quest.objectives)
            {
                GameObject objTextGO = Instantiate(objectiveTextPrefab, objectiveList);
                TMP_Text objText = objTextGO.GetComponent<TMP_Text>();
                objText.text = $"{objetcive.description} ({ objetcive.currentAmount}/{ objetcive.requiredAmount})";
            }
        }
    }
}
