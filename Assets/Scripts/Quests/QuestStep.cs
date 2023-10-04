using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuestStep()
    {
        if(!isFinished)
        {
            isFinished = true;

            // TODO - Advance quest foward since we finished the step

            Destroy(gameObject);

        }
    }   

}
