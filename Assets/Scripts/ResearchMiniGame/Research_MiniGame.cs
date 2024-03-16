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
using UnityEngine.Windows;

public class Research_MiniGame : MonoBehaviour
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
    public List<Space> turnSpaces;
    public List<Space> straightSpaces;


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

    public PlayerControls input;
    private InputAction move_UI;
    private InputAction submit_UI;

    public ResearchMiniGame_Data activeGame;

    public GameObject completeResearch;

    public GameObject pressSprite;
    public List<Sprite> pressButtons;

    public InputType storedType;
    public Player_Interact playerInteract;

    private void Awake()
    {
        grid = new General_Grid();
        eventSystem = EventSystem.current;
        input = Player.Instance.interact.input;

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
                //sp.onMouseUp += MouseUp;
                //sp.onMoveController += DrawLine;
                sp.connectingSprites = connectSprites;

            }
        }
        
        //m_Root = GetComponent<UIDocument>().rootVisualElement;
        //m_Exit = m_Root.Query<Button>("Exit");
        //RegisterCallback<NavigationMoveEvent>(OnNavMoveEvent);
        //RegisterCallback<NavigationCancelEvent>(OnNavCancelEvent);
    }

    private void OnEnable()
    {
        input.UI.Point.performed += OnPointerEnter;
        input.UI.Click.canceled += MouseUp;
        input.UI.Navigate.performed += OnMove;
        input.UI.ButtonUp.canceled += MouseUp;
        //input.UI.Navigate.canceled += RemovePress;
        input.UI.Navigate.Enable();
        input.UI.Point.Enable();
    }

    private void OnDisable()
    {
        //input.UI.Navigate.canceled -= RemovePress;
        input.UI.Point.performed -= OnPointerEnter;
        input.UI.Navigate.performed -= OnMove;
        input.UI.ButtonUp.canceled -= MouseUp;
        input.UI.Click.canceled -= MouseUp;

        ResetGame();

    }

    public void DisplayInteractButtons(InputType type, List<Sprite> buttons, GameObject sp)
    {
        if (type == InputType.KBM)
        {
            sp.GetComponent<Image>().sprite = buttons[0];
        }
        else if (type == InputType.XBox)
        {
            sp.GetComponent<Image>().sprite = buttons[1];

        }
        else if (type == InputType.PS)
        {
            sp.GetComponent<Image>().sprite = buttons[2];

        }

        sp.SetActive(true);
    }

    public Sprite ReturnCornerSprites(List<Sprite> tiles, int x, int y)
    {
        Sprite sprite = null;
        if (x == 0 && y == 0)
        {
            sprite = tiles[8];

        }
        else if (x == height - 1 && y == width - 1)
        {
            sprite = tiles[2];

        }
        else if (x == 0 && y == width - 1)
        {
            sprite = tiles[9];

        }
        else if (x == height - 1 && y == 0)
        {
            sprite = tiles[1];

        }
        else if (x == 0)
        {
            sprite = tiles[6];

        }
        else if (y == 0)
        {
            sprite = tiles[5];

        }
        else if (x == height - 1)
        {
            sprite = tiles[7];
        }
        else if (y == width - 1)
        {
            sprite = tiles[3];
        }
        else
        {
            sprite = tiles[0];
        }

        return sprite;
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
        playerInteract = FindObjectOfType<Player_Interact>();

        OpenUI();

        m_Exit.onClick.AddListener(() =>
        {
            ResetGame();
            researchStation.CloseResearchGame();
        });

        // Sets up the info about each space's relation to the others
        grid.PopulateGrid();

        storedType = playerInteract.inputType;
        DisplayInteractButtons(storedType, pressButtons, pressSprite);

    }

    public void OpenUI()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {               
                Space sp = grid.grid[i, j];

                sp.Icon.sprite = ReturnCornerSprites(defaultTiles, i, j);

                if (i == activeGame.start.x && j == activeGame.start.y)
                {
                    sp.Icon.sprite = ReturnCornerSprites(startTiles, i, j);
                    sp.type = SpaceType.Start;
                    startSpace = sp;
                }

                foreach (Vector2 vec in activeGame.end)
                {
                    if (i == vec.x && j == vec.y)
                    {
                        sp.Icon.sprite = ReturnCornerSprites(endTiles, i, j);
                        sp.type = SpaceType.End;
                        endSpace.Add(sp);
                    }
                }

                foreach (Vector2 vec in activeGame.turn)
                {
                    if (i == vec.x && j == vec.y)
                    {
                        sp.Icon.sprite = ReturnCornerSprites(turnTiles, i, j);
                        sp.type = SpaceType.Black;
                        turnSpaces.Add(sp);
                    }
                }

                foreach (Vector2 vec in activeGame.straight)
                {
                    if (i == vec.x && j == vec.y)
                    {
                        sp.Icon.sprite = ReturnCornerSprites(straightTiles, i, j);
                        sp.type = SpaceType.White;
                        straightSpaces.Add(sp);

                    }
                }


            }
        }

        playerInteract.EnableUI();

    }


    private void Update()
    {
        foreach (Space end in endSpace)
        {
            CheckCompletePath(startSpace, end);
        }

        if(PathFound)
        {
            DisplayComplete();
        }

        if (storedType != playerInteract.inputType)
        {
            DisplayInteractButtons(storedType, pressButtons, pressSprite);
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

    public void OnPointerEnter(InputAction.CallbackContext context)
    {
        if (!IsMouseDown)
        {
            return;
        }
        //Debug.Log($"Move Vector: {context.ReadValue<Vector2>()}");

        //Vector2 moveVector = context.ReadValue<Vector2>();
        //Space space = GetSpace(context.ReadValue<Vector2>());
        //IEnumerable<Space> space = spaces.Where(x =>
        //       x.worldBound.Contains(evt.position));


        Space space = GetMousedOverSquare(GetEventSystemRaycastResults()).GetComponent<Space>();
        if (space != null)
        {
            //Space closestSlot = space.OrderBy(x => Vector2.Distance
            //   (x.worldBound.position, evt.position)).First();
            Vector2 moveVector = space.transform.position - tempSpace.transform.position;

            Space closestSlot = space;
            if (closestSlot != tempSpace && closestSlot != null)
            {
                //Debug.DrawLine(startSpace.transform.position, square.transform.position);
                if (grid.DoesEdgeExist(tempSpace, closestSlot))
                {
                    grid.RemoveAnEdge(tempSpace, closestSlot);
                    tempSpace.UpdateSpace();
                    closestSlot.UpdateSpace();
                    EraseLine(-moveVector, closestSlot);
                    EraseLine(moveVector, tempSpace);
                }
                else
                {
                    grid.AddAnEdge(tempSpace, closestSlot);
                    tempSpace.UpdateSpace();
                    closestSlot.UpdateSpace();
                    DrawLine(-moveVector, closestSlot);
                    DrawLine(moveVector, tempSpace);


                }

                tempSpace = closestSlot;
            }
        }
    }

    // private void OnNavMoveEvent(NavigationMoveEvent evt)
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"Move Vector: {context.ReadValue<Vector2>()}");
        //Only take action if the player is dragging an item around the screen
        if (!IsMouseDown)
        {
            return;
        }
        //Debug.Log($"OnNavMoveEvent {evt.propagationPhase} - move {evt.move} - direction {evt.direction}");
        Vector2 moveVector = context.ReadValue<Vector2>();
        Space space = GetSpace(context.ReadValue<Vector2>());
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
                    EraseLine(-moveVector, closestSlot);
                    EraseLine(moveVector, tempSpace);
                }
                else
                {
                    grid.AddAnEdge(tempSpace, closestSlot);
                    tempSpace.UpdateSpace();
                    closestSlot.UpdateSpace();
                    DrawLine(-moveVector, closestSlot);
                    DrawLine(moveVector, tempSpace);


                }

                tempSpace = closestSlot;
            }
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        UnityEngine.Debug.Log($"Submit OBJ: {eventData.selectedObject.name}");

       
    }

    public void OnSelect(BaseEventData eventData)
    {
        UnityEngine.Debug.Log($"Selected OBJ: {eventData.selectedObject.name}");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        UnityEngine.Debug.Log($"Selected OBJ: {eventData.selectedObject}");
    }


    Vector3[] compass = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

    public Vector3 ClosestDirection(Vector3 v)
    {
	    float maxDot = -Mathf.Infinity;
        Vector3 ret = Vector3.zero;
	
	    foreach(Vector3 dir in compass) 
        { 
		    float t = Vector3.Dot(v, dir);
		    if (t > maxDot) {
			    ret = dir;
			    maxDot = t;
		    }
	    }

	    return ret;
    }


    public void DrawLine(Vector2 moveVec, Space space)
    {
        foreach (Space e in space.Edges)
        {
            

        }

        GameObject edge;

        moveVec = ClosestDirection(moveVec);

        if (moveVec == Vector2.up)
        {
            edge = space.directionSpaces[0];
            edge.SetActive(true);
        }
        else if (moveVec == Vector2.right)
        {
            edge = space.directionSpaces[1];
            edge.SetActive(true);

        }
        else if (moveVec == Vector2.down)
        {
            edge = space.directionSpaces[2];
            edge.SetActive(true);

        }
        else if (moveVec == Vector2.left)
        {
            edge = space.directionSpaces[3];
            edge.SetActive(true);

        }

        //RectTransform rectTransform = space.GetComponent<RectTransform>();

        //Vector2 sizeVec = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        //sizeVec /= 2;

        //sizeVec *= moveVec;

        //GameObject temp = Instantiate(space.connectingSprites[0], sizeVec + (Vector2)this.transform.position, Quaternion.identity);

        //temp.transform.SetParent(space.transform, false);

    }

    public void EraseLine(Vector2 moveVec, Space space)
    {
       
        GameObject edge;

        moveVec = ClosestDirection(moveVec);

        if (moveVec == Vector2.up)
        {
            edge = space.directionSpaces[0];
            edge.SetActive(false);
        }
        else if (moveVec == Vector2.right)
        {
            edge = space.directionSpaces[1];
            edge.SetActive(false);

        }
        else if (moveVec == Vector2.down)
        {
            edge = space.directionSpaces[2];
            edge.SetActive(false);

        }
        else if (moveVec == Vector2.left)
        {
            edge = space.directionSpaces[3];
            edge.SetActive(false);

        }

        //RectTransform rectTransform = space.GetComponent<RectTransform>();

        //Vector2 sizeVec = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        //sizeVec /= 2;

        //sizeVec *= moveVec;

        //GameObject temp = Instantiate(space.connectingSprites[0], sizeVec + (Vector2)this.transform.position, Quaternion.identity);

        //temp.transform.SetParent(space.transform, false);

    }

    public void ReleaseMouse()
    {
        if (!IsMouseDown)
        {
            return;
        }
        IsMouseDown = false;
        tempSpace = null;
    }

    public void DisplayComplete()
    {
        completeResearch.SetActive(true);
        StartCoroutine(ActivateDisplay());
    }

    public IEnumerator ActivateDisplay()
    {
        yield return new WaitForSeconds(1.5f);
        completeResearch.SetActive(false);
        ResetGame();
        researchStation.CloseResearchGame();
    }

    private void MouseUp(InputAction.CallbackContext context)
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

        if (path.ContainsKey(end) && CheckTiles())
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

    public bool CheckTiles()
    {
        foreach(Space sp in straightSpaces)
        {
            if (sp.IsSatisfied == false)
            {
                return false;
            }
        }

        foreach (Space sp in turnSpaces)
        {
            if (sp.IsSatisfied == false)
            {
                return false;
            }
        }

        return true;
    }

    public void ResetGame()
    {
        grid.RemoveAllEdges();

        foreach(Space sp in spaces)
        {
            foreach(GameObject go in sp.directionSpaces)
            {
                go.SetActive(false);
            }
            
        }

        PathFound = false;

    }

    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
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
        eventData.position = UnityEngine.Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

}
