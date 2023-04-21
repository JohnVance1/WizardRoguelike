using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] leftRooms;
    public GameObject[] upRooms;
    public GameObject[] rightRooms;
    public GameObject[] downRooms;
    public GameObject[] startingRooms;

    private Grid grid;
    private GameObject startRoom;
    private Node currentRoom;

    private List<Node> openList;

    private void Start()
    {
        grid = new Grid();
        openList = new List<Node>();
        SpawnRooms();
    }

    public void SpawnRooms()
    {
        if (grid.grid[4, 4].room == null) 
        {
            startRoom = Instantiate(startingRooms[Random.Range(0, 3)], Vector3.zero, Quaternion.identity);
            grid.grid[4, 4].room = startRoom;
            currentRoom = grid.grid[4, 4];
            currentRoom.dir = currentRoom.room.GetComponent<Room>().openDirections;
            currentRoom.dir -= OpenDir.D;
            openList.Add(currentRoom);
        }
        
        if (currentRoom.dir == OpenDir.L)
        {
            grid.grid[currentRoom.x - 1, currentRoom.y].room = Instantiate(leftRooms[Random.Range(0, 3)], 
                currentRoom.room.transform.position - Vector3.right * 2, 
                Quaternion.identity);
            currentRoom.dir -= OpenDir.L;
            if(currentRoom.dir == OpenDir.None)
            {
                openList.Remove(currentRoom);
            }
            currentRoom = grid.grid[currentRoom.x - 1, currentRoom.y];

            grid.grid[currentRoom.x - 1, currentRoom.y].dir = grid.grid[currentRoom.x - 1, currentRoom.y].room.GetComponent<Room>().openDirections;
            grid.grid[currentRoom.x - 1, currentRoom.y].dir -= OpenDir.R;
            openList.Add(grid.grid[currentRoom.x - 1, currentRoom.y]);

            SpawnRoomsRec(openList[openList.Count - 1]);

        }

        if (currentRoom.dir == OpenDir.R)
        {
            grid.grid[currentRoom.x + 1, currentRoom.y].room = Instantiate(rightRooms[Random.Range(0, 3)],
                currentRoom.room.transform.position + Vector3.right * 2,
                Quaternion.identity);
            currentRoom.dir -= OpenDir.R;
            if (currentRoom.dir == OpenDir.None)
            {
                openList.Remove(currentRoom);
            }
            //currentRoom = grid.grid[currentRoom.x + 1, currentRoom.y];

            grid.grid[currentRoom.x + 1, currentRoom.y].dir = grid.grid[currentRoom.x + 1, currentRoom.y].room.GetComponent<Room>().openDirections;
            grid.grid[currentRoom.x + 1, currentRoom.y].dir -= OpenDir.L;
            openList.Add(grid.grid[currentRoom.x + 1, currentRoom.y]);

            SpawnRoomsRec(openList[openList.Count - 1]);

        }

        if (currentRoom.dir == OpenDir.U)
        {
            grid.grid[currentRoom.x, currentRoom.y + 1].room = Instantiate(upRooms[Random.Range(0, 3)],
                currentRoom.room.transform.position + Vector3.up * 2,
                Quaternion.identity);
            currentRoom.dir -= OpenDir.U;
            if (currentRoom.dir == OpenDir.None)
            {
                openList.Remove(currentRoom);
            }
            //currentRoom = grid.grid[currentRoom.x, currentRoom.y + 1];

            grid.grid[currentRoom.x, currentRoom.y + 1].dir = grid.grid[currentRoom.x, currentRoom.y + 1].room.GetComponent<Room>().openDirections;
            grid.grid[currentRoom.x, currentRoom.y + 1].dir -= OpenDir.D;
            openList.Add(grid.grid[currentRoom.x, currentRoom.y + 1]);
            SpawnRoomsRec(openList[openList.Count - 1]);

        }



    }

    public void SpawnRoomsRec(Node current)
    {
        if (current.dir == OpenDir.L)
        {
            grid.grid[current.x - 1, current.y].room = Instantiate(leftRooms[Random.Range(0, 3)],
                current.room.transform.position - Vector3.right * 2,
                Quaternion.identity);
            current.dir -= OpenDir.L;
            if (current.dir == OpenDir.None)
            {
                openList.Remove(current);
            }
            currentRoom = grid.grid[current.x - 1, current.y];

            grid.grid[current.x - 1, current.y].dir = grid.grid[current.x - 1, current.y].room.GetComponent<Room>().openDirections;
            grid.grid[current.x - 1, current.y].dir -= OpenDir.R;
            if (grid.grid[current.x - 1, current.y].dir != OpenDir.None)
            {
                openList.Add(grid.grid[current.x - 1, current.y]);
                SpawnRoomsRec(openList[openList.Count - 1]);
            }
        }

        if (current.dir == OpenDir.R)
        {
            grid.grid[current.x + 1, current.y].room = Instantiate(rightRooms[Random.Range(0, 3)],
                current.room.transform.position + Vector3.right * 2,
                Quaternion.identity);
            current.dir -= OpenDir.R;
            if (current.dir == OpenDir.None)
            {
                openList.Remove(currentRoom);
            }
            currentRoom = grid.grid[current.x + 1, current.y];

            grid.grid[current.x + 1, current.y].dir = grid.grid[current.x + 1, current.y].room.GetComponent<Room>().openDirections;
            grid.grid[current.x + 1, current.y].dir -= OpenDir.L;
            if (grid.grid[current.x + 1, current.y].dir != OpenDir.None)
            {
                openList.Add(grid.grid[current.x + 1, current.y]);
                SpawnRoomsRec(openList[openList.Count - 1]);
            }
        }

        if (current.dir == OpenDir.U)
        {
            grid.grid[current.x, current.y + 1].room = Instantiate(upRooms[Random.Range(0, 3)],
                current.room.transform.position + Vector3.up * 2,
                Quaternion.identity);
            current.dir -= OpenDir.U;
            if (current.dir == OpenDir.None)
            {
                openList.Remove(current);
            }
            current = grid.grid[current.x, current.y + 1];

            grid.grid[current.x, current.y + 1].dir = grid.grid[current.x, current.y + 1].room.GetComponent<Room>().openDirections;
            grid.grid[current.x, current.y + 1].dir -= OpenDir.D;

            if (grid.grid[current.x, current.y + 1].dir != OpenDir.None)
            {
                openList.Add(grid.grid[current.x, current.y + 1]);
                SpawnRoomsRec(openList[openList.Count - 1]);
            }

        }

        if (current.dir == OpenDir.D)
        {
            grid.grid[current.x, current.y - 1].room = Instantiate(downRooms[Random.Range(0, 3)],
                current.room.transform.position - Vector3.up * 2,
                Quaternion.identity);
            current.dir -= OpenDir.D;
            if (current.dir == OpenDir.None)
            {
                openList.Remove(current);
            }
            current = grid.grid[current.x, current.y - 1];

            grid.grid[current.x, current.y - 1].dir = grid.grid[current.x, current.y - 1].room.GetComponent<Room>().openDirections;
            grid.grid[current.x, current.y - 1].dir -= OpenDir.U;
            if (grid.grid[current.x, current.y - 1].dir != OpenDir.None)
            {
                openList.Add(grid.grid[current.x, current.y - 1]);
                SpawnRoomsRec(openList[openList.Count - 1]);
            }
        }

    }




}
