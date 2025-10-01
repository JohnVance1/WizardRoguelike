using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        SetHerb(storedHerb);
    }

    private void Update()
    {
        if (CanInteract)
        {
            if (playerInteract.IsInteractButtonDown && player.inventory.Contains(defaultHerb) && storedHerb == null)
            {
                //Herb playerHerb = (Herb)player.inventory.GetItemFromInventory<Herb>();
                //SetHerb(playerHerb);
                //playerInteract.OpenFarmInventory(this);

            }

            if(playerInteract.IsInteractButtonDown && CanHarvest && storedHerb != null)
            {
                HarvestHerb();
            }


        }
    }

    public void HarvestHerb()
    {
        if (!(storedHerb).IsFound)
        {
            (storedHerb).IsFound = true;
            GameEventsManager.instance.journalEvents.FirstHerbCollected(storedHerb);
        }
        player.AddItemToInventory(storedHerb);
        GameEventsManager.instance.miscEvents.HerbCollected();
        herbGO.GetComponent<SpriteRenderer>().sprite = null;
        CanHarvest = false;
        StartGrowing();
    }

    public override void SetHerb(Herb herb)
    {
        storedHerb = herb;
        herbStages = herb.herbStages;
        //player.RemoveItemFromInventory(herb);
        //playerInteract.CloseInventory();
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
