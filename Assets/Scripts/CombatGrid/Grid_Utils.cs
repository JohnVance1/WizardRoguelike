using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace GridGame
{
    public class Grid_Utils
    {
        /// <summary>
        /// Translates coordinates space on the TileMap to the in-code grid.
        /// </summary>
        /// <param name="mapPosX"></param>
        /// <param name="mapPosY"></param>
        /// <returns></returns>
        public GridSpace MapToGrid(int mapPosX, int mapPosY, int boundsXMin, int boundsYMin, GridSpace[,] grid)
        {
            int px = Mathf.Abs(boundsXMin) + mapPosX;
            int py = Mathf.Abs(boundsYMin) + mapPosY;

            return grid[px, py];
        }

        public static Vector2Int MapToGridVec(int mapPosX, int mapPosY, int boundsXMin, int boundsYMin)
        {
            int px = Mathf.Abs(boundsXMin) + mapPosX;
            int py = Mathf.Abs(boundsYMin) + mapPosY;

            return new Vector2Int(px, py);
        }

        /// <summary>
        /// Translates the in-code grid coordinates to the TileMap space
        /// </summary>
        /// <param name="gridPosX"></param>
        /// <param name="gridPosY"></param>
        /// <returns></returns>
        public Vector3Int GridToMap(int gridPosX, int gridPosY, int w, int h)
        {
            Vector3Int mapPos = new Vector3Int(
                gridPosX - (w / 2),
                gridPosY - (h / 2),
                0
            );


            return mapPos;
        }





    }
}
