using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField]
    private QuestInfo_SO questInfoForPoint;

    [Header("Config")]
    [SerializeField]
    private bool startPoint = true;
    [SerializeField]
    private bool finishPoint = true;

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

    private void SubmitPressed()
    {
        if(!playerIsNear)
        {
            return;
        }

        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questID);
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questID);
        }

    }

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
