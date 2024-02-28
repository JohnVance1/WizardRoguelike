using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class ResearchStation : Interactable_Base
{
    public bool IsResearching;

    public GameObject researchCanvas;
    [SerializeField]
    private GameObject[] miniGames;
    public Player player;

    private PlayerControls input;

    void Start()
    {
        IsResearching = false;
        //researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        //researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        //player = Player.Instance;

        input = player.gameObject.GetComponent<Player_Interact>().input;

        input.UI.Cancel.performed += Cancel;
        input.UI.Cancel.Enable();

        //researchCanvas.GetComponent<Research_MiniGame>().OnExit += CloseResearchGame;
    }

    private void OnDisable()
    {
        //input.UI.Cancel.performed -= Cancel;
        input.UI.Cancel.Disable();
        //researchCanvas.GetComponent<Research_MiniGame>().OnExit -= CloseResearchGame;

    }

    public void EnablePlayer()
    {
        input.UI.Disable();
        input.Player.Enable();
    }

    public void EnableUI()
    {
        input.Player.Disable();
        input.UI.Enable();

    }

    void Update()
    {
        if (CanInteract)
        {
            if(playerInteract.IsInteractButtonDown && !IsResearching)
            {
                playerInteract.OpenResearchInventory(this);
            }
            if(IsResearching && miniGames[0].GetComponent<Research_MiniGame>().PathFound)
            {
                if(currentHerb != null)
                {
                    currentHerb.IsResearched = true;

                }
            }

        }
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("OnCancel");
        if(IsResearching)
        {
            CloseResearchGame();

        }

    }

    public void OpenResearchGame(Herb herb)
    {
        playerInteract.CloseInventory();
        //researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        researchCanvas.SetActive(true);
        researchCanvas.GetComponent<Research_MiniGame>().OpenUI();
        IsResearching = true;
        currentHerb = herb;
        EnableUI();

    }

    public void CloseResearchGame()
    {
        //researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        researchCanvas.SetActive(false);
        IsResearching = false;
        currentHerb = null;
        EnablePlayer();
    }
}
