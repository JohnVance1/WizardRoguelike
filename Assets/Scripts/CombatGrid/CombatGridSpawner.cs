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

    public Herb tempHerb;

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
                grid[i, j].GetComponent<GridSpace>().xPos = i;
                grid[i, j].GetComponent<GridSpace>().yPos = j;
                if(i == 2 && j == 1)
                {
                    grid[i, j].GetComponent<GridSpace>().UpdateContents(GridContents.Herb);
                    grid[i, j].GetComponent<GridSpace>().herb = tempHerb;
                }

            }
        }

    }

    public Vector3 SetEntityPos(GridContents contents, int x, int y)
    {
        grid[x, y].GetComponent<GridSpace>().UpdateContents(contents);

        return grid[x, y].transform.position;
    }

    public void ResetGridSpaceContents(GridContents contents, int x, int y)
    {
        grid[x, y].GetComponent<GridSpace>().UpdateContents(contents);
    }

    public void ResetGridSpaces()
    {
        foreach(GameObject GO in grid)
        {
            GO.GetComponent<GridSpace>().ResetHighlight();
        }
    }

    public void HighlightMoveableSpaces(List<GameObject> spaces)
    {
        foreach(GameObject GO in spaces)
        {
            GO.GetComponent<GridSpace>().HighlightSpace();
        }
    }

    public List<GameObject> SetMoveableSpaces(int moveDist, int xPos, int yPos)
    {
        List<GameObject> visited = new List<GameObject>();
        visited.Add(grid[xPos, yPos]);

        SetMoveableSpacesRecursive(moveDist, xPos, yPos, visited);

        return visited;

    }

    public void SetMoveableSpacesRecursive(int moveDist, int xPos, int yPos, List<GameObject> visited)
    {
        if(moveDist == 0)
        {
            return;
        }

        if ((xPos + 1) < width)
        {
            if(!visited.Contains(grid[xPos + 1, yPos]))
            {
                visited.Add(grid[xPos + 1, yPos]);
                SetMoveableSpacesRecursive(moveDist - 1, xPos + 1, yPos, visited);
            }
        }

        if ((yPos + 1) < height)
        {
            if (!visited.Contains(grid[xPos, yPos + 1]))
            {
                visited.Add(grid[xPos, yPos + 1]);
                SetMoveableSpacesRecursive(moveDist - 1, xPos, yPos + 1, visited);

            }
        }

        if ((xPos - 1) >= 0)
        {
            if (!visited.Contains(grid[xPos - 1, yPos]))
            {
                visited.Add(grid[xPos - 1, yPos]);
                SetMoveableSpacesRecursive(moveDist - 1, xPos - 1, yPos, visited);

            }
        }

        if ((yPos - 1) >= 0)
        {
            if (!visited.Contains(grid[xPos, yPos - 1]))
            {
                visited.Add(grid[xPos, yPos - 1]);
                SetMoveableSpacesRecursive(moveDist - 1, xPos, yPos - 1, visited);

            }
        }


    }


}
