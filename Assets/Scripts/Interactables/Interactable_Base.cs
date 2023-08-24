using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Interactable_Base : SerializedMonoBehaviour
{
    public bool CanInteract { get; protected set; }
    protected Player player;
    protected Player_Interact playerInteract;
    public Herb currentHerb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanInteract = true;
            player = collision.GetComponent<Player>();
            playerInteract = player.GetComponent<Player_Interact>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CanInteract = false;
        }
    }

    public virtual void SetHerb(Herb herb)
    {
        currentHerb = herb;
    }
}
