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
            if(player.IsInteractButtonDown && !IsResearching)
            {
                IsResearching = true;
                player.OpenResearchInventory(this);
            }

        }
    }

    public void OpenResearchGame(Herb herb)
    {
        player.CloseInventory();
        researchCanvas.SetActive(true);
        
    }

    public void CloseResearchGame()
    {
        researchCanvas.SetActive(false);

    }
}
