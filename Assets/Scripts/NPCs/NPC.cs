using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable_Base
{
    public NPC_Lines lines;
    public GameObject dialogueBox;

    public void Start()
    {
        lines.NPCname = gameObject.name;
    }

    public void Update()
    {
        if(CanInteract)
        {
            if (player.IsInteractButtonDown && !dialogueBox.activeSelf)
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


}
