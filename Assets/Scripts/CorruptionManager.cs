using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CorruptionManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap corruptionMap;

    [SerializeField]
    private Tilemap baseMap;

    [SerializeField]
    private Color corrupted, clear;
    public int testVal;
    public int testRadius;

    [SerializeField]
    private float corruptionFallOff;

    [SerializeField]
    private float reduceAmount, reduceIntervall = 1f;

    public List<Fountain> spawnedFountains = new List<Fountain>();

    private Dictionary<Vector3Int, float> corruptedTiles = new Dictionary<Vector3Int, float>();

    private void Start()
    {
        //SetCorruption();
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
        //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    AddCorruption(mousePos, testVal, testRadius);

        //}
        //CheckFountains();
    }

    private void CheckFountains()
    {
        foreach(Fountain fount in spawnedFountains)
        {
            if(fount.storage == null && fount.reclaim)
            {
                StartCoroutine(ReduceCorruptedRoutine());
                fount.reclaim = false;
            }
            else if(fount.toHeal)
            {
                AddCorruption(fount.transform.position, 10, fount.storage.radius);
                fount.toHeal = false;
            }
        }
    }

    private void SetCorruption()
    {
        foreach (var pos in baseMap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = baseMap.CellToWorld(localPlace);
            if (baseMap.HasTile(localPlace))
            {
                AddCorruption(place, 0, 0);
            }
        }

    }

    public void FountainSpawned(Vector3 worldPos, GameObject fount)
    {
        Fountain spawned = fount.GetComponentInChildren<Fountain>();
        spawnedFountains.Add(spawned);
        AddCorruption(worldPos, testVal, 0);
    }


    public void AddCorruption(Vector3 worldPos, float changeby, int radius)
    {
        Vector3Int gridPos = corruptionMap.WorldToCell(worldPos);

        for(int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                //float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                float distanceFromCenter = Mathf.Abs(x) >= Mathf.Abs(y) ? Mathf.Abs(x) : Mathf.Abs(y);
                //distanceFromCenter *= 2;
                Vector3Int nextTilePosition = new Vector3Int(gridPos.x + x, gridPos.y + y, 0);
                ChangeCorrupt(nextTilePosition, changeby - (distanceFromCenter * corruptionFallOff * changeby));
                //if (distanceFromCenter <= radius) 
                //{
                    
                //}

                

            }
        }

        ChangeCorrupt(gridPos, changeby);
        VisualizeHealed();

    }



    private void ChangeCorrupt(Vector3Int gridPos, float changeby)
    {    
        if (!corruptedTiles.ContainsKey(gridPos))
            corruptedTiles.Add(gridPos, 0f);

        float newValue = corruptedTiles[gridPos] + changeby;

        if (newValue <= 0f)
        {
            corruptedTiles.Remove(gridPos);

            corruptionMap.SetTileFlags(gridPos, TileFlags.None);
            corruptionMap.SetColor(gridPos, corrupted);
            corruptionMap.SetTileFlags(gridPos, TileFlags.LockColor);

        }
        else
        {
            corruptedTiles[gridPos] = Mathf.Clamp(newValue, 0f, 12f); ;
        }

    }

    private void VisualizeHealed()
    {
        foreach (var entry in corruptedTiles)
        {            
            corruptionMap.SetTileFlags(entry.Key, TileFlags.None);
            corruptionMap.SetColor(entry.Key, clear);
            corruptionMap.SetTileFlags(entry.Key, TileFlags.LockColor);

        }
    }

    public IEnumerator ReduceCorruptedRoutine()
    {
        Dictionary<Vector3Int, float> corruptedTilesCopy = new Dictionary<Vector3Int, float>(corruptedTiles);
        while (corruptedTilesCopy.Count > 0)
        {
            corruptedTilesCopy = new Dictionary<Vector3Int, float>(corruptedTiles);

            foreach (var entry in corruptedTilesCopy)
            {
                ChangeCorrupt(entry.Key, reduceAmount);
            }

            VisualizeHealed();

            yield return new WaitForSeconds(reduceIntervall);

        }



    }


}
