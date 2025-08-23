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
    PlayerControls controls;
    public Tilemap map;
    private Vector3 destination;
    private int movementSpeed;

    public CombatGridSpawner Spawner;
    private int x, y;

    public int moveDistance;
    private List<GameObject> moveableSpaces;

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

        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.GridPlayer.Enable();

    }

    private void OnDisable()
    {
        controls.GridPlayer.Disable();
        controls.Disable();
    }

    void Start()
    {
        state = PlayerState.Idle;
        //moveableSpaces = new List<GameObject>();
        x = 3; 
        y = 3;
        //transform.position = Spawner.SetEntityPos(GridContents.Player, x, y);
        movementSpeed = 2;

        destination = transform.position;
        controls.GridPlayer.MouseClick.performed += _ => MouseClick();

        moveDistance = 2;

    }

    public void MouseClick()
    {
        Vector2 mousePos = controls.GridPlayer.MouseMove.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = map.WorldToCell(mousePos);
        if (map.HasTile(gridPos))
        {
            transform.position = map.GetCellCenterWorld(gridPos);
            Spawner.SetCurrentGridNode(gridPos.x, gridPos.y, this);
            destination = mousePos;
        }

    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, destination) > 0.1f)
        {
            //transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        }
    }

    public void SetPos(int xPos, int yPos)
    {
        transform.position = map.GetCellCenterWorld(new Vector3Int(xPos, yPos, 0));

    }

    public void ShowMoveableSpaces()
    {
        moveableSpaces = Spawner.SetMoveableSpaces(moveDistance, x, y);

        Spawner.HighlightMoveableSpaces(moveableSpaces);
    }
    public void ShowAttackableSpaces()
    {
        List<GameObject> temp = Spawner.SetAttackableSpaces(currentAttackPotionArray);
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
