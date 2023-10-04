using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfo_SO info;
    public QuestState state;
    private int currentQuestStepIndex;


    public Quest(QuestInfo_SO questInfo) 
    { 
        this.info = questInfo;
        state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
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
            Object.Instantiate(questStepPrefab, parentTransform);
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


}
