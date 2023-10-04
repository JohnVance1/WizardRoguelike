using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();

        
        
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // Loads all QuestInfo_SO objects under the Assets/Resoucres/Quests folder
        QuestInfo_SO[] allQuests = Resources.LoadAll<QuestInfo_SO>("Quests");

        Dictionary<string, Quest> idToQuestMap= new Dictionary<string, Quest>();
        foreach(QuestInfo_SO questInfo in allQuests)
        {
            if(idToQuestMap.ContainsKey(questInfo.id)) 
            {
                Debug.LogWarning("Duplicate ID found when making questMap: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if(quest== null)
        {
            Debug.LogError("ID not found in Quest Map: " + id);
        }
        return quest;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void StartQuest(string id)
    {
        // TODO - start the quest
        Debug.Log("Start Quest: " + id);
    }

    private void AdvanceQuest(string id)
    {
        // TODO - advance the quest
        Debug.Log("Advance Quest: " + id);
    }

    private void FinishQuest(string id)
    {
        // TODO - finish the quest
        Debug.Log("Finish Quest: " + id);
    }




}
