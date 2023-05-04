using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    private const int width = 4;
    private const int height = 5;

    private int xCount;
    private int yCount;

    public Inventory inv;
    public GameObject[,] inventorySlots = new GameObject[height, width];


    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                inventorySlots[i, j] = transform.GetChild(i).GetChild(j).gameObject;

            }

        }

        inv.onInventoryChanged += OnUpdateInventory;
    }

    private void OnEnable()
    {
        OnUpdateInventory();
    }

    private void OnUpdateInventory()
    {
        //foreach(Transform t in transform)
        //{
        //    Destroy(t.gameObject);
        //}
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



}
