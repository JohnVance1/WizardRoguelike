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

    /// <summary>
    /// Returns the directions that must be in the spawned room
    /// </summary>
    /// <param name="current"></param>
    /// <param name="parentRoomDir"></param>
    /// <returns></returns>
    public OpenDir CheckNeighbors(Node current, OpenDir parentRoomDir)
    {
        OpenDir dir = OpenDir.None;
        if (current.y - 1 >= 0)
        {
            if (grid[current.x, current.y - 1].room != null /*&& grid[current.x, current.y - 1].dir.HasFlag(OpenDir.U) && parentRoomDir != OpenDir.U*/)
            {
                dir = dir | OpenDir.D;
            }
        }
        if (current.y + 1 < height)
        {
            if (grid[current.x, current.y + 1].room != null /*&& grid[current.x, current.y + 1].dir.HasFlag(OpenDir.D) && parentRoomDir != OpenDir.D*/)
            {
                dir = dir | OpenDir.U;
            }
        }
        if (current.x - 1 >= 0)
        {
            if (grid[current.x - 1, current.y].room != null /*&& grid[current.x - 1, current.y].dir.HasFlag(OpenDir.R) && parentRoomDir != OpenDir.R*/)
            {
                dir = dir | OpenDir.L;
            }
        }
        if (current.x + 1 < width)
        {
            if (grid[current.x + 1, current.y].room != null /*&& grid[current.x + 1, current.y].dir.HasFlag(OpenDir.L) && parentRoomDir != OpenDir.L*/)
            {
                dir = dir | OpenDir.R;
            }
        }
        return dir;
    }

    /// <summary>
    /// Returns the directions that cannot be included in the spawned room
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    public OpenDir CheckBounds(Node current, OpenDir parentRoomDir)
    {
        OpenDir dir = OpenDir.None;
        if (current.y - 1 < 0 || (grid[current.x, current.y - 1].room != null && grid[current.x, current.y - 1].dir.HasFlag(OpenDir.U) && parentRoomDir != OpenDir.U))
        {
            dir = dir | OpenDir.D;

        }
        if (current.y + 1 >= height || (grid[current.x, current.y + 1].room != null && !grid[current.x, current.y + 1].dir.HasFlag(OpenDir.D) && parentRoomDir != OpenDir.D))
        {
            dir = dir | OpenDir.U;

        }
        if (current.x - 1 < 0 || (grid[current.x - 1, current.y].room != null && !grid[current.x - 1, current.y].dir.HasFlag(OpenDir.R) && parentRoomDir != OpenDir.R))
        {
            dir = dir | OpenDir.L;

        }
        if (current.x + 1 >= width || (grid[current.x + 1, current.y].room != null && !grid[current.x + 1, current.y].dir.HasFlag(OpenDir.L) && parentRoomDir != OpenDir.L))
        {
            dir = dir | OpenDir.R;

        }
        return dir;

    }

}
