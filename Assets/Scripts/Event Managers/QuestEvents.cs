using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvents
{
    public event Action<string> onStartQuest;

    public void StartQuest(string id)
    {
        //onStartQuest?.Invoke(id);
        if (onStartQuest != null)
        {
            onStartQuest(id);
        }
    }

    public event Action<string> onAdvanceQuest;

    public void AdvanceQuest(string id)
    {
        //onStartQuest?.Invoke(id);
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;

    public void FinishQuest(string id)
    {
        //onStartQuest?.Invoke(id);
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }


    public event Action<Quest> onQuestStateChange;

    public void QuestStateChange(Quest quest)
    {
        //onStartQuest?.Invoke(id);
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }
}
