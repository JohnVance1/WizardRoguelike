using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questID;
    private int stepIndex;

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

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            GameEventsManager.instance.questEvents.AdvanceQuest(questID);

            Destroy(gameObject);

        }
    }

    protected void ChangeState(string newState)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(questID, stepIndex, new QuestStepState(newState));
    }

    protected abstract void SetQuestStepState(string state);

}
