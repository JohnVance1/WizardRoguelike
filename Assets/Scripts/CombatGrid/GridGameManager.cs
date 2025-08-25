using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGameManager : MonoBehaviour
{
    public CombatGridSpawner spawner;
    private PlayerCombatGrid player;

    public int levelNum;
    public bool PlayerTurn;
    public int turnCount;

    public static GridGameManager Instance { get; private set; }

    PlayerControls controls;
    public Tilemap map;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        PlayerTurn = true;
        player = PlayerCombatGrid.Instance;
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

    private void Start()
    {
        controls.GridPlayer.MouseClick.performed += _ => MouseClick();

    }
    public void MouseClick()
    {
        Vector2 mousePos = controls.GridPlayer.MouseMove.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = map.WorldToCell(mousePos);
        if (map.HasTile(gridPos))
        {
            GridSpace space = spawner.MapToGrid(gridPos.x, gridPos.y);
            if (space.contents == PlayerCombatGrid.Instance && PlayerCombatGrid.Instance.state == PlayerState.Idle)
            {
                PlayerCombatGrid.Instance.ShowMoveableSpaces(space.position.x, space.position.y);
                PlayerCombatGrid.Instance.state = PlayerState.Move;
            }
            else if(PlayerCombatGrid.Instance.state == PlayerState.Move && space.IsHighlighted)
            {
                PlayerCombatGrid.Instance.transform.position = map.GetCellCenterWorld(gridPos);
                spawner.SetCurrentGridNode(gridPos.x, gridPos.y, PlayerCombatGrid.Instance);
                spawner.ResetGridSpaces();
                PlayerCombatGrid.Instance.state = PlayerState.Idle;
            }
            
        }
        else
        {
            spawner.ResetGridSpaces();
        }

    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        //    if (hit.collider == null)
        //    {
        //        spawner.ResetGridSpaces();
        //    }
            
        //}
    }

    public void EndPlayerTurn()
    {
        StartCoroutine(StartPlayerTurn());

    }

    public IEnumerator StartPlayerTurn()
    {
        PlayerTurn = false;
        yield return new WaitForSeconds(2f);
        PlayerTurn = true;
        PlayerCombatGrid.Instance.moveNums = 1;

    }


}
