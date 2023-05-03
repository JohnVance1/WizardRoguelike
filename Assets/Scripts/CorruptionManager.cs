using System.Collections;
using System.Collections.Generic;
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

    private Dictionary<Vector3Int, float> corruptedTiles = new Dictionary<Vector3Int, float>();

    private void Start()
    {
        //StartCoroutine(ReduceCorruptedRoutine());
        SetCorruption();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddCorruption(mousePos, testVal, testRadius);
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
                AddCorruption(place, testVal, testRadius);
            }
        }

    }



    public void AddCorruption(Vector3 worldPos, float changeby, int radius)
    {
        Vector3Int gridPos = corruptionMap.WorldToCell(worldPos);

        for(int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                if (distanceFromCenter <= radius)
                {
                    Vector3Int nextTilePosition = new Vector3Int(gridPos.x + x, gridPos.y + y, 0);
                    ChangeCorrupt(nextTilePosition, changeby - (distanceFromCenter * corruptionFallOff * changeby));

                }


            }
        }

        ChangeCorrupt(gridPos, changeby);
        VisualizeCorruption();

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
            corruptionMap.SetColor(gridPos, clear);
            corruptionMap.SetTileFlags(gridPos, TileFlags.LockColor);

        }
        else
        {
            corruptedTiles[gridPos] = Mathf.Clamp(newValue, 0f, 12f); ;
        }

    }

    private void VisualizeCorruption()
    {
        foreach (var entry in corruptedTiles)
        {            
            corruptionMap.SetTileFlags(entry.Key, TileFlags.None);
            corruptionMap.SetColor(entry.Key, corrupted);
            corruptionMap.SetTileFlags(entry.Key, TileFlags.LockColor);

        }
    }

    private IEnumerator ReduceCorruptedRoutine()
    {
        while (true)
        {
            Dictionary<Vector3Int, float> stinkingTilesCopy = new Dictionary<Vector3Int, float>(corruptedTiles);

            foreach (var entry in stinkingTilesCopy)
            {
                ChangeCorrupt(entry.Key, reduceAmount);
            }

            VisualizeCorruption();

            yield return new WaitForSeconds(reduceIntervall);

        }



    }


}
