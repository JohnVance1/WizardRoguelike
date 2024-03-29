﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum SpaceType
{
    None,
    Black,
    White,
    Start,
    End,

}

public class Space : MonoBehaviour, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    public int x;
    public int y;
    public List<Space> Edges { get; set; }
    private List<Space> temp;

    [SerializeField]
    public SpaceType type;
    public Image Icon;

    public bool IsSatisfied { get; private set; }

    public bool IsError { get; private set; }

    public delegate void OnMouseDown(Vector3 pos, Space slot);
    public OnMouseDown onMouseDown;

    //public delegate void OnMoveController(Vector3 pos, Space slot);
    //public OnMoveController onMoveController;


    public delegate void OnMouseUp();
    public OnMouseUp onMouseUp;
    private Color c;
    private EventSystem eventSystem;
    public List<Sprite> connectingSprites;
    public List<GameObject> directionSpaces;   // North = [0], East = [1], South = [2], West = [3] 

    public event Action onEdgesChange;

    public void Awake()
    {
        Edges = new List<Space>();
        temp = new List<Space>();
        Icon = GetComponent<Image>();
        //Add(Icon);
        //AddToClassList("space");
        //focusable= true;
        //generateVisualContent += DrawLine;
        onEdgesChange += RemoveDiagonals;
        onEdgesChange += CheckNodeBehavior;
        //c = this.style.backgroundColor.value;
        eventSystem = EventSystem.current;
        IsSatisfied = false;

        //RegisterCallback<PointerDownEvent>(OnPointerDown);
        //RegisterCallback<PointerUpEvent>(OnPointerUp);
        //RegisterCallback<FocusInEvent>(OnFocusInSlot);
        //RegisterCallback<FocusOutEvent>(OnFocusOutSlot);

        //RegisterCallback<NavigationCancelEvent>(OnNavCancelEvent);
        //RegisterCallback<NavigationMoveEvent>(OnNavMoveEvent);
        //RegisterCallback<NavigationSubmitEvent>(OnNavSubmitEvent);


    }

    private void Start()
    {
        //type = SpaceType.None;

    }

    //public void OnFocusInSlot(FocusInEvent evt)
    //{
    //    //this.style.backgroundColor = Color.white;
    //}

    //public void OnFocusOutSlot(FocusOutEvent evt)
    //{
    //    //this.style.backgroundColor = c;

    //}

    /// <summary>
    /// Used for XBox controller
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSubmit(BaseEventData eventData)
    {
        UnityEngine.Debug.Log($"Submit OBJ: {eventData.selectedObject.name}");

        Vector3 center = this.transform.position;

        RemoveDiagonals();

        CheckNodeBehavior();

        onMouseDown(center, this);
    }

    /// <summary>
    /// Used for Mouse and Keyboard
    /// </summary>
    public void ButtonDown()
    {
        Vector3 center = this.transform.position;

        RemoveDiagonals();

        CheckNodeBehavior();

        onMouseDown(center, this);
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        //UnityEngine.Debug.Log($"Selected OBJ: {eventData.selectedObject.name}");
        //onMoveController(this.transform.position, this);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //UnityEngine.Debug.Log($"Selected OBJ: {eventData.selectedObject}");
    }
        


    //private void OnNavSubmitEvent(NavigationSubmitEvent evt)
    //{
    //    Debug.Log($"OnNavSubmitEvent {evt.propagationPhase}");

    //    Vector3 center = this.transform.position;

    //    RemoveDiagonals();

    //    CheckNodeBehavior();

    //    onMouseDown(center, this);


    //}

   
    

    private void OnPointerDown(/*PointerDownEvent evt*/)
    {
        //if (evt.button != 0)
        //{
        //    return;
        //}

        Vector3 center = this.transform.position;

        RemoveDiagonals();

        CheckNodeBehavior();

        onMouseDown(center, this);


    }

    //private void OnPointerUp(PointerUpEvent evt)
    //{
    //    if (evt.button != 0)
    //    {
    //        return;
    //    }

    //    RemoveDiagonals();

    //    CheckNodeBehavior();

    //    onMouseUp();

        

    //}

    private void Update()
    {
        

    }

    public void DrawLine()
    {



        //var painter2D = mgc.painter2D;


        //painter2D.strokeColor = Color.red;
        //painter2D.lineWidth = 5.0f;
        //painter2D.BeginPath();

        foreach (Space e in Edges)
        {
            //Vector2 baseVec = new Vector2(this.resolvedStyle.width / 2,
            //    this.resolvedStyle.height / 2);

            //painter2D.MoveTo(baseVec);

            Vector2 middlePos = (e.transform.position - this.transform.position) / 2;

            Instantiate(connectingSprites[0], middlePos, Quaternion.identity);

            //lineTo.Normalize();
            ////lineTo *= ClosestDirection(lineTo);
            //lineTo *= new Vector2(this.resolvedStyle.width,
            //    this.resolvedStyle.height);

            //lineTo += baseVec;

            //painter2D.LineTo(lineTo);

        }
        ////painter2D.ClosePath();
        //painter2D.Stroke();

    }


    public Vector3 ClosestDirection(Vector3 v)
    {
        Vector3[] compass = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

        float maxDot = -Mathf.Infinity;
        Vector3 ret = Vector3.zero;
	
	    foreach(Vector3 dir in compass) { 
		    float t = Vector3.Dot(v, dir);
		    if (t > maxDot) {
			    ret = dir;
			    maxDot = t;
		    }
	    }

	    return ret;
    }

    public void UpdateSpace()
    {
        RemoveDiagonals();
        CheckNodeBehavior();
        //MarkDirtyRepaint();
    }

    /// <summary>
    /// Removes any of the possible diagonals placed by the player
    /// </summary>
    public void RemoveDiagonals()
    {
        foreach (Space s in Edges)
        {
            if ((s.x + 1 == x && s.y + 1 == y) ||
                (s.x - 1 == x && s.y - 1 == y) ||
                (s.x + 1 == x && s.y - 1 == y) ||
                (s.x - 1 == x && s.y + 1 == y))
            {
                temp.Add(s);
            }
        }

        foreach (Space s in temp)
        {
            Edges.Remove(s);
        }
        temp.Clear();

    }

    /// <summary>
    /// Checks to see if the player has correctly placed their lines 
    /// Mainly checks special nodes to see if their conditions are met
    /// </summary>
    public void CheckNodeBehavior()
    {
        if (Edges.Count == 2)
        {
            if (type == SpaceType.Black)
            {
                if (!CheckEdgesTurn(Edges[0], Edges[1]))
                {
                    ErrorDisplay();
                    IsSatisfied = false;
                }
                else
                {
                    IsSatisfied = true;
                    Icon.color = Color.white;

                }
            }
            else if (type == SpaceType.White)
            {
                if (!CheckEdgesSraight(Edges[0], Edges[1]))
                {
                    ErrorDisplay();
                    IsSatisfied = false;
                }
                else
                {
                    IsSatisfied = true;
                    Icon.color = Color.white;

                }

            }
            else if (type == SpaceType.End)
            {
                ErrorDisplay();

            }
        }
        else if (Edges.Count > 2)
        {
            ErrorDisplay();
        }
        else
        {
            Icon.color = Color.white;

        }
    }

    /// <summary>
    /// Checks to see if the 2 edges attached to a node are parallell
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public bool CheckEdgesSraight(Space s1, Space s2)
    {
        if (((s1.x + 1 == s2.x - 1 || s2.x + 1 == s1.x - 1) && (s1.y == s2.y)) ||
            ((s1.y + 1 == s2.y - 1 || s2.y + 1 == s1.y - 1) && (s1.x == s2.x)))
        {
            return true;
        }
        return false;

    }

    /// <summary>
    /// Checks to see if the 2 edges attached to a node are perpendicular
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public bool CheckEdgesTurn(Space s1, Space s2)
    {
        if ((s1.x + 1 == s2.x && s1.y + 1 == s2.y) ||
            (s1.x - 1 == s2.x && s1.y - 1 == s2.y) ||
            (s1.x + 1 == s2.x && s1.y - 1 == s2.y) ||
            (s1.x - 1 == s2.x && s1.y + 1 == s2.y) ||
            (s1.x == s2.y && s1.y == s2.x))
        {
            return true;
        }
        return false;
    }

    

    /// <summary>
    /// Placeholder for showing when a node is not used correctly
    /// </summary>
    public void ErrorDisplay()
    {
        UnityEngine.Debug.Log("Error with space X: " + x + " Y: " + y);
        Icon.color = Color.red;
    }

    


    //#region UXML
    //[Preserve]
    //public new class UxmlFactory : UxmlFactory<Space, UxmlTraits> { }
    //[Preserve]
    //public new class UxmlTraits : VisualElement.UxmlTraits { }
    //#endregion

}
