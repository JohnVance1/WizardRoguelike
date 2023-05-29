using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct Node
{
    public int x;
    public int y;
    public GameObject space;

}

public struct Edge
{
    public Node first;
    public Node second;
    public bool IsActive;

}

public class General_Grid
{
    public const int width = 6;
    public const int height = 6;

    public Space[,] grid = new Space[width, height];

    public List<Space> AdjacencyList { get; set; }
    public General_Grid()
    {
        AdjacencyList = new List<Space>();        
    }

    /// <summary>
    /// Sets up the grid verticies and the adjacency matrix
    /// </summary>
    public void PopulateGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {                
                grid[i, j].x = i;
                grid[i, j].y = j;
                AddVertex(grid[i, j]);
            }
        }
    }

    /// <summary>
    /// Adds a new vertex to the graph
    /// </summary>
    /// <param name="newVertex">Name of the new vertex</param>
    /// <returns>Returns the success of the operation</returns>
    public bool AddVertex(Space newVertex)
    {
        // We will keep the implementation simple and focus on the concepts
        // Ignore duplicate vertices.
        if (AdjacencyList.Find(v => v == newVertex) != null) return true;

        // Add vertex to the graph
        AdjacencyList.Add(newVertex);
        return true;
    }

    /// <summary>
    /// Adds a new edge between two given vertices in the graph
    /// </summary>
    /// <param name="v1">Name of the first vertex</param>
    /// <param name="v2">Name of the second vertex</param>
    /// <returns>Returns the success of the operation</returns>
    public bool AddAnEdge(Space v1, Space v2)
    {      
        // Add vertex v2 to the edges of vertex v1
        AdjacencyList.Find(v => v == v1).Edges.Add(v2);

        // Add vertex v1 to the edges of vertex v2
        AdjacencyList.Find(v => v == v2).Edges.Add(v1);

        return true;
    }

    /// <summary>
    /// Removes an edge between two given vertices in the graph
    /// </summary>
    /// <param name="v1">Name of the first vertex</param>
    /// <param name="v2">Name of the second vertex</param>
    /// <returns>Returns the success of the operation</returns>
    public bool RemoveAnEdge(Space v1, Space v2)
    {
        // We assume all edges are valid and already exist.

        // Remove vertex v2 to the edges of vertex v1
        AdjacencyList.Find(v => v == v1).Edges.Remove(v2);

        // Remove vertex v1 to the edges of vertex v2
        AdjacencyList.Find(v => v == v2).Edges.Remove(v1);

        return true;
    }

    /// <summary>
    /// Returns whether or not there is an edge between the two Spaces
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public bool DoesEdgeExist(Space v1, Space v2)
    {
        if(AdjacencyList.Find(v => v == v1).Edges.Contains(v2) ||
        AdjacencyList.Find(v => v == v2).Edges.Contains(v1))
        {
            return true;
        }
        return false;

    }


}

