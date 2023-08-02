using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestsActive : ScriptableObject
{
    public List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }
    public void RemoveQuest(Quest quest)
    {
        activeQuests.Remove(quest);
    }

    public bool ContainsQuest(Quest quest)
    {
        return activeQuests.Contains(quest);
    }


}
