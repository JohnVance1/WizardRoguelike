using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Interactable_Base
{
    public Item_Base storage;
    public Potion potion;

    void Start()
    {
        
    }

    void Update()
    {
        if(CanInteract)
        {            
            if (player.IsInteractButtonDown && player.inventory.Contains(storage))
            {
                player.inventory.GetItemFromInventory<Item_Base>();
                player.AddItemToInventory(potion);

            }
            if (player.IsInteractButtonDown)
            {
                Debug.Log("Mouse Button Down!");
            }
        }
        
    }

   
}
