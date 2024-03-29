using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionJournal_Slot : MonoBehaviour
{
    public Sprite potionSprite;
    public string potionName;
    public Sprite potionElementGraph;
    public string potionInfo;

    public PotionInfo_SO potion_SO;

    private void Awake()
    {
        if (potion_SO)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = potion_SO.sprite;
            potionSprite = potion_SO.sprite;
            //potionName = potion_SO.displayName;
            //potionElementGraph = potion_SO.graph;
            //potionInfo = potion_SO.info;
            transform.GetChild(0).GetComponent<Image>().color = Color.black;

        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);

        }

        
    }

    private void OnEnable()
    {
        if (potion_SO)
        {
            if (potion_SO.IsFound)
            {
                PotionFound();
            }
        }
        
    }


    public void PotionFound()
    {
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        potionName = potion_SO.displayName;
        potionInfo = potion_SO.info;

    }
        
}
