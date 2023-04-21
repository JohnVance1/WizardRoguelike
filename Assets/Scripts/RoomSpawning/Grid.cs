using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int width = 9;
    public int height = 9;
    public Node[,] grid;

    public Grid()
    {
        grid = new Node[width, height];
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Node n = new Node();
                n.x = i;
                n.y = j;
                grid[i,j] = n;
            }
        }
    }

}
