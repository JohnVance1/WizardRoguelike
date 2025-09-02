using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;
public enum PlayerState
{
    Idle,
    Move,
    UsePotion,

}


public class PlayerCombatGrid : GridContent
{
    public Player_Interact interact;

    public Tilemap map;

    public CombatGridSpawner Spawner;
    private int x, y;

    public int moveDistance;
    private List<GridSpace> moveableSpaces;

    public Inventory inventory;

    public int moveNums = 1;

    public PlayerState state;

    public bool[,] currentAttackPotionArray;

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
        //interact = GetComponent<Player_Interact>();

        state = PlayerState.Idle;

        moveDistance = 2;

    }

    

    private void Update()
    {
        
    }

    public void SetPos(int xPos, int yPos)
    {
        transform.position = map.GetCellCenterWorld(new Vector3Int(xPos, yPos, 0));

    }

    public void ShowMoveableSpaces(int xPos, int yPos)
    {
        moveableSpaces = Spawner.SetMoveableSpaces(moveDistance, xPos, yPos);

        Spawner.HighlightMoveableSpaces(moveableSpaces);
    }
    public void ShowAttackableSpaces()
    {
        List<GridSpace> temp = Spawner.SetAttackableSpaces(currentAttackPotionArray);
        Spawner.HighlightAttackingSpaces(temp);
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
