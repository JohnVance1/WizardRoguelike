using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHerbQuestStep : QuestStep
{
    private int herbsCollected = 0;

    private int herbsToCollect = 2;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onHerbCollected += HerbCollected;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onHerbCollected -= HerbCollected;

    }

    private void HerbCollected()
    {
        if(herbsCollected < herbsToCollect)
        {
            herbsCollected++;
            UpdateState();
        }

        if(herbsCollected >= herbsToCollect)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = herbsCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.herbsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
