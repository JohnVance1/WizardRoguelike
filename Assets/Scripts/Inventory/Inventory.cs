using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public enum OpenState
{
    None,
    General,
    Farm,
    Cauldron,
    Storage,
    Research,
    Fountain

}



[CreateAssetMenu(fileName = "Inventroy", menuName = "ScriptableObjects/Inventory", order = 1)]

public class Inventory : ScriptableObject
{
    public Dictionary<Item_Base, InventoryItem> itemDictionary = new Dictionary<Item_Base, InventoryItem>();
    [SerializeField]
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;
    public delegate void OnInventoryAdd(InventoryItem item);
    public OnInventoryAdd onInventoryAdd;
    public delegate void OnInventoryRemove(InventoryItem item);
    public OnInventoryRemove onInventoryRemove;

    /// <summary>
    /// Checks if the Inventory contains a specific Item_Base
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(Item_Base item)
    {
        if (itemDictionary.ContainsKey(item))
        {
            return true;
        }
        return false;
    }   

    /// <summary>
    /// Checks the Inventory to see if it contains the 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool DoesInventoryContainItemType<T>()
    {
        foreach (var item in itemDictionary)
        {
            if (item.Key is T)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns the Item_Base and removes it from the Inventory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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

    public InventoryItem ReturnItemFromInventory(InventoryItem item)
    {
        if (inventory.Contains(item))
        {
            return item;
        }

        return null;

    }

    /// <summary>
    /// Adds Item to the player's Inventory
    /// </summary>
    /// <param name="reference"></param>
    public void Add(Item_Base reference)
    {
        InventoryItem item;
        if(itemDictionary.TryGetValue(reference, out InventoryItem value)) 
        {
            value.AddCount();
            item = value;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(reference);
            inventory.Add(newItem);
            itemDictionary.Add(reference, newItem);
            item = newItem;


        }
        if (onInventoryAdd != null)
        {
            onInventoryAdd(item);
        }

    }

    /// <summary>
    /// Removes an Item from the players Inventory
    /// </summary>
    /// <param name="reference"></param>
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
            if (onInventoryRemove != null)
            {
                onInventoryRemove(value);
            }
        }
        

    }




}
