using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class Research_MiniGame : SerializedMonoBehaviour
{
    public const int width = 6;
    public const int height = 6;

    [SerializeField]
    public GameObject[,] spaces = new GameObject[height, width];

    public GameObject startSpace;

    private int UILayer;
    private bool IsMouseDown;

    private General_Grid grid;

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        grid = new General_Grid();
        IsMouseDown = false;

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                // Adds all of the grid spaces to the grid
                grid.grid[i, j] = spaces[i, j].GetComponent<Space>();
            }
        }

        // Sets up the info about each space's relation to the others
        grid.PopulateGrid();       


    }

    private void Update()
    {
        print(IsPointerOverUIElement() ? "Over UI" : "Not over UI");
        if (Input.GetMouseButtonDown(0))
        {
            IsMouseDown = true;

            GameObject square = GetMousedOverSquare(GetEventSystemRaycastResults());
            if (square != null)
            {
                startSpace = square;

            }
        }

        if (IsMouseDown && Input.GetMouseButton(0))
        {
            GameObject square = GetMousedOverSquare(GetEventSystemRaycastResults());

            if (square != startSpace && square != null)
            {
                //Debug.DrawLine(startSpace.transform.position, square.transform.position);
                if(grid.DoesEdgeExist(startSpace.GetComponent<Space>(), square.GetComponent<Space>()))
                {
                    grid.RemoveAnEdge(startSpace.GetComponent<Space>(), square.GetComponent<Space>());

                }
                else
                {
                    grid.AddAnEdge(startSpace.GetComponent<Space>(), square.GetComponent<Space>());

                }

                startSpace = square;
            }
            
        }

        if(IsMouseDown && Input.GetMouseButtonUp(0))
        {
            IsMouseDown = false;
            startSpace = null;
        }
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (grid.AdjacencyList != null)
        {
            foreach (Space e in grid.AdjacencyList)
            {
                if (e.Edges.Count != 0)
                {
                    foreach(Space e2 in e.Edges)
                    {
                        Gizmos.DrawLine(e.transform.position, e2.transform.position);

                    }
                }
            }
        }
    }

    public void ResetGame()
    {
        grid = new General_Grid();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // Adds all of the grid spaces to the grid
                grid.grid[i, j] = spaces[i, j].GetComponent<Space>();
            }
        }

        // Sets up the info about each space's relation to the others
        grid.PopulateGrid();
    }

    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }    


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private GameObject GetMousedOverSquare(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            if (eventSystemRaysastResults[index].gameObject.GetComponent<Space>() != null)
            {
                return eventSystemRaysastResults[index].gameObject;
            }
            
        }
        return null;
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

}
