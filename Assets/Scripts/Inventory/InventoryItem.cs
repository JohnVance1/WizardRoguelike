using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public Item_Base item { get; private set; }
    public int count { get; private set; }

    public InventoryItem(Item_Base source)
    {
        item = source;
        count = 1;
    }

    public void AddCount()
    {
        count++;
    }
    public void RemoveCount()
    {
        count--;
        if(count <= 0)
        {
            count = 0;
        }
    }
}
