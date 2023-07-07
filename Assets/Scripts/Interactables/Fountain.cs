using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fountain : Interactable_Base
{
    public Potion storage;
    public int range = 2;
    public GameObject healedPrefab;
    public Tilemap tilemap;
    public bool toHeal;
    public bool reclaim;
    private float timer;
    public List<Vector3Int> healedTiles = new List<Vector3Int>();

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
        reclaim = false;
        timer = 0;
    }

    private void Update()
    {
        Interact();
        PotionTimer();
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
            //if (player.IsInteractButtonDown && player.inventory.inventory.OfType<Potion>().Any())
            //{
            //    for (int i = 0; i < player.inventory.inventory.Count; i++)
            //    {
            //        if (player.inventory.inventory[i].GetType() == typeof(Potion))
            //        {
            //            storage = (Potion)player.inventory.inventory[i];
            //            player.RemoveItemFromInventory(player.inventory[i].inventory);
            //            toHeal = true;
            //            //HealLand();
            //        }
            //    }
            //}
            //if (player.IsInteractButtonDown)
            //{
            //    Debug.Log("Mouse Button Down!");
            //}
        }
    }

    private void PotionTimer()
    {
        if(storage != null)
        {
            timer += Time.deltaTime;
            if(timer >= storage.usageTime)
            {
                storage = null;
                reclaim = true;
                timer = 0;
            }

        }
    }


}