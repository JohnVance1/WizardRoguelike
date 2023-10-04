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
        }

        if(herbsCollected >= herbsToCollect)
        {
            FinishQuestStep();
        }
    }
}
