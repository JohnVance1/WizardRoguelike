using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FarmPlot : Interactable_Base
{
    public Herb storedHerb;
    public Herb defaultHerb;

    public int currentStage;

    private Sprite[] herbStages;

    [SerializeField]
    private GameObject herbGO;

    private bool CanHarvest;


    private void Start()
    {
        CanHarvest = false;
    }

    private void Update()
    {
        if (CanInteract)
        {
            if (player.IsInteractButtonDown && player.inventory.Contains(defaultHerb) && storedHerb == null)
            {
                //Herb playerHerb = (Herb)player.inventory.GetItemFromInventory<Herb>();
                //SetHerb(playerHerb);
                player.OpenFarmInventory(this);

            }

            if(player.IsInteractButtonDown && CanHarvest && storedHerb != null)
            {
                HarvestHerb();
            }


        }
    }

    public void HarvestHerb()
    {
        if(CanHarvest)
        {
            player.AddItemToInventory(storedHerb);
            storedHerb = null;
            herbGO.GetComponent<SpriteRenderer>().sprite = null;
            CanHarvest = false;
        }
    }

    public void SetHerb(Herb herb)
    {
        storedHerb = herb;
        herbStages = herb.herbStages;
        player.RemoveItemFromInventory(herb);
        player.CloseInventory();
        StartGrowing();
    }

    public void StartGrowing()
    {
        if(storedHerb != null)
        {
            StartCoroutine(UpdateHerbStage());
        }
    }

    public IEnumerator UpdateHerbStage()
    {
        foreach(Sprite s in herbStages)
        {
            herbGO.GetComponent<SpriteRenderer>().sprite = s;
            if(s == herbStages[herbStages.Length-1])
            {
                CanHarvest = true;
            }
            yield return new WaitForSeconds(storedHerb.timeToGrow);
        }

    }


}
