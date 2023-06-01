using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cauldron_UI : MonoBehaviour
{
    public GameObject selected;

    public Cauldron cauldron;

    public List<Herb> storedHerbs;
    public Herb currentHerb;
    public List<Device> devices;
    public Potion potion;


    public Player player;

    public void SetStoredHerb(Herb herb, ProcessType type = ProcessType.Raw)
    {
        if(herb != null) 
        {
            storedHerbs.Add(herb);
            storedHerbs[storedHerbs.Count - 1].processType = type;
            player.RemoveItemFromInventory(currentHerb);
            currentHerb = null;
        }        
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SetStoredHerb(Object.Instantiate(currentHerb)));

    }

    public void MakePotion()
    {
        if (storedHerbs.Count > 0)
        {
            player.AddItemToInventory(potion);

            storedHerbs.Clear();
            currentHerb = null;
        }
    }

    public void CancelPotion()
    {
        foreach(Herb h in storedHerbs)
        {
            
            player.AddItemToInventory(cauldron.AddBackHerb(h));
        }
        storedHerbs.Clear(); 
        currentHerb = null;
    }

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public void UseDevice(Herb herb)
    {
        currentHerb = herb;
        foreach (Device d in devices)
        {
            d.ChangeOnClick(herb);
            if (d.onFinishedDevice == null)
            {
                d.onFinishedDevice += SetStoredHerb;
            }
        }
    }



}
