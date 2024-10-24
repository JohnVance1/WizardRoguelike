using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfo_SO info;   // The GameObject that contains most of the quest data
    public QuestState state;    // The current state of the quest
    private int currentQuestStepIndex;  // The current step that the quest is on
    private QuestStepState[] questStepStates;   // Used for storing the quests state when the game closes 

    /// <summary>
    /// Base constructor for when a quest is first made
    /// </summary>
    /// <param name="questInfo"></param>
    public Quest(QuestInfo_SO questInfo) 
    { 
        this.info = questInfo;
        state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
        questStepStates= new QuestStepState[info.questStepPrefabs.Length];
        for(int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    /// <summary>
    /// Constructor for when the quest is being loaded from "memory"
    /// </summary>
    /// <param name="questInfo"></param>
    /// <param name="state"></param>
    /// <param name="currentQuestStepIndex"></param>
    /// <param name="questStepStates"></param>
    public Quest(QuestInfo_SO questInfo, QuestState state, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = state;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if(this.questStepStates.Length != this.info.questPrereqs.Length)
        {
            Debug.LogWarning("Quest Step Prefab and Quest Step States are of different lengths");
        }
    }

    /// <summary>
    /// Advances the quest step to the next step
    /// </summary>
    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    /// <summary>
    /// Checks to see if the curent questStep exists
    /// </summary>
    /// <returns></returns>
    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    /// <summary>
    /// Creates an instance of the current questStep if it exists
    /// </summary>
    /// <param name="parentTransform"></param>
    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if(questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitilizeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }

    }

    /// <summary>
    /// Gets the prefab it needs to spawn if the current quest step exists
    /// </summary>
    /// <returns></returns>
    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
            
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab but stepIndex was out of range:: "
                + "No current quest step: QuestID= " + info.id + ", stepIndex= " + currentQuestStepIndex);
        }
        return questStepPrefab;
    }

    /// <summary>
    /// Stores the quest steps in the QuestStepState array
    /// </summary>
    /// <param name="questStepState"></param>
    /// <param name="stepIndex"></param>
    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
            questStepStates[stepIndex].status = questStepState.status;

        }
        else
        {
            Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: " + "Quest ID =" + info.id + ", Step Index= " + stepIndex);
        }
    }

    /// <summary>
    /// Turns quest into a Serializable format to put into a JSON
    /// </summary>
    /// <returns></returns>
    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }

    public string GetFullStatusText()
    {
        string fullStatus = "";

        if (state == QuestState.REQUIREMENTS_NOT_MET)
        {
            fullStatus = "Requirements are not yet met to start this quest.";
        }
        else if (state == QuestState.CAN_START)
        {
            fullStatus = "This quest can be started!";
        }
        else
        {
            for(int i = 0; i < currentQuestStepIndex; i++)
            {
                fullStatus += $"<s>{questStepStates[i].status}</s>\n";
            }
            if(CurrentStepExists())
            {
                fullStatus += questStepStates[currentQuestStepIndex].status;
            }
            if(state == QuestState.CAN_FINISH)
            {
                fullStatus += "The quest is ready to be turned in!";
            }
            else if (state == QuestState.FINISHED)
            {
                fullStatus += "The quest has been completed!";
            }
        }

        return fullStatus;
    }


}
