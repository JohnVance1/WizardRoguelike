using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Windows;

public class Interactable_Base : SerializedMonoBehaviour
{
    public bool CanInteract { get; protected set; }
    protected Player player;
    protected Player_Interact playerInteract;
    public Herb currentHerb;
    public List<Sprite> interactButtons;
    public GameObject interactSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanInteract = true;
            player = collision.GetComponent<Player>();
            playerInteract = player.GetComponent<Player_Interact>();
            DisplayInteractButtons(playerInteract.inputType);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CanInteract = false;
            CloseInteractButton();
        }
    }

    public virtual void SetHerb(Herb herb)
    {
        currentHerb = herb;
    }

    public void DisplayInteractButtons(InputType type)
    {
        if(interactSprite!= null)
        {        
            if(type == InputType.KBM)
            {
                interactSprite.GetComponent<SpriteRenderer>().sprite = interactButtons[0];
            }
            else if(type == InputType.XBox)
            {
                interactSprite.GetComponent<SpriteRenderer>().sprite = interactButtons[1];

            }
            else if(type == InputType.PS)
            {
                interactSprite.GetComponent<SpriteRenderer>().sprite = interactButtons[2];

            }

            interactSprite.SetActive(true);
        }
    }

    public void CloseInteractButton()
    {
        interactSprite.SetActive(false);

    }


}
