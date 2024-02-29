using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerbJournal_Slot : MonoBehaviour
{
    public Sprite herbSprite;
    public string herbName;
    public Sprite herbElementGraph;
    public string herbInfo;

    public Herb herb_SO;

    private void Awake()
    {
        if (herb_SO)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = herb_SO.sprite;
            herbSprite = herb_SO.sprite;
            herbName = herb_SO.displayName;
            herbElementGraph = herb_SO.graph;
            herbInfo = herb_SO.info;
        }
    }
}
