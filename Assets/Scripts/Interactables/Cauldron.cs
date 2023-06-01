using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Interactable_Base
{
    public List<Item_Base> storage;

    public GameObject cauldronUI;

    [SerializeField]
    public Dictionary<Type, Herb> allHerbs;

    void Start()
    {
        
    }

    void Update()
    {
        if(CanInteract)
        {            
            if (player.IsInteractButtonDown && player.inventory.DoesInventoryContainItemType<Item_Base>())
            {
                //player.inventory.GetItemFromInventory<Item_Base>();
                //player.AddItemToInventory(potion);
                OpenCauldronUI();
                player.OpenCauldronInventory(this);


            }

        }
        
    }

    public Herb AddBackHerb(Herb herb)
    {
        allHerbs.TryGetValue(herb.GetType(), out herb);
        return herb;
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
        cauldronUI.transform.GetChild(0).GetComponent<Cauldron_UI>().UseDevice(herb);
        cauldronUI.transform.GetChild(0).GetComponent<Cauldron_UI>().SetPlayer(player);
    }



}
