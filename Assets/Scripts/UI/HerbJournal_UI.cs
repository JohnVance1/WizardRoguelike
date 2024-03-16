using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.Windows;

public class HerbJournal_UI : MonoBehaviour
{
    public List<Button> herbSlots;
    public Image herbImage;
    public TextMeshProUGUI herbName;
    public RadarPolygon herbElementGraph;
    public TextMeshProUGUI herbInfo;

    public Sprite herbSprite_Q;
    public string herbName_Q;
    public RadarPolygon herbElementGraph_Q;
    public string herbInfo_Q;

    public GameObject player;
    private PlayerControls input;

    private void OnEnable()
    {
        input = GetComponentInParent<Player_Interact>().input;
        input.UI.Cancel.performed += Cancel;
        input.UI.Cancel.Enable();
        EventSystem.current.SetSelectedGameObject(herbSlots[0].gameObject);
    }

    private void OnDisable()
    {
        input.UI.Cancel.performed -= Cancel;
        input.UI.Cancel.Disable();
    }

    private void Start()
    {
        player = transform.parent.gameObject;
        Selected(herbSlots[0].GetComponent<HerbJournal_Slot>());
        foreach (Button herb in herbSlots)
        {
            herb.onClick.AddListener(() => Selected(herb.GetComponent<HerbJournal_Slot>()));
        }
    }

    

    public void OpenUI()
    {
        

    }

    public void Cancel(InputAction.CallbackContext context)
    {
        player.GetComponent<Player_Interact>().CloseOpenUI();
    }

    public void Selected(HerbJournal_Slot slot)
    {
        if (slot.herb_SO.IsFound && !slot.herb_SO.IsResearched)
        {
            slot.HerbPickedUp();
            ResetRadarGraph(slot.herb_SO);
            herbImage.sprite = slot.herbSprite;
            herbName.text = slot.herbName;
            herbImage.color = Color.white;
            herbInfo.text = herbInfo_Q;

        }
        else if (slot.herb_SO.IsResearched)
        {
            slot.HerbResearched();
            slot.HerbPickedUp();
            ResetRadarGraph(slot.herb_SO);
            SetRadarGraph(slot.herb_SO);
            herbImage.sprite = slot.herbSprite;
            herbName.text = slot.herbName;
            herbImage.color = Color.white;
            herbInfo.text = slot.herbInfo;
        }
        else
        {
            ResetRadarGraph(slot.herb_SO);
            herbImage.color = Color.black;
            herbImage.sprite = slot.herbSprite;
            herbName.text = herbName_Q;
            //herbElementGraph = herbElementGraph_Q;
            herbInfo.text = herbInfo_Q;
        }
    }

    public void SetRadarGraph(Herb herb)
    {
        int radarPoint = 0;
        //RadarPolygon radar = herbElementGraph.GetComponent<RadarPolygon>();
        foreach (var ele in herb.elements)
        {
            herbElementGraph.value[radarPoint] += Mathf.Clamp((((float)ele.Value) / 5.0f), 0f, 1f);
            herbElementGraph.SetAllDirty();
            radarPoint++;
        }
    }

    public void ResetRadarGraph(Herb herb)
    {
        int radarPoint = 0;
        //RadarPolygon radar = herbElementGraph.GetComponent<RadarPolygon>();
        foreach (var ele in herb.elements)
        {
            herbElementGraph.value[radarPoint] = 0.1f;
            herbElementGraph.SetAllDirty();
            radarPoint++;
        }
    }
}
