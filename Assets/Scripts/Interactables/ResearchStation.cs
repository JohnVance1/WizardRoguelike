using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchStation : Interactable_Base
{
    public bool IsResearching;

    public GameObject researchCanvas;
    [SerializeField]
    private GameObject[] miniGames;
   

    void Start()
    {
        IsResearching = false;
    }

    void Update()
    {
        if (CanInteract)
        {
            if(playerInteract.IsInteractButtonDown && !IsResearching)
            {
                IsResearching = true;
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

    public void OpenResearchGame(Herb herb)
    {
        playerInteract.CloseInventory();
        researchCanvas.SetActive(true);
        currentHerb = herb;
    }

    public void CloseResearchGame()
    {
        researchCanvas.SetActive(false);
        IsResearching = false;
        currentHerb = null;

    }
}
