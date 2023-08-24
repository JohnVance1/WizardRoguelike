using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : ScriptableObject
{
    public List<Quest> ActiveQuests;
    public List<Quest> CompletedQuests;
    public Dictionary<string, Quest> AllQuests;


    void ActivateQuest(string quest)
    {
        ActiveQuests.Add(AllQuests[quest]);
    }

    void CompleteQuest(string quest)
    {
        Quest temp = AllQuests[quest];
        ActiveQuests.Remove(temp);
        CompletedQuests.Add(temp);

    }



}
