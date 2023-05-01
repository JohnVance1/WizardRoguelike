using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fountain : Interactable_Base
{
    public Potion storage;
    public int range = 2;
    public GameObject healedPrefab;
    public Tilemap tilemap;
    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();

    }

    private void Update()
    {
        Interact();
        
    }

    private void HealLand()
    {
        Vector3Int centertile = tilemap.WorldToCell(transform.position);
        Debug.Log(centertile.ToString());

        for(int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (i != 0 || j != 0)
                {
                    Instantiate(healedPrefab, tilemap.GetCellCenterWorld(new Vector3Int(centertile.x + i, centertile.y + j, centertile.z)), Quaternion.identity);
                }
            }
        }

    }

    private void Interact()
    {
        if (CanInteract)
        {
            if (player.IsInteractButtonDown && player.inventory.OfType<Potion>().Any())
            {
                for (int i = 0; i < player.inventory.Count; i++)
                {
                    if (player.inventory[i].GetType() == typeof(Potion))
                    {
                        storage = (Potion)player.inventory[i];
                        player.RemoveItemFromInventory(player.inventory[i]);
                        HealLand();
                    }
                }
            }
            if (player.IsInteractButtonDown)
            {
                Debug.Log("Mouse Button Down!");
            }
        }
    }


}
