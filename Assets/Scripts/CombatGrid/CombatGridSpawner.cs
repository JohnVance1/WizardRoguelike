using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject currentGridSpace;

    private int potionMaxWidth;
    private int potionMaxHeight;


    private void Start()
    {
        //potionMaxWidth = 3;
        //potionMaxHeight = 3;

        //grid = new GameObject[width, height];
        //spriteHeight = gridSpacePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        //spriteWidth = gridSpacePrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        //for (int i = 0; i < width; i++)
        //{
        //    for (int j = 0; j < height; j++)
        //    {
        //        grid[i, j] = Instantiate(gridSpacePrefab, transform.position + new Vector3((spriteWidth / 2 * j) - (spriteWidth / 2 * i), (spriteHeight / 2 * i) + (spriteHeight / 2 * j)), Quaternion.identity, transform);
        //        GridSpace space = grid[i, j].GetComponent<GridSpace>();
        //        space.xPos = i;
        //        space.yPos = j;
        //        if (i == 2 && j == 1)
        //        {
        //            space.UpdateContents(GridContents.Herb);
        //            space.herb = tempHerb;
                    
        //        }
        //        space.updateCurrentSpace += SetCurrentGridNode;
        //        space.onGridHighlightReset += ResetGridSpaces;

        //    }
        //}

    }

    public void SetCurrentGridNode(int x, int y)
    {
        currentGridSpace = grid[x, y];
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
        foreach (GameObject GO in grid)
        {
            GO.GetComponent<GridSpace>().ResetHighlight();
        }
    }

    public void HighlightMoveableSpaces(List<GameObject> spaces)
    {
        foreach (GameObject GO in spaces)
        {
            GO.GetComponent<GridSpace>().HighlightSpace();
        }
    }

    public void HighlightAttackingSpaces(List<GameObject> spaces)
    {
        foreach (GameObject GO in spaces)
        {
            GO.GetComponent<GridSpace>().WeaponHighlightSpace();
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
        if (moveDist == 0)
        {
            return;
        }

        if ((xPos + 1) < width)
        {
            if (!visited.Contains(grid[xPos + 1, yPos]))
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
    public List<GameObject> SetAttackableSpaces(bool[,] array)
    {
        List<GameObject> visited = new List<GameObject>();

        for (int i = 0; i <= potionMaxWidth - 1; i++)
        {
            for(int j = 0; j <= potionMaxHeight - 1; j++)
            {
                if (array[i, j] == true)
                {
                    int tempX = ((potionMaxWidth - 1) / 2) - i + currentGridSpace.GetComponent<GridSpace>().xPos;
                    int tempY = ((potionMaxHeight - 1) / 2) - j + currentGridSpace.GetComponent<GridSpace>().yPos;
                    if(tempX < width && tempY < height && tempX >= 0 && tempY >= 0)
                    {
                        visited.Add(grid[tempX, tempY]);

                    }

                }
            }
        }

        return visited;
    }


}
