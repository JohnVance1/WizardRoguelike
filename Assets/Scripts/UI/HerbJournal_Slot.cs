using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class HerbJournal_Slot : MonoBehaviour
{
    public Sprite herbSprite;
    public string herbName;
    public GameObject herbElementGraph;
    public string herbInfo;

    public Herb herb_SO;

    private void Awake()
    {
        if (herb_SO)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = herb_SO.sprite;
            herbSprite = herb_SO.sprite;
            if (!herb_SO.IsFound)
            {
                transform.GetChild(0).GetComponent<Image>().color = Color.black;
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(herb_SO) 
        { 
            //if(herb_SO.IsFound) 
            //{
            //    HerbPickedUp();
            //}
            //if(herb_SO.IsResearched)
            //{
            //    HerbResearched();
            //}
        }
    }


    public void HerbPickedUp()
    {
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        herbSprite = herb_SO.sprite;
        herbName = herb_SO.displayName;

    }

    public void HerbResearched()
    {
        herbInfo = herb_SO.info;
    }

    
}
