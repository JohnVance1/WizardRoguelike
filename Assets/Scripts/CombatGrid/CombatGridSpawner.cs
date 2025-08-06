using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CombatGridSpawner : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    private float spriteHeight;
    private float spriteWidth;

    public GameObject gridSpacePrefab;
    public GameObject[,] grid;

    private void Start()
    {
        grid = new GameObject[width, height];
        spriteHeight = gridSpacePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        spriteWidth = gridSpacePrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid[i, j] = Instantiate(gridSpacePrefab, transform.position + new Vector3((spriteWidth/2 * j) - (spriteWidth / 2 * i), (spriteHeight/2 * i) + (spriteHeight / 2 * j)), Quaternion.identity, transform);

            }
        }

    }
    
    
}
