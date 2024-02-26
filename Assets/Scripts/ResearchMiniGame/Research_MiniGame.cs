using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine.UI;
using System.Linq;
using static UnityEditor.VersionControl.Asset;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class Research_MiniGame : SerializedMonoBehaviour, IMoveHandler
{
    public const int width = 6;
    public const int height = 6;

    [SerializeField]
    public List<Space> spaces;

    [SerializeField]
    private LineController lineController;

    public ResearchStation researchStation;

    public Space tempSpace;

    public Space startSpace;
    public List<Space> endSpace;

    private int UILayer;
    private bool IsMouseDown;
    public bool PathFound;

    private General_Grid grid;

    public List<Sprite> defaultTiles;
    public List<Sprite> startTiles;
    public List<Sprite> endTiles;
    public List<Sprite> straightTiles;
    public List<Sprite> turnTiles;

    public List<Sprite> connectSprites;

    //private VisualElement m_Root;
    //private VisualElement m_Row1;
    //private VisualElement m_Row2;
    //private VisualElement m_Row3;
    //private VisualElement m_Row4;
    //private VisualElement m_Row5;

    public List<GameObject> m_Rows;

    //private VisualElement temp;
    public Button m_Exit;

    [SerializeField]
    private Camera mainCamera;

    public event Action OnExit;
    public EventSystem eventSystem;

    private void Awake()
    {
        grid = new General_Grid();
        eventSystem = EventSystem.current;

        for (int i = 0; i < m_Rows.Count; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //Space sp = spaces[i + j];
                Space sp = m_Rows[i].transform.GetChild(j).GetComponent<Space>();
                grid.grid[i, j] = sp;
                sp.Icon.sprite = defaultTiles[0];
                //sp.parent = m_Rows[i];
                //m_Rows[i].(sp);
                spaces.Add(sp);
                sp.onMouseDown += ButtonCall;
                sp.onMouseUp += MouseUp;
                sp.connectingSprites = connectSprites;

            }
        }


                //m_Root = GetComponent<UIDocument>().rootVisualElement;
                //m_Exit = m_Root.Query<Button>("Exit");
                //RegisterCallback<NavigationMoveEvent>(OnNavMoveEvent);
                //RegisterCallback<NavigationCancelEvent>(OnNavCancelEvent);
    }


    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        IsMouseDown = false;
        PathFound = false;
        endSpace = new List<Space>();
        eventSystem.SetSelectedGameObject(grid.grid[0, 0].gameObject);

        //m_Row1 = m_Root.Q<VisualElement>("Row1");
        //m_Row2 = m_Root.Q<VisualElement>("Row2");
        //m_Row3 = m_Root.Q<VisualElement>("Row3");
        //m_Row4 = m_Root.Q<VisualElement>("Row4");
        //m_Row5 = m_Root.Q<VisualElement>("Row5");

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // Adds all of the grid spaces to the grid
                //grid.grid[i, j] = spaces[i, j].GetComponent<Space>();
                Space sp = grid.grid[i, j];
                

                //grid.grid[i, j] = sp;
                //sp.Icon.sprite = defaultTiles[0];
                ////sp.parent = m_Rows[i];
                ////m_Rows[i].(sp);
                //spaces.Add(sp);
                //sp.onMouseDown += ButtonCall;
                //sp.onMouseUp += MouseUp;

                if (i == 0 && j == 0)
                {
                    sp.Icon.sprite = defaultTiles[8];

                }
                else if (i == height - 1 && j == width - 1)
                {
                    sp.Icon.sprite = defaultTiles[2];

                }
                else if (i == 0 && j == width - 1)
                {
                    sp.Icon.sprite = defaultTiles[9];

                }
                else if (i == height - 1 && j == 0)
                {
                    sp.Icon.sprite = defaultTiles[1];

                }
                else if (i == 0)
                {
                    sp.Icon.sprite = defaultTiles[6];

                }
                else if (j == 0)
                {
                    sp.Icon.sprite = defaultTiles[5];

                }
                else if (i == height - 1)
                {
                    sp.Icon.sprite = defaultTiles[7];
                }
                else if (j == width - 1)
                {
                    sp.Icon.sprite = defaultTiles[3];
                }
                else
                {
                    sp.Icon.sprite = defaultTiles[0];
                }


                if (i == 3 && j == 3)
                {
                    //sp.style.backgroundColor = Color.red;
                    sp.Icon.sprite = endTiles[0];
                    sp.type = SpaceType.End;
                    endSpace.Add(sp);
                }
                if (i == 1 && j == 1)
                {
                    //sp.style.backgroundColor = Color.green;
                    sp.Icon.sprite = startTiles[0];
                    sp.type = SpaceType.Start;
                    startSpace = sp;
                }

                if (i == 2 && j == 4)
                {
                    //sp.style.backgroundColor = Color.black;
                    sp.Icon.sprite = turnTiles[0];
                    sp.type = SpaceType.Black;
                }
                if (i == 3 && j == 1)
                {
                    //sp.style.backgroundColor = Color.white;
                    sp.Icon.sprite = straightTiles[0];
                    sp.type = SpaceType.White;
                }

            }
        }

        m_Exit.onClick.AddListener(() =>
        {
            researchStation.CloseResearchGame();
        });

        // Sets up the info about each space's relation to the others
        grid.PopulateGrid();


    }

    public void OpenUI()
    {
        //grid.grid[0, 0].Focus();

    }


    private void Update()
    {
        foreach (Space end in endSpace)
        {
            CheckCompletePath(startSpace, end);
        }


    }

    

    public void ButtonCall(Vector3 center, Space space)
    {
        //IsMouseDown = IsMouseDown ? false : true;

        if (IsMouseDown)
        {
            IsMouseDown = false;
            tempSpace = null;
        }
        else
        {
            IsMouseDown = true;
            if (space != null)
            {
                tempSpace = space;

            }
        }


        

        //GameObject square = GetMousedOverSquare(GetEventSystemRaycastResults());
        

    }

    public Space GetSpace(Vector2 move)
    {
        return grid.grid[tempSpace.x - (int)move.y, tempSpace.y + (int)move.x];

    }

    //private void OnNavCancelEvent(NavigationCancelEvent evt)
    //{
    //    Debug.Log($"OnNavCancelEvent {evt.propagationPhase}");
    //    researchStation.CloseResearchGame();
    //}

    

    // private void OnNavMoveEvent(NavigationMoveEvent evt)
    public void OnMove(AxisEventData eventData)
    {
        Debug.Log($"Move Vector: {eventData.moveVector}");
        //Only take action if the player is dragging an item around the screen
        if (!IsMouseDown)
        {
            return;
        }
        //Debug.Log($"OnNavMoveEvent {evt.propagationPhase} - move {evt.move} - direction {evt.direction}");

        Space space = GetSpace(eventData.moveVector);
        //IEnumerable<Space> space = spaces.Where(x =>
        //       x.worldBound.Contains(evt.position));

        // Space square = GetMousedOverSquare(GetEventSystemRaycastResults()).GetComponent<Space>();
        if (space != null)
        {
            //Space closestSlot = space.OrderBy(x => Vector2.Distance
            //   (x.worldBound.position, evt.position)).First();
            Space closestSlot = space;
            if (closestSlot != tempSpace && closestSlot != null)
            {
                //Debug.DrawLine(startSpace.transform.position, square.transform.position);
                if (grid.DoesEdgeExist(tempSpace, closestSlot))
                {
                    grid.RemoveAnEdge(tempSpace, closestSlot);
                    tempSpace.UpdateSpace();
                    closestSlot.UpdateSpace();

                }
                else
                {
                    grid.AddAnEdge(tempSpace, closestSlot);
                    tempSpace.UpdateSpace();
                    closestSlot.UpdateSpace();



                }

                tempSpace = closestSlot;
            }
        }
    }

    

    void DrawLine()
    {
        if(grid.AdjacencyList != null)
        {
            foreach (Space e in grid.AdjacencyList)
            {
                if (e.Edges.Count != 0)
                {
                    foreach (Space e2 in e.Edges)
                    {
                        //Gizmos.DrawLine(e.transform.position, e2.transform.position);
                        //lineController.SetUpLine(e.transform.position, e2.transform.position);
                    }
                }
            }
        }




    }


    private void MouseUp()
    {
        if (!IsMouseDown)
        {
            return;
        }


        IsMouseDown = false;
        tempSpace = null;

    }

    



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //if (grid.AdjacencyList != null)
        //{
        //    foreach (Space e in grid.AdjacencyList)
        //    {
        //        if (e.Edges.Count != 0)
        //        {
        //            foreach(Space e2 in e.Edges)
        //            {
                       
                       
        //                Vector3 temp1 = Camera.main.ScreenToWorldPoint(e.worldBound.center);
        //                Vector3 temp2 = Camera.main.ScreenToWorldPoint(e2.worldBound.center);
        //                temp1.z = 0;
        //                //temp1.y = Screen.height - temp1.y;
        //                temp2.z = 0;
        //                //temp2.y = Screen.height - temp2.y;

        //                Gizmos.DrawLine(temp1,temp2);
        //                //Gizmos.DrawLine(e.worldBound.center, e2.worldBound.center);

        //            }
        //        }
        //    }
        //}
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
                //Debug.Log(startEnd + ": " + end);
                Debug.Log("Path Found!!!");

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
