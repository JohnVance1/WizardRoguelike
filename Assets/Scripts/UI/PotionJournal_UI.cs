using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PotionJournal_UI : MonoBehaviour
{
    public List<Button> potionSlots;
    public Image potionImage;
    public TextMeshProUGUI potionName;
    public Image potionElementGraph;
    public TextMeshProUGUI potionInfo;

    public Sprite potionSprite_Q;
    public string potionName_Q;
    public Sprite potionElementGraph_Q;
    public string potionInfo_Q;

    private void Start()
    {
        Selected(potionSlots[0].GetComponent<PotionJournal_Slot>());
        foreach(Button potion in potionSlots)
        {
            potion.onClick.AddListener(() => Selected(potion.GetComponent<PotionJournal_Slot>()));
        }
    }

    public void Selected(PotionJournal_Slot slot)
    {
        if(slot.potion_SO.IsFound)
        {
            potionImage.sprite = slot.potionSprite;
            potionName.text = slot.potionName;
            potionElementGraph.sprite = slot.potionElementGraph;
            potionInfo.text = slot.potionInfo;
        }
        else
        {
            potionImage.sprite = potionSprite_Q;
            potionName.text = potionName_Q;
            potionElementGraph.sprite = potionElementGraph_Q;
            potionInfo.text = potionInfo_Q;
        }
    }



}
