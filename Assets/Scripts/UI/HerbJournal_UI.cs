using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HerbJournal_UI : MonoBehaviour
{
    public List<Button> herbSlots;
    public Image herbImage;
    public TextMeshProUGUI herbName;
    public Image herbElementGraph;
    public TextMeshProUGUI herbInfo;

    public Sprite herbSprite_Q;
    public string herbName_Q;
    public Sprite herbElementGraph_Q;
    public string herbInfo_Q;

    private void Start()
    {
        Selected(herbSlots[0].GetComponent<HerbJournal_Slot>());
        foreach (Button herb in herbSlots)
        {
            herb.onClick.AddListener(() => Selected(herb.GetComponent<HerbJournal_Slot>()));
        }
    }

    public void Selected(HerbJournal_Slot slot)
    {
        if (slot.herb_SO.IsFound)
        {
            herbImage.sprite = slot.herbSprite;
            herbName.text = slot.herbName;
            herbElementGraph.sprite = slot.herbElementGraph;
            herbInfo.text = slot.herbInfo;
        }
        else
        {
            herbImage.sprite = herbSprite_Q;
            herbName.text = herbName_Q;
            herbElementGraph.sprite = herbElementGraph_Q;
            herbInfo.text = herbInfo_Q;
        }
    }
}
