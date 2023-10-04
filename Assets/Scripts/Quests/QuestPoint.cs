using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField]
    private QuestInfo_SO questInfoForPoint;


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

    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;

    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.EndsWith(questID))
        {
            currentQuestState = quest.state;
            Debug.Log("Quest with id: " + questID + " updated to state: " + currentQuestState);
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
