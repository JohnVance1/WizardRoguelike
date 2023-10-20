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

    /// <summary>
    /// Adds up the Herbs collected until the amount needed is reached
    /// </summary>
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

    /// <summary>
    /// Updates the state of the quest for saving after the game exits
    /// </summary>
    private void UpdateState()
    {
        string state = herbsCollected.ToString();
        ChangeState(state);
    }

    /// <summary>
    /// Sets the state of the quest when the game loads
    /// </summary>
    /// <param name="state"></param>
    protected override void SetQuestStepState(string state)
    {
        this.herbsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
