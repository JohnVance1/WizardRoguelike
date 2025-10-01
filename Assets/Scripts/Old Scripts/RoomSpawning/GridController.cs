using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    public int width = 9;
    public int height = 9;
    //public Node[,] grid;

    private Grid grid;
    [SerializeField] 
    private Tilemap interactiveMap = null;
    [SerializeField] 
    private Tile hoverTile = null;
    [SerializeField] 
    private GameObject fountainPrefab;

  


    private Vector3Int previousMousePos = new Vector3Int();


    

    private void Awake()
    {


    }



    public void Start()
    {
        grid = GetComponent<Grid>();
        //grid = new Node[width, height];
        //for (int i = 0; i < width; i++)
        //{
        //    for (int j = 0; j < height; j++)
        //    {
        //        Node n = new Node();
        //        n.x = i;
        //        n.y = j;
        //        n.visited = false;
        //        grid[i, j] = n;
        //    }
        //}
    }
    

    void Update()
    {
        // Mouse over -> highlight tile
        //Vector3Int mousePos = GetMousePosition();
        //if (!mousePos.Equals(previousMousePos))
        //{
        //    interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
        //    interactiveMap.SetTile(mousePos, hoverTile);
        //    previousMousePos = mousePos;
        //}

        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    GameObject fount = Instantiate(fountainPrefab, interactiveMap.GetCellCenterWorld(mousePos), Quaternion.identity);

        //    corruptionManager.FountainSpawned(interactiveMap.GetCellCenterWorld(mousePos), fount);

        //}

    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return grid.WorldToCell(mouseWorldPos);
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < width; i++)
    //    {
    //        for (int j = 0; j < height; j++)
    //        {
    //            Gizmos.color = (i + j) % 2 == 0 ? Color.black : Color.white;
    //            Gizmos.DrawCube(new Vector3((i * 0.5f), (j * 0.5f) + 0.25f + (0.25f * i), 0), new Vector3(0.5f, 0.5f, 0));
    //        }
    //    }
    //}


}
