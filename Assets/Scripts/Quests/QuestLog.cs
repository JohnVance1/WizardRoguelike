using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "QuestLog", menuName = "ScriptableObjects/QuestLog", order = 1)]
public class QuestLog : ScriptableObject
{
    public List<Quest> ActiveQuests = new List<Quest>();
    public List<Quest> CompletedQuests = new List<Quest>();
    public Dictionary<string, Quest> AllQuests;

    

    public void ActivateQuest(string quest)
    {
        ActiveQuests.Add(AllQuests[quest]);
    }

    public void CompleteQuest(string quest)
    {
        Quest temp = AllQuests[quest];
        ActiveQuests.Remove(temp);
        CompletedQuests.Add(temp);

    }



}
