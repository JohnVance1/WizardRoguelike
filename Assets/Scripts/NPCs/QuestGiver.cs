using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public string questType = "AskForPotion";
    public Quest quest;
    public bool AssignedQuest;
    public bool QuestCompleted;
    //public GameObject gameManager;
    public QuestsActive questLog;
    public Dictionary<string, Quest> storedQuests;

    protected override void Awake()
    {
        base.Awake();
        AssignedQuest = false;
        QuestCompleted = false;
    }

    protected override void Update()
    {
        base.Update();

        if (CanInteract)
        {
            if (playerInteract.IsInteractButtonDown && dialogueBox.activeInHierarchy == false)
            {

                Dialogue dialogue = dialogueBox.GetComponent<Dialogue>();

                if (questLog.ContainsQuest(quest))
                {
                    AssignedQuest = true;
                }

                if (!AssignedQuest && !QuestCompleted)
                {
                    // Assign Quest
                    dialogue.lines = questStartLines.lines;
                    AssignQuest();
                    dialogue.StartDialogue();
                    Dialogue.OnEnd += EndDialouge;

                }
                else if (AssignedQuest && !QuestCompleted)
                {
                    // Check Quest Status
                    CheckQuestStatus(dialogue);
                }
                else if (QuestCompleted)
                {
                    // Dialouge after Quest is completeed
                    dialogue.lines = questCompleteLines.lines;
                    dialogue.StartDialogue();
                    Dialogue.OnEnd += EndDialouge;
                }

            }
        }

    }

    public void AssignQuest()
    {
        AssignedQuest = true;
        //quest = (Quest)gameManager.AddComponent<AskForPotion>();
        questLog.AddQuest(quest);
        //quest.Active = true;
    }

    public void CheckQuestStatus(Dialogue dialogue)
    {
        if (Player.Instance.inventory.DoesInventoryContainItemType<Potion>() && questLog.ContainsQuest(quest))
        {
            AssignedQuest = false;
            QuestCompleted = true;
            Player.Instance.RemoveItemFromInventory(Player.Instance.inventory.GetItemFromInventory<Potion>());
            questLog.RemoveQuest(quest);
            dialogue.lines = questCompleteLines.lines;
            dialogue.StartDialogue();
            Dialogue.OnEnd += EndDialouge;

        }
        else
        {
            //if (Player.Instance.inventory.DoesInventoryContainItemType<Potion>())
            //{
            //    quest.Completed = true;
            //}
            //else
            //{
            dialogue.lines = questActiveLines.lines;
            dialogue.StartDialogue();
            Dialogue.OnEnd += EndDialouge;
            //}

        }
    }


}
