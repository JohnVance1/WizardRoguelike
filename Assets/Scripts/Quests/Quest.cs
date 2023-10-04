using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfo_SO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;


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

    public Quest(QuestInfo_SO questInfo, QuestState state, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = state;
        this.questStepStates = questStepStates;
        this.questStepStates = questStepStates;

        if(this.questStepStates.Length != this.info.questPrereqs.Length)
        {
            Debug.LogWarning("Quest Step Prefab and Quest Step States are of different lengths");
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if(questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitilizeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }

    }

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

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: " + "Quest ID =" + info.id + ", Step Index= " + stepIndex);
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }


}
