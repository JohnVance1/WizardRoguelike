using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;
using System.Xml.Linq;

public class QuestJournalSlot_UI : MonoBehaviour
{
    public Quest storedQuest;
    public string questName;
    public QuestState currentState;
    public TextMeshProUGUI title;
        

    public void SetUpQuest(Quest quest, QuestState state)
    {
        storedQuest = quest;
        questName = quest.info.name;
        title.text = questName;
        currentState = state;
    }

    public void SetQuestState(QuestState state)
    {
        currentState = state;
    }

    public void ChangeQuest(Quest quest)
    {
        questName = quest.info.name;
        storedQuest = quest;
        title.text = questName;

    }





}
