using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width = 9;
    public int height = 9;
    public Node[,] grid;

    public void Start()
    {
        grid = new Node[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Node n = new Node();
                n.x = i;
                n.y = j;
                n.visited = false;
                grid[i, j] = n;
            }
        }
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
