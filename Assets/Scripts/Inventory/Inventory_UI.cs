using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    private const int width = 4;
    private const int height = 5;

    private int xCount;
    private int yCount;

    public Inventory inv;
    public InventorySlot selected;
    public GameObject[,] inventorySlots = new GameObject[height, width];

    public Button[] inventoryButtons = new Button[height * width];

    public OpenState state;

    public Interactable_Base interactable;



    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                inventorySlots[i, j] = transform.GetChild(i).GetChild(j).gameObject;
                
            }

        }

        //inv.onInventoryChanged += OnUpdateInventory;
    }

    private void Update()
    {
        //if(selected != null)
        //{
        //    if(state == OpenState.Farm)
        //    {
        //        if (selected.storedItem.item is Herb)
        //        {
        //            ((FarmPlot)interactable).SetHerb((Herb)selected.storedItem.item);
        //        }
        //    }

        //}
    }

    public void ButtonCallback(InventorySlot slot)
    {
        selected = slot;
        if (selected != null)
        {
            switch(state)
            {
                case OpenState.Farm:
                    if (selected.storedItem.item is Herb)
                    {
                        ((FarmPlot)interactable).SetHerb((Herb)selected.storedItem.item);
                    }
                    break;
                case OpenState.Research:
                    if (selected.storedItem.item is Herb)
                    {
                        if (!((Herb)selected.storedItem.item).IsResearched)
                        {
                            ((ResearchStation)interactable).OpenResearchGame((Herb)selected.storedItem.item);
                        }
                        else
                        {
                            Debug.Log("This Herb has been researched already!");
                        }
                    }
                    break;
                case OpenState.Cauldron:
                    if (selected.storedItem.item is Herb)
                    {
                        if (((Herb)selected.storedItem.item).IsResearched)
                        {
                            ((Cauldron)interactable).SelectCauldron((Herb)selected.storedItem.item);
                        }
                        else
                        {
                            Debug.Log("This Herb needs to be researched!!");
                        }
                    }
                    break;
                case OpenState.None:
                    Debug.Log("Not open in a valid state!");
                    break;
            }
            
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                inventoryButtons[(i * 4) + j] = inventorySlots[i, j].GetComponent<Button>();
                InventorySlot s = inventorySlots[i, j].GetComponent<InventorySlot>();
                inventoryButtons[(i * 4) + j].onClick.AddListener(() => ButtonCallback(s));
            }

        }
        OnUpdateInventory();

    }

    private void OnUpdateInventory()
    {        
        xCount = 0;
        yCount = 0;

        DrawInventory();
    }

    public void DrawInventory()
    {
        InventorySlot[] slotList = transform.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slotList)
        {
            slot.Clear();
        }

        foreach(InventoryItem item in inv.inventory)
        {
            AddInventorySlot(item);
        }

        
    }

    public void AddInventorySlot(InventoryItem item)
    {
        inventorySlots[yCount, xCount].GetComponent<InventorySlot>().Set(item);

        xCount++;
        if(xCount >= height)
        {
            xCount = 0;
            yCount++;
            if(yCount >= width) 
            { 
                yCount = 0;
            }
        }

    }

    public void FarmState()
    {
        

    }



}
