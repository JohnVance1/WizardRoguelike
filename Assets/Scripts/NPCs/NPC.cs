using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable_Base
{
    public NPC_Lines lines;
    public GameObject dialogueBox;
    public NavMeshAgent agent;

    public GameObject pathOBJ;
    public List<Transform> npcPath;
    public int pathCount;
    public bool IsPaused;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    public void Start()
    {
        lines.NPCname = gameObject.name;
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


    public void Update()
    {
        MoveAgent();

        if(CanInteract)
        {
            if (player.IsInteractButtonDown && dialogueBox.activeInHierarchy == false)
            {
                dialogueBox.SetActive(true);
                player.SetSpeed(0f);
                Dialogue dialogue = dialogueBox.GetComponent<Dialogue>();
                dialogue.lines = lines.lines;
                dialogue.StartDialogue();
                Dialogue.OnEnd += EndDialouge;
            }

            
        }
    }

    public void EndDialouge()
    {
        player.SetSpeed(5f);
    }

    public void MoveAgent()
    {
        if(!CanInteract && !IsPaused)
        {
            if (!player.IsInteractButtonDown)
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
            else
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
