using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.Windows;

public class PotionJournal_UI : MonoBehaviour
{
    public List<Button> potionSlots;
    public Image potionImage;
    public TextMeshProUGUI potionName;
    public RadarPolygon potionElementGraph;
    public TextMeshProUGUI potionInfo;

    public Sprite potionSprite_Q;
    public string potionName_Q;
    public GameObject potionElementGraph_Q;
    public string potionInfo_Q;

    public GameObject player;
    private PlayerControls input;

    private void OnEnable()
    {
        input = GetComponentInParent<Player_Interact>().input;
        input.UI.Cancel.performed += Cancel;
        input.UI.Cancel.Enable();
        EventSystem.current.SetSelectedGameObject(potionSlots[0].gameObject);

    }

    private void OnDisable()
    {
        input.UI.Cancel.performed -= Cancel;
        input.UI.Cancel.Disable();
    }

    private void Start()
    {
        player = transform.parent.gameObject;
        Selected(potionSlots[0].GetComponent<PotionJournal_Slot>());
        foreach(Button potion in potionSlots)
        {
            potion.onClick.AddListener(() => Selected(potion.GetComponent<PotionJournal_Slot>()));
        }
    }

    public void OpenUI()
    {
        //Selected(potionSlots[0].GetComponent<PotionJournal_Slot>());


    }

    public void Cancel(InputAction.CallbackContext context)
    {        
        player.GetComponent<Player_Interact>().CloseOpenUI();
    }

    public void Selected(PotionJournal_Slot slot)
    {
        if(slot.potion_SO.IsFound)
        {
            slot.PotionFound();
            ResetRadarGraph(slot.potion_SO);
            SetRadarGraph(slot.potion_SO);
            potionImage.color = Color.white;

            potionImage.sprite = slot.potionSprite;
            potionName.text = slot.potionName;
            //potionElementGraph.sprite = slot.potionElementGraph;
            potionInfo.text = slot.potionInfo;
        }
        else
        {
            ResetRadarGraph(slot.potion_SO);
            potionImage.color = Color.black;

            potionImage.sprite = slot.potionSprite;
            potionName.text = potionName_Q;
            //potionElementGraph.sprite = potionElementGraph_Q;
            potionInfo.text = potionInfo_Q;
        }
        
    }

    public void SetRadarGraph(PotionInfo_SO potion)
    {
        int radarPoint = 0;
        //RadarPolygon radar = potionElementGraph.GetComponent<RadarPolygon>();
        foreach (var ele in potion.elementsNeeded)
        {
            potionElementGraph.value[radarPoint] += Mathf.Clamp((((float)ele.Value) / 5.0f), 0f, 1f);
            potionElementGraph.SetAllDirty();
            radarPoint++;
        }
    }

    public void ResetRadarGraph(PotionInfo_SO potion)
    {
        int radarPoint = 0;
        //RadarPolygon radar = potionElementGraph.GetComponent<RadarPolygon>();
        foreach (var ele in potion.elementsNeeded)
        {
            potionElementGraph.value[radarPoint] = 0.1f;
            potionElementGraph.SetAllDirty();
            radarPoint++;
        }
    }



}
