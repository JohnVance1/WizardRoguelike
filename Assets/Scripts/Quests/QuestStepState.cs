using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to save and load quests between exiting the game
/// Need to update to allow for other data to be saved
/// </summary>
[System.Serializable]
public class QuestStepState
{
    public string state;
    public string status;

    public QuestStepState(string state, string status)
    {
        this.state = state;
        this.status = status;

    }

    public QuestStepState() 
    {
        this.state = "";
        this.status = "";

    }
}
