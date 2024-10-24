using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class QuestLog_UI : MonoBehaviour
{
    public QuestLog questLogOBJ;

    private List<QuestJournalSlot_UI> m_ActiveQuests;
    private List<QuestJournalSlot_UI> m_CompletedQuests;

    private Dictionary<string, QuestJournalSlot_UI> ActiveQuests;
    private Dictionary<string, QuestJournalSlot_UI> CompletedQuests;




    private void Awake()
    {
        //m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_ActiveQuests = new List<QuestJournalSlot_UI>();
        m_CompletedQuests = new List<QuestJournalSlot_UI>();
        ActiveQuests = new Dictionary<string, QuestJournalSlot_UI>();
        CompletedQuests = new Dictionary<string, QuestJournalSlot_UI>();
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
        //this.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        //this.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void StartQuest(string id)
    {
        Quest quest = QuestManager.instance.GetQuestByID(id);
        questLogOBJ.ActiveQuests.Add(quest);

        //QuestJournalSlot_UI journalSlot;
        //m_ActiveQuests.Add(journalSlot);
        //ActiveQuests.Add(quest.info.id, journalSlot);

    }

    private void AdvanceQuest(string id)
    {
        QuestJournalSlot_UI questSlot = ActiveQuests[id];
        Quest quest = questSlot.storedQuest;
        questSlot.SetQuestState(quest.state);
        // Later add in the new info for the next step of the quest


    }

    private void FinishQuest(string id)
    {
        QuestJournalSlot_UI questSlot = ActiveQuests[id];
        Quest quest = questSlot.storedQuest;
        questSlot.SetQuestState(quest.state);

        CompletedQuests[id] = questSlot;
        ActiveQuests.Remove(id);

        m_ActiveQuests.Remove(questSlot);
        m_CompletedQuests.Add(questSlot);



    }


}
