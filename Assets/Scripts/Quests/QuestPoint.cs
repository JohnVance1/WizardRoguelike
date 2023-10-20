using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField]
    private QuestInfo_SO questInfoForPoint; //  Info about the quest that the NPC can give

    [Header("Config")]
    [SerializeField]
    private bool startPoint = true; //  Set to true if this is the start point of a quest
    [SerializeField]
    private bool finishPoint = true;//  Set to true if this is the end point of a quest

    private bool playerIsNear = false;

    private string questID;

    private QuestState currentQuestState;

    private void Awake()
    {
        questID = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;

    }

    /// <summary>
    /// Runs whenever the 'Submit' button is pressed
    /// </summary>
    private void SubmitPressed()
    {
        if(!playerIsNear)
        {
            return;
        }

        //  If the player is at the start point of a quest and the quest can be started
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questID);
        }
        // If the player is at the finish point and the quest can be finished
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questID);
        }

    }

    /// <summary>
    /// Updates the state of the quest 
    /// </summary>
    /// <param name="quest"></param>
    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.EndsWith(questID))
        {
            currentQuestState = quest.state;
        }
    }


    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        if(othercollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D othercollider)
    {
        if (othercollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

}
