using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public enum SpaceType
{
    None,
    Black,
    White,
    Start,
    End,

}

public class Space : MonoBehaviour
{
    public int x;
    public int y;
    public List<Space> Edges { get; set; }
    private List<Space> temp;

    [SerializeField]
    public SpaceType type;

    public bool IsError { get; private set; }

    private void Start()
    {
        Edges = new List<Space>();
        temp = new List<Space>();
        //type = SpaceType.None;

        
    }

    private void Update()
    {
        RemoveDiagonals();

        CheckNodeBehavior();

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
                }
            }
            else if (type == SpaceType.White)
            {
                if (!CheckEdgesSraight(Edges[0], Edges[1]))
                {
                    ErrorDisplay();
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
        Debug.Log("Error with space X: " + x + " Y: " + y);
    }

}
