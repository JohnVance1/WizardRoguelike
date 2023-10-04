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

    void Start()
    {
        //cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        //cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;

    }

    void Update()
    {
        if(CanInteract)
        {            
            if (playerInteract.IsInteractButtonDown)// && player.inventory.DoesInventoryContainItemType<Item_Base>())
            {
                //player.inventory.GetItemFromInventory<Item_Base>();
                //player.AddItemToInventory(potion);
                OpenCauldronUI();
                playerInteract.OpenCauldronInventory(this);


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
        //cauldronUI.SetActive(true);
        cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void CloseCauldronUI()
    {
        playerInteract.CloseInventory();
        cauldronUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
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
