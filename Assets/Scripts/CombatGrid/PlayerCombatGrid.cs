using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatGrid : MonoBehaviour
{
    public CombatGridSpawner Spawner;
    private int x, y;

    public int moveDistance;
    private List<GameObject> moveableSpaces;

    public Inventory inventory;

    public int moveNums = 1;

    public static PlayerCombatGrid Instance { get; private set; }
    public int X { get { return x; } private set { x = value; } }
    public int Y { get { return y; } private set { y = value;  } }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        moveableSpaces = new List<GameObject>();
        x = 3; 
        y = 3;
        transform.position = Spawner.SetEntityPos(GridContents.Player, x, y);
        moveDistance = 2;

    }

    void Update()
    {
        

    }

    public void SetPos(int xPos, int yPos)
    {
        if (moveableSpaces.Contains(Spawner.grid[xPos, yPos]))
        {
            transform.position = Spawner.SetEntityPos(GridContents.Player, xPos, yPos);
            Spawner.ResetGridSpaceContents(GridContents.None, x, y);
            x = xPos;
            y = yPos;
            Spawner.ResetGridSpaces();
            moveableSpaces = new List<GameObject>();
            moveNums--;
        }

    }

    public void ShowMoveableSpaces()
    {
        moveableSpaces = Spawner.SetMoveableSpaces(moveDistance, x, y);

        Spawner.HighlightMoveableSpaces(moveableSpaces);
    }

    public void AddItemToInventory(Item_Base item)
    {
        inventory.Add(item);
    }

    public void RemoveItemFromInventory(Item_Base item)
    {
        inventory.Remove(item);
    }

}
