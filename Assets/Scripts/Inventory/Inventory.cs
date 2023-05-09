using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum OpenState
{
    None,
    General,
    Farm,
    Cauldron,
    Storage

}



[CreateAssetMenu(fileName = "Inventroy", menuName = "ScriptableObjects/Inventory", order = 1)]

public class Inventory : ScriptableObject
{
    public Dictionary<Item_Base, InventoryItem> itemDictionary = new Dictionary<Item_Base, InventoryItem>();
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;


    public bool Contains(Item_Base item)
    {
        if (itemDictionary.ContainsKey(item))
        {
            return true;
        }
        return false;
    }   

    public Item_Base GetItemFromInventory<T>()
    {
        foreach(var item in itemDictionary)
        {
            if(item.Key is T)
            {
                Item_Base reference = item.Key;
                Remove(item.Key);
                return reference;
            }
        }

        return null;

    }

    public void Add(Item_Base reference)
    {
        if(itemDictionary.TryGetValue(reference, out InventoryItem value)) 
        {
            value.AddCount();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(reference);
            inventory.Add(newItem);
            itemDictionary.Add(reference, newItem);            

        }
        if (onInventoryChanged != null)
        {
            onInventoryChanged();
        }
    }

    public void Remove(Item_Base reference)
    {
        if (itemDictionary.TryGetValue(reference, out InventoryItem value))
        {
            value.RemoveCount();

            if(value.count <= 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(reference);
            }
        }
        if (onInventoryChanged != null)
        {
            onInventoryChanged();
        }

    }




}
