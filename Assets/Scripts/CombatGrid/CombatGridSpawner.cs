using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using GridGame;

public class CombatGridSpawner : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    private float spriteHeight;
    private float spriteWidth;

    public GameObject gridSpacePrefab;
    public GridSpace[,] grid;
    public Tilemap map;
    public Tilemap highlightMap;
    public Tilemap collectableMap;

    public Tile highlightMoveSprite;
    public Tile highlightAttackSprite;
    public Tile defaultSprite;
    public Tile herbSprite;


    public Herb tempHerb;
    public PotionInfo_SO tempPotion;
    public GridSpace currentGridSpace;

    private int potionMaxWidth;
    private int potionMaxHeight;
    private BoundsInt bounds;

    private void Start()
    {
        potionMaxWidth = 3;
        potionMaxHeight = 3;
                
        spriteHeight = gridSpacePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        spriteWidth = gridSpacePrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        map.CompressBounds();
        bounds = map.cellBounds;
        grid = new GridSpace[bounds.size.x, bounds.size.y];
        width = bounds.size.x;
        height = bounds.size.y;

        Debug.Log("Bounds:" + bounds);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int px = bounds.xMin + x;
                int py = bounds.yMin + y;

                if (map.HasTile(new Vector3Int(px, py, 0)))
                {
                    grid[x, y] = new GridSpace(new Vector2Int(px, py), new Vector2Int(x, y));
                    grid[x, y].defaultSprite = defaultSprite;

                    

                    if (x == 2 && y == 1)
                    {
                        grid[x, y].herb = tempHerb;
                        grid[x, y].contentType = GridContentType.Herb;
                        collectableMap.SetTile(new Vector3Int(px, py, 0), herbSprite);
                    }
                }
                
            }
        }

        for (int x = 0; x < width; x++)
        { 
            for(int y = 0; y < height;y++)
            {
                if(y > 0 && y < height - 1)
                {
                    grid[x, y].neighbors.Add(grid[x, y - 1]);
                    grid[x, y].neighbors.Add(grid[x, y + 1]);
                }
                else if(y == 0)
                {
                    grid[x, y].neighbors.Add(grid[x, y + 1]);
                }
                else if(y == height - 1)
                {
                    grid[x, y].neighbors.Add(grid[x, y - 1]);
                }

                if (x > 0 && x < width - 1)
                {
                    grid[x, y].neighbors.Add(grid[x + 1, y]);
                    grid[x, y].neighbors.Add(grid[x - 1, y]);

                }
                else if(x == 0)
                {
                    grid[x, y].neighbors.Add(grid[x + 1, y]);
                }
                else if(x == width - 1)
                {
                    grid[x, y].neighbors.Add(grid[x - 1, y]);
                }
                


               
            }
        }

        PlayerCombatGrid.Instance.AddItemToInventory(tempPotion);
        SetCurrentGridNode(0, 0, PlayerCombatGrid.Instance);
        PlayerCombatGrid.Instance.currentSpace = grid[0, 0];
        //grid[0, 0].contents = PlayerCombatGrid.Instance;
        PlayerCombatGrid.Instance.SetPos(0, 0);

    }

    public void SetCurrentGridNode(int x, int y, GridContent newContent)
    {
        int px = Mathf.Abs(bounds.xMin) + x;
        int py = Mathf.Abs(bounds.yMin) + y;

        if (grid[px, py].contentType != GridContentType.None)
        {
            switch(grid[px, py].contentType)
            {
                case GridContentType.Player:
                    break;
                case GridContentType.Enemy:
                    break;
                case GridContentType.Herb:
                    PlayerCombatGrid.Instance.AddItemToInventory(grid[px, py].herb);
                    grid[px, py].CollectHerb();
                    collectableMap.SetTile(new Vector3Int(x, y, 0), null);
                    break;
            }
        }


        newContent.currentSpace = grid[px, py];
        grid[px, py].contents = newContent;
        Debug.Log(grid[px, py].contents.name);
    }

    /// <summary>
    /// Translates coordinates space on the TileMap to the in-code grid.
    /// </summary>
    /// <param name="mapPosX"></param>
    /// <param name="mapPosY"></param>
    /// <returns></returns>
    public GridSpace MapToGrid(int mapPosX, int mapPosY)
    {
        int px = Mathf.Abs(bounds.xMin) + mapPosX;
        int py = Mathf.Abs(bounds.yMin) + mapPosY;

        return grid[px, py];
    }

    public Vector2Int MapToGridVec(int mapPosX, int mapPosY)
    {
        int px = Mathf.Abs(bounds.xMin) + mapPosX;
        int py = Mathf.Abs(bounds.yMin) + mapPosY;

        return new Vector2Int(px, py);
    }

    /// <summary>
    /// Translates the in-code grid coordinates to the TileMap space
    /// </summary>
    /// <param name="gridPosX"></param>
    /// <param name="gridPosY"></param>
    /// <returns></returns>
    public Vector3Int GridToMap(int gridPosX, int gridPosY)
    {
        Vector3Int mapPos = new Vector3Int(
            gridPosX - (width / 2),
            gridPosY - (height / 2),
            0
        );


        return mapPos;
    }

    //public Vector3 SetEntityPos(GridContents contents, int x, int y)
    //{
    //    //grid[x, y].GetComponent<GridSpace>().UpdateContents(contents);

    //    //return grid[x, y].transform.position;
    //}

    //public void ResetGridSpaceContents(GridContents contents, int x, int y)
    //{
    //    grid[x, y].GetComponent<GridSpace>().UpdateContents(contents);
    //}

    public void ResetHighlightGridSpaces()
    {
        foreach (GridSpace GO in grid)
        {
            highlightMap.SetTile(new Vector3Int(GO.position.x, GO.position.y, 0), null);
            GO.IsHighlighted = false;
        }
    }

    public void HighlightMoveableSpaces(List<GridSpace> spaces)
    {
        foreach (GridSpace GO in spaces)
        {
            Vector3Int tilePos = new Vector3Int(GO.position.x, GO.position.y, 0);
            GO.IsHighlighted = true;
            highlightMap.SetTile(tilePos, highlightMoveSprite);
            
        }
    }

    public void HighlightAttackingSpaces(List<GridSpace> spaces)
    {
        foreach (GridSpace GO in spaces)
        {
            Vector3Int tilePos = new Vector3Int(GO.position.x, GO.position.y, 0);
            GO.IsHighlighted = true;
            highlightMap.SetTile(tilePos, highlightAttackSprite);

        }       
        
    }

    public List<GridSpace> SetMoveableSpaces(int moveDist, int xPos, int yPos)
    {
        List<GridSpace> visited = new List<GridSpace>();
        //visited.Add(grid[xPos, yPos]);
        Vector2Int vec = Grid_Utils.MapToGridVec(xPos, yPos, bounds.xMin, bounds.yMin);
        SetMoveableSpacesRecursive(moveDist, vec.x, vec.y, visited);

        return visited;

    }

    public void SetMoveableSpacesRecursive(int moveDist, int xPos, int yPos, List<GridSpace> visited)
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

    public List<GridSpace> SetAttackableSpaces(bool[,] array)
    {
        List<GridSpace> visited = new List<GridSpace>();

        for (int i = 0; i <= potionMaxWidth - 1; i++)
        {
            for(int j = 0; j <= potionMaxHeight - 1; j++)
            {
                if (array[i, j] == true)
                {
                    int tempX = ((potionMaxWidth - 1) / 2) - i + currentGridSpace.gridPos.x;
                    int tempY = ((potionMaxHeight - 1) / 2) - j + currentGridSpace.gridPos.y;
                    if (tempX < width && tempY < height && tempX >= 0 && tempY >= 0)
                    {
                        visited.Add(grid[tempX, tempY]);

                    }
                }
            }
        }

        return visited;
    }


}
