using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cauldron_UI : MonoBehaviour
{
    public GameObject selected;

    public Cauldron cauldron;
    public PotionManager potionManager;

    //public List<Herb> storedHerbs;
    public Herb currentHerb;
    public List<Device> devices;
    public Potion potion;


    public Player player;



    private void Update()
    {
        //if (currentHerb == null)
        //{
        //    foreach (Device d in devices)
        //    {
        //        d.RemoveOnClick();
        //    }
        //}
    }

    private void Start()
    {
        //GetComponent<Button>().onClick.AddListener(() => SetStoredHerb(Object.Instantiate(currentHerb)));

    }

    public void SetRawHerb()
    {
        if (currentHerb != null)
        {
            Instantiate(currentHerb);
            cauldron.storedHerbs.Add(currentHerb);
            cauldron.storedHerbs[cauldron.storedHerbs.Count - 1].processType = ProcessType.Raw;
            currentHerb = null;
            
        }
    }

    public void SetStoredHerb(Herb herb, ProcessType type = ProcessType.Raw)
    {
        if (herb != null)
        {
            cauldron.storedHerbs.Add(herb);
            cauldron.storedHerbs[cauldron.storedHerbs.Count - 1].processType = type;
            //currentHerb = null;
            
        }
    }

    public void MakePotion()
    {
        if (cauldron.storedHerbs.Count > 0)
        {
            Potion p = potionManager.CalculatePotion(cauldron.storedHerbs);
            player.AddItemToInventory(p.info);
            //player.AddItemToInventory(potion.info);

            cauldron.storedHerbs.Clear();
            currentHerb = null;
        }
    }

    public void CancelPotion()
    {
        foreach(Herb h in cauldron.storedHerbs)
        {
            
            player.AddItemToInventory(cauldron.AddBackHerb(h));
        }
        cauldron.storedHerbs.Clear(); 
        currentHerb = null;
    }

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public void UseDevice(Herb herb)
    {
        currentHerb = herb;
        if (currentHerb != null)
        {
            foreach (Device d in devices)
            {
                //d.ChangeOnClick(herb);
                d.selectedHerb = herb;
                //d.GetComponent<Button>().onClick.AddListener(() => DeviceSelected(d));
                if (d.onFinishedDevice == null)
                {
                    d.onFinishedDevice += SetStoredHerb;
                }
                if (d.onStartedDevice == null)
                {
                    d.onStartedDevice += DeviceSelected;
                }
            }
        }
    }

    public void DeviceSelected()
    {
        if (currentHerb != null)
        {
            player.RemoveItemFromInventory(currentHerb);
            currentHerb = null;
            foreach (Device d in devices)
            {
                d.selectedHerb = null;
            }
        }

    }

    public void ChangeStoredUI()
    {

    }



}
