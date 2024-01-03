using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cauldron : Interactable_Base
{
    public List<Herb> storedHerbs;

    public GameObject cauldronUI;

    [SerializeField]
    public Dictionary<Type, Herb> allHerbs;

    private bool IsCauldronOpen;

    void Start()
    {
        cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        IsCauldronOpen = false;

    }

    void Update()
    {
        if(CanInteract)
        {            
            if (playerInteract.IsInteractButtonDown && !IsCauldronOpen)// && player.inventory.DoesInventoryContainItemType<Item_Base>())
            {
                //Debug.Log(playerInteract.IsInteractButtonDown);

                //player.inventory.GetItemFromInventory<Item_Base>();
                //player.AddItemToInventory(potion);
                OpenCauldronUI();


            }

            

        }
        
    }

    public Item_Base AddBackHerb(Herb herb)
    {
        allHerbs.TryGetValue(herb.GetType(), out herb);
        return herb;
    }

    public void RemoveHerb(Herb herb)
    {
        storedHerbs.Remove(herb);
    }

    public void OpenCauldronUI()
    {
        //cauldronUI.SetActive(true);
        playerInteract.OpenCauldronInventory(this);
        cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        IsCauldronOpen = true;


    }

    public void CloseCauldronUI()
    {
        cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        IsCauldronOpen = false;
        playerInteract.CloseInventory();

        //cauldronUI.SetActive(false);
    }

    public void SelectCauldron(Herb herb)
    {
        //cauldronUI.GetComponentInChildren<Cauldron_UI>().UseDevice(herb);
        //cauldronUI.GetComponentInChildren<Cauldron_UI>().SetPlayer(player);
        cauldronUI.GetComponentInChildren<PotionUIController>().UseDevice(herb);
        cauldronUI.GetComponentInChildren<PotionUIController>().SetPlayer(player);
    }



}
