using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuestPoint))]
public class QuestGiver : NPC
{
    public string questType = "AskForPotion";
    public QuestInfo_SO quest;
    public bool AssignedQuest;
    public bool QuestCompleted;
    //public GameObject gameManager;
    public QuestsActive questLog;
    public Dictionary<string, Quest> storedQuests;

    private QuestPoint questPoint;

    protected override void Awake()
    {
        base.Awake();        
        questPoint = GetComponent<QuestPoint>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

    }
    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

    }

    protected override void Update()
    {
        base.Update();


    }

    private void SubmitPressed()
    {
        if(!CanInteract)
        {
            return;
        }
        AssignQuest();
        //if(dialogueBox.activeInHierarchy == false)
        //{
        //    Dialogue dialogue = dialogueBox.GetComponent<Dialogue>();


        //    switch (questPoint.currentQuestState)
        //    { 
        //        case QuestState.REQUIREMENTS_NOT_MET:
        //            break;
        //        case QuestState.CAN_START:
        //            dialogue.lines = questStartLines.lines;
        //            AssignQuest();
        //            dialogue.StartDialogue();
        //            Dialogue.OnEnd += EndDialouge;
        //            break;
        //        case QuestState.IN_PROGRESS:
        //            dialogue.lines = questActiveLines.lines;
        //            dialogue.StartDialogue();
        //            Dialogue.OnEnd += EndDialouge;
        //            break;
        //        case QuestState.CAN_FINISH:
        //            break;
        //        case QuestState.FINISHED:
        //            // Dialouge after Quest is completeed
        //            dialogue.lines = questCompleteLines.lines;
        //            dialogue.StartDialogue();
        //            Dialogue.OnEnd += EndDialouge;
        //            break;
        //        default:
        //            Debug.Log("Quest is not in an accepted QuestState");
        //            break;

        //    }

        //}


    }

    public void AssignQuest()
    {
        questPoint.SubmitCode();
    }

    //public void CheckQuestStatus(Dialogue dialogue)
    //{
    //    if (Player.Instance.inventory.DoesInventoryContainItemType<Potion>() && questLog.ContainsQuest(quest))
    //    {
    //        AssignedQuest = false;
    //        QuestCompleted = true;
    //        Player.Instance.RemoveItemFromInventory(Player.Instance.inventory.GetItemFromInventory<Potion>());
    //        questLog.RemoveQuest(quest);
    //        dialogue.lines = questCompleteLines.lines;
    //        dialogue.StartDialogue();
    //        Dialogue.OnEnd += EndDialouge;

    //    }
    //    else
    //    {
            
    //        dialogue.lines = questActiveLines.lines;
    //        dialogue.StartDialogue();
    //        Dialogue.OnEnd += EndDialouge;
            

    //    }
    //}

    /// <summary>
    /// Runs when the quest is finished
    /// </summary>
    /// <param name="id"></param>
    private void FinishQuest(string id)
    {
        QuestInfo_SO quest = questPoint.QuestInfo;

        if (quest.expReward > 0)
        {
            GainEnvironmentalProgress(quest.expReward);
        }
    }

    public void GainEnvironmentalProgress(float amountGained)
    {
        currentEnviroProgress += amountGained;
        if(currentEnviroProgress > maxEnviroProgress)
        {
            currentEnviroProgress = maxEnviroProgress;
        }
    }


}
