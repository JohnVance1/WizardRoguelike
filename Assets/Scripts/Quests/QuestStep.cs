using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questID; //  The ID of the quest
    private int stepIndex;  //  Which step the quest is currently on in the array of quest steps

    /// <summary>
    /// Initilizes the quest step
    /// Runs whenever a new Quest step starts
    /// </summary>
    /// <param name="questID"></param>
    /// <param name="stepIndex"></param>
    /// <param name="questStepState"></param>
    public void InitilizeQuestStep(string questID, int stepIndex, string questStepState)
    {
        this.questID = questID;
        this.stepIndex = stepIndex;
        if(questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }

    /// <summary>
    /// Runs whenever the quest step finishes
    /// </summary>
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            GameEventsManager.instance.questEvents.AdvanceQuest(questID);

            Destroy(gameObject);

        }
    }

    /// <summary>
    /// Saves the quest state whever it updates
    /// Used for saving after the game exits
    /// </summary>
    /// <param name="newState"></param>
    protected void ChangeState(string newState, string newStatus)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(
            questID, 
            stepIndex, 
            new QuestStepState(newState, newStatus));
    }

    /// <summary>
    /// Sets the quest's state when the game loads
    /// </summary>
    /// <param name="state"></param>
    protected abstract void SetQuestStepState(string state);

}
