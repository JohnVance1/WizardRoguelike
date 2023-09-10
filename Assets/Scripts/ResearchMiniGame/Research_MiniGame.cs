using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;

public class Research_MiniGame : SerializedMonoBehaviour
{
    public const int width = 6;
    public const int height = 6;

    [SerializeField]
    public GameObject[,] spaces = new GameObject[height, width];

    public GameObject tempSpace;

    public Space startSpace;
    public List<Space> endSpace;

    private int UILayer;
    private bool IsMouseDown;
    public bool PathFound;

    private General_Grid grid;

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        grid = new General_Grid();
        IsMouseDown = false;
        PathFound = false;

        for (int i = 0; i < width; i++)
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
                tempSpace = square;

            }
        }

        if (IsMouseDown && Input.GetMouseButton(0))
        {
            GameObject square = GetMousedOverSquare(GetEventSystemRaycastResults());

            if (square != tempSpace && square != null)
            {
                //Debug.DrawLine(startSpace.transform.position, square.transform.position);
                if(grid.DoesEdgeExist(tempSpace.GetComponent<Space>(), square.GetComponent<Space>()))
                {
                    grid.RemoveAnEdge(tempSpace.GetComponent<Space>(), square.GetComponent<Space>());

                }
                else
                {
                    grid.AddAnEdge(tempSpace.GetComponent<Space>(), square.GetComponent<Space>());

                }

                tempSpace = square;
            }
            
        }

        if(IsMouseDown && Input.GetMouseButtonUp(0))
        {
            IsMouseDown = false;
            tempSpace = null;
        }

        foreach (Space end in endSpace)
        {
            CheckCompletePath(startSpace, end);
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

    private void OnDisable()
    {
        ResetGame();
    }


    public void CheckCompletePath(Space start, Space end)
    {
        Dictionary<Space, bool> visited = new Dictionary<Space, bool>();
        Dictionary<Space, Space> path = new Dictionary<Space, Space>();

        Queue<Space> worklist = new Queue<Space>();

        visited.Add(start, false);

        worklist.Enqueue(start);

        while (worklist.Count != 0)
        {
            Space node = worklist.Dequeue();

            foreach (Space neighbor in node.Edges)
            {
                if (!visited.ContainsKey(neighbor))
                {
                    visited.Add(neighbor, false);
                    path.Add(neighbor, node);
                    worklist.Enqueue(neighbor);
                }
            }
        }

        if (path.ContainsKey(end))
        {
            Space startEnd = end;
            while (end != start)
            {
                Debug.Log(startEnd + ": " + end);
                end = path[end];
            }
            PathFound = true;
        }
    }

    public void ResetGame()
    {
        grid.RemoveAllEdges();
        PathFound = false;

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
