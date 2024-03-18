using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Fountain : Interactable_Base
{
    public List<PotionInfo_SO> storageNeeded;
    public List<PotionInfo_SO> storageCurrent;

    public int storageNum = 4;

    private bool IsFountainOpen;
    public GameObject fountainUI;
    private PlayerControls input;

    [SerializeField]
    public List<PotionInfo_SO> allPotions;
    
    PotionInfo_SO currentPotion;

    private void Awake()
    {
        storageCurrent = new List<PotionInfo_SO>();
    }
    private void OnEnable()
    {
        input = Player.Instance.gameObject.GetComponent<Player_Interact>().input;

        input.UI.Cancel.performed += Cancel;
        input.UI.Cancel.Enable();

    }

    private void OnDisable()
    {
        input.UI.Cancel.performed -= Cancel;
        input.UI.Cancel.Disable();

    }

    private void Update()
    {
        Interact();
        if(CheckPotions())
        {
            HealLand();
        }
    }

    /// <summary>
    /// Runs when all of the potions meet the storage requirements
    /// </summary>
    private void HealLand()
    {
        Debug.Log("All Potions Correct");

    }

    private void Interact()
    {
        if (CanInteract)
        {
            if (playerInteract.IsInteractButtonDown && !IsFountainOpen)// && player.inventory.DoesInventoryContainItemType<Item_Base>())
            {
                OpenFountainUI();
            }
            //if (player.IsInteractButtonDown && player.inventory.inventory.OfType<Potion>().Any())
            //{
            //    for (int i = 0; i < player.inventory.inventory.Count; i++)
            //    {
            //        if (player.inventory.inventory[i].GetType() == typeof(Potion))
            //        {
            //            storage = (Potion)player.inventory.inventory[i];
            //            player.RemoveItemFromInventory(player.inventory[i].inventory);
            //            toHeal = true;
            //            //HealLand();
            //        }
            //    }
            //}
            //if (player.IsInteractButtonDown)
            //{
            //    Debug.Log("Mouse Button Down!");
            //}
        }
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        if (IsFountainOpen)
        {
            CloseFountainUI();
        }
    }

    public void OpenFountainUI()
    {
        playerInteract.OpenFountainInventory(this);
        fountainUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        IsFountainOpen = true;
    }

    public void CloseFountainUI()
    {
        fountainUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        IsFountainOpen = false;
        playerInteract.CloseInventory();
    }

    public void SelectFountain(PotionInfo_SO potion)
    {
        currentPotion = potion;
        fountainUI.GetComponentInChildren<Fountain_UI>().SetPotion(potion, player);
    }

    public Item_Base AddBackPotion(PotionInfo_SO potion)
    {
        //allPotions.Find(potion);
        return potion;
    }

    public void RemovePotion(PotionInfo_SO potion)
    {
        storageCurrent.Remove(potion);
    }

    public bool CheckPotions()
    {
        bool allCorrect = false;
        if(storageCurrent.Count > 0)
        {
            foreach (var potion in storageCurrent)
            {
                if (!storageNeeded.Contains(potion))
                {
                    allCorrect = false;
                }

            }
        }
        
        return allCorrect;
    }



}
