using System.Collections;
using UnityEngine;

public class GridGameManager : MonoBehaviour
{
    public CombatGridSpawner spawner;
    public PlayerCombatGrid player;

    public int levelNum;
    public bool PlayerTurn;
    public int turnCount;

    public static GridGameManager Instance { get; private set; }


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
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider == null)
            {
                spawner.ResetGridSpaces();
            }
            
        }
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
