using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;
using System.Xml.Linq;

public class QuestJournalSlot_UI : VisualElement
{
    public Quest storedQuest;
    public string questName;
    public QuestState currentState;
    public Label Name;

    public QuestJournalSlot_UI()
    {
        Name = new Label();
        Add(Name);
        Name.AddToClassList("questName");
        Name.text = "???";

        AddToClassList("questSlot");
    }

    public QuestJournalSlot_UI(Quest quest, QuestState state)
    {
        Name = new Label();
        Add(Name);
        Name.AddToClassList("questName");
        AddToClassList("questSlot");
        storedQuest = quest;
        questName = quest.info.name;
        Name.text = questName;
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
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<QuestJournalSlot_UI, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion



}
