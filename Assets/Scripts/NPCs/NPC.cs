using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable_Base
{
    public NPC_Lines questCompleteLines;
    public NPC_Lines questActiveLines;
    public NPC_Lines questStartLines;

    public GameObject dialogueBox;
    public NavMeshAgent agent;

    public GameObject pathOBJ;
    public List<Transform> npcPath;
    public int pathCount;
    public bool IsPaused;

    

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        

    }
    public virtual void Start()
    {
        questCompleteLines.NPCname = gameObject.name;
        questActiveLines.NPCname = gameObject.name;
        questStartLines.NPCname = gameObject.name;

        

        pathCount = 0;
        IsPaused = false;
        SetPath();
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }


    public void SetPath()
    {
        foreach(Transform t in pathOBJ.transform) 
        {
            npcPath.Add(t);
        }
    }


    protected virtual void Update()
    {
        MoveAgent();

        if(CanInteract)
        {
            if (playerInteract.IsInteractButtonDown && dialogueBox.activeInHierarchy == false)
            {
                dialogueBox.SetActive(true);
                player.SetSpeed(0f);
                Dialogue dialogue = dialogueBox.GetComponent<Dialogue>();
                //if(questLog.ContainsQuest(quest))
                //{
                //    AssignedQuest = true;
                //}

                //if (!AssignedQuest && !QuestCompleted)
                //{
                //    // Assign Quest
                //    dialogue.lines = questStartLines.lines;
                //    AssignQuest();
                //    dialogue.StartDialogue();
                //    Dialogue.OnEnd += EndDialouge;

                //}
                //else if(AssignedQuest && !QuestCompleted)
                //{
                //    // Check Quest Status
                //    CheckQuestStatus(dialogue);
                //}
                //else if(QuestCompleted)
                //{
                //    // Dialouge after Quest is completeed
                //    dialogue.lines = questCompleteLines.lines;
                //    dialogue.StartDialogue();
                //    Dialogue.OnEnd += EndDialouge;
                //}
                

            }

            
        }
    }

    public void EndDialouge()
    {
        player.SetSpeed(5f);
        dialogueBox.SetActive(false);
        Dialogue.OnEnd -= EndDialouge;
    }

    public void MoveAgent()
    {
        if(!CanInteract && !IsPaused)
        {
            if (!playerInteract.IsInteractButtonDown)
            {
                agent.isStopped = false;
                agent.SetDestination(npcPath[pathCount].position);
                Vector3 npcPos = transform.position;
                Vector3 npcDes = agent.destination;
                npcPos.z = 0;
                npcDes.z = 0;

                if (Vector3.Distance(npcPos, npcDes) <= 0.1f)
                {
                    StartCoroutine(PauseMovement(2f));
                }
            }
            if(dialogueBox.activeInHierarchy == true)
            {
                agent.isStopped = true;
            }
        }
        

    }

    public IEnumerator PauseMovement(float pauseTime)
    {
        IsPaused = true;
        yield return new WaitForSeconds(pauseTime);
        pathCount++;
        if(pathCount >= npcPath.Count)
        {
            pathCount = 0;
        }
        IsPaused = false;

    }

    



}
