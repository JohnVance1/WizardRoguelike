using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ResearchStation : Interactable_Base
{
    public bool IsResearching;

    public GameObject researchCanvas;
    [SerializeField]
    private GameObject[] miniGames;
   

    void Start()
    {
        IsResearching = false;
        researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        researchCanvas.GetComponent<Research_MiniGame>().OnExit += CloseResearchGame;
    }

    private void OnDisable()
    {
        researchCanvas.GetComponent<Research_MiniGame>().OnExit -= CloseResearchGame;

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
        researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

        currentHerb = herb;
    }

    public void CloseResearchGame()
    {
        researchCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        IsResearching = false;
        currentHerb = null;

    }
}
