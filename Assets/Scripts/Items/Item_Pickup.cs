using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    public Item_Base item;


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().AddItemToInventory(item);
            if(item is Herb)
            {
                GameEventsManager.instance.miscEvents.HerbCollected();
            }
            Destroy(gameObject);
        }
    }
}
