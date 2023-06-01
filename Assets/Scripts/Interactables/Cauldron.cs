using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Interactable_Base
{
    public List<Item_Base> storage;
    public Potion potion;

    public GameObject cauldronUI;

    void Start()
    {
        
    }

    void Update()
    {
        if(CanInteract)
        {            
            if (player.IsInteractButtonDown && player.inventory.DoesInventoryContainItemType<Item_Base>())
            {
                player.inventory.GetItemFromInventory<Item_Base>();
                player.AddItemToInventory(potion);
                OpenCauldronUI();
                player.OpenCauldronInventory(this);


            }

        }
        
    }

    public void OpenCauldronUI()
    {
        cauldronUI.SetActive(true);        
    }

    public void CloseCauldronUI()
    {
        cauldronUI.SetActive(false);
    }

    public void SelectCauldron(Herb herb)
    {
        cauldronUI.GetComponent<Cauldron_UI>().UseDevice(herb);
    }



}
