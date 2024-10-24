using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : Interactable_Base
{
    [Header("Quest")]
    [SerializeField]
    private QuestInfo_SO questInfoForPoint; //  Info about the quest that the NPC can give
    public QuestInfo_SO QuestInfo
    {
        get { return questInfoForPoint; }
        private set { questInfoForPoint = value; }
    } //  Info about the quest that the NPC can give


    [Header("Config")]
    [SerializeField]
    private bool startPoint = true; //  Set to true if this is the start point of a quest
    [SerializeField]
    private bool finishPoint = true;//  Set to true if this is the end point of a quest

    private bool playerIsNear = false;

    [SerializeField] private bool IsNPC;

    private string questID;

    public QuestState currentQuestState { get; private set; }

    private UnityAction<QuestState> onQuestStateChange;
    private UnityAction<QuestInfo_SO> onQuestFinished;


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
    public void SubmitPressed()
    {
        if(!IsNPC && CanInteract)
        {           
            SubmitCode();
        }        
    }

    public void SubmitCode()
    {
        //  If the player is at the start point of a quest and the quest can be started
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questID);
        }
        // If the player is at the finish point and the quest can be finished
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questID);
            //onQuestFinished(questInfoForPoint);
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
            //onQuestStateChange(currentQuestState);
        }
    }

}
