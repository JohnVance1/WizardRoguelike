using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    public Item_Base item;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;


    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().AddItemToInventory(item);
            if(item is Herb)
            {
                if (!((Herb)item).IsFound)
                {
                    ((Herb)item).IsFound = true;
                    GameEventsManager.instance.journalEvents.FirstHerbCollected((Herb)item);
                }
                GameEventsManager.instance.miscEvents.HerbCollected();

            }
            Destroy(gameObject);
        }
    }
}
