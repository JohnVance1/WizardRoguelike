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
    private int roomNum;

    private Queue<Node> openQueue;

    private void Start()
    {
        grid = new Grid();
        openQueue = new Queue<Node>();
        roomNum = 4;
        SpawnRooms();
    }

    public void SpawnRooms()
    {
        Node currentRoom;

        startRoom = Instantiate(startingRooms[Random.Range(0, 3)], Vector3.zero, Quaternion.identity);
        grid.grid[4, 4].room = startRoom;
        currentRoom = grid.grid[4, 4];
        currentRoom.dir = currentRoom.room.GetComponent<Room>().openDirections;
        currentRoom.dir -= OpenDir.D;
        currentRoom.visited = true;
        openQueue.Enqueue(currentRoom);

        //  while queue > 0
        //  pop queue
        //  check which directions are open
        //  get first direction open(op) and check its neighbors to see if there are any rooms next to it. if there are then get a room that has the required openings
        //  check if the neighbors of (op) are past the limit of the grid. if one is make sure that they aren't included in the possible openings for (op)
        //  spawn a random room with the required openings and without the limiting ones



        while (openQueue.Count > 0)
        {
            //while (openQueue.Peek().visited)
            //{

            //}
            currentRoom = openQueue.Dequeue();



            if (currentRoom.dir.HasFlag(OpenDir.L))
            {
                GameObject room = GetSpawnedRoom(rightRooms, grid.grid[currentRoom.x - 1, currentRoom.y], OpenDir.L);

                if (roomNum <= 0)
                {
                    for (int i = 0; i < rightRooms.Length; i++)
                    {
                        if (rightRooms[i].GetComponent<Room>().openDirections == (OpenDir.R))
                        {
                            room = rightRooms[i];
                        }
                    }
                }
                else
                {
                    roomNum--;
                }

                grid.grid[currentRoom.x - 1, currentRoom.y].room = Instantiate(room,
                    currentRoom.room.transform.position - Vector3.right * 3,
                    Quaternion.identity);

                currentRoom.dir -= OpenDir.L;
                //currentRoom = grid.grid[currentRoom.x - 1, currentRoom.y];

                grid.grid[currentRoom.x - 1, currentRoom.y].dir = grid.grid[currentRoom.x - 1, currentRoom.y].room.GetComponent<Room>().openDirections;
                grid.grid[currentRoom.x - 1, currentRoom.y].dir -= OpenDir.R;

                

                if (grid.grid[currentRoom.x - 1, currentRoom.y].dir != OpenDir.None)
                {
                    grid.grid[currentRoom.x - 1, currentRoom.y].visited = true;
                    openQueue.Enqueue(grid.grid[currentRoom.x - 1, currentRoom.y]);
                    
                }
            }

            if (currentRoom.dir.HasFlag(OpenDir.R))
            {
                GameObject room = GetSpawnedRoom(leftRooms, grid.grid[currentRoom.x + 1, currentRoom.y], OpenDir.R);
                
                if (roomNum <= 0)
                {
                    for (int i = 0; i < leftRooms.Length; i++)
                    {
                        if (leftRooms[i].GetComponent<Room>().openDirections == (OpenDir.L))
                        {
                            room = leftRooms[i];
                        }
                    }
                }
                else
                {
                    roomNum--;
                }
                grid.grid[currentRoom.x + 1, currentRoom.y].room = Instantiate(room,
                    currentRoom.room.transform.position + Vector3.right * 3,
                    Quaternion.identity);
                currentRoom.dir -= OpenDir.R;
                
                grid.grid[currentRoom.x + 1, currentRoom.y].dir = grid.grid[currentRoom.x + 1, currentRoom.y].room.GetComponent<Room>().openDirections;
                grid.grid[currentRoom.x + 1, currentRoom.y].dir -= OpenDir.L;
                if (grid.grid[currentRoom.x + 1, currentRoom.y].dir != OpenDir.None)
                {
                    grid.grid[currentRoom.x + 1, currentRoom.y].visited = true;

                    openQueue.Enqueue(grid.grid[currentRoom.x + 1, currentRoom.y]);
                    
                }
            }

            if(currentRoom.dir.HasFlag(OpenDir.U))
            {
                GameObject room = GetSpawnedRoom(downRooms, grid.grid[currentRoom.x, currentRoom.y + 1], OpenDir.U);

                if (roomNum <= 0)
                {
                    for (int i = 0; i < downRooms.Length; i++)
                    {
                        if (downRooms[i].GetComponent<Room>().openDirections == (OpenDir.D))
                        {
                            room = downRooms[i];
                        }
                    }
                }
                else
                {
                    roomNum--;
                }

                grid.grid[currentRoom.x, currentRoom.y + 1].room = Instantiate(room,
                    currentRoom.room.transform.position + Vector3.up * 3,
                    Quaternion.identity);
                currentRoom.dir -= OpenDir.U;
                
                grid.grid[currentRoom.x, currentRoom.y + 1].dir = grid.grid[currentRoom.x, currentRoom.y + 1].room.GetComponent<Room>().openDirections;
                grid.grid[currentRoom.x, currentRoom.y + 1].dir -= OpenDir.D;

                if (grid.grid[currentRoom.x, currentRoom.y + 1].dir != OpenDir.None)
                {
                    grid.grid[currentRoom.x, currentRoom.y + 1].visited = true;

                    openQueue.Enqueue(grid.grid[currentRoom.x, currentRoom.y + 1]);
                    
                }

            }

            if (currentRoom.dir.HasFlag(OpenDir.D))
            {
                GameObject room = GetSpawnedRoom(upRooms, grid.grid[currentRoom.x, currentRoom.y - 1], OpenDir.D);

                if (roomNum <= 0)
                {
                    for (int i = 0; i < upRooms.Length; i++)
                    {
                        if (upRooms[i].GetComponent<Room>().openDirections == (OpenDir.U))
                        {
                            room = upRooms[i];
                        }
                    }
                }
                else
                {
                    roomNum--;
                }

                grid.grid[currentRoom.x, currentRoom.y - 1].room = Instantiate(room,
                    currentRoom.room.transform.position - Vector3.up * 3,
                    Quaternion.identity);
                currentRoom.dir -= OpenDir.D;
               
                grid.grid[currentRoom.x, currentRoom.y - 1].dir = grid.grid[currentRoom.x, currentRoom.y - 1].room.GetComponent<Room>().openDirections;
                grid.grid[currentRoom.x, currentRoom.y - 1].dir -= OpenDir.U;
                if (grid.grid[currentRoom.x, currentRoom.y - 1].dir != OpenDir.None)
                {
                    grid.grid[currentRoom.x, currentRoom.y - 1].visited = true;

                    openQueue.Enqueue(grid.grid[currentRoom.x, currentRoom.y - 1]);
                    
                }
            }

            //if (currentRoom.dir != OpenDir.None)
            //{
            //    openQueue.Enqueue(currentRoom);
            //}

        }


        
        //if (currentRoom.dir == OpenDir.L)
        //{
        //    grid.grid[currentRoom.x - 1, currentRoom.y].room = Instantiate(rightRooms[Random.Range(0, 3)], 
        //        currentRoom.room.transform.position - Vector3.right * 3, 
        //        Quaternion.identity);
        //    currentRoom.dir -= OpenDir.L;
        //    if(currentRoom.dir == OpenDir.None)
        //    {
        //        openQueue.RemoveAt(0);
        //    }
        //    currentRoom = grid.grid[currentRoom.x - 1, currentRoom.y];

        //    grid.grid[currentRoom.x - 1, currentRoom.y].dir = grid.grid[currentRoom.x - 1, currentRoom.y].room.GetComponent<Room>().openDirections;
        //    grid.grid[currentRoom.x - 1, currentRoom.y].dir -= OpenDir.R;
        //    openQueue.Add(grid.grid[currentRoom.x - 1, currentRoom.y]);
        //    Node n = openQueue[openQueue.Count - 1];
        //    SpawnRoomsRec(ref n);

        //}

        //if (currentRoom.dir == OpenDir.R)
        //{
        //    grid.grid[currentRoom.x + 1, currentRoom.y].room = Instantiate(leftRooms[Random.Range(0, 3)],
        //        currentRoom.room.transform.position + Vector3.right * 3,
        //        Quaternion.identity);
        //    currentRoom.dir -= OpenDir.R;
        //    if (currentRoom.dir == OpenDir.None)
        //    {
        //        openQueue.RemoveAt(0);
        //    }
        //    //currentRoom = grid.grid[currentRoom.x + 1, currentRoom.y];

        //    grid.grid[currentRoom.x + 1, currentRoom.y].dir = grid.grid[currentRoom.x + 1, currentRoom.y].room.GetComponent<Room>().openDirections;
        //    grid.grid[currentRoom.x + 1, currentRoom.y].dir -= OpenDir.L;
        //    openQueue.Add(grid.grid[currentRoom.x + 1, currentRoom.y]);
        //    Node n = openQueue[openQueue.Count - 1];
        //    SpawnRoomsRec(ref n);

        //}

        //if (currentRoom.dir == OpenDir.U)
        //{
        //    grid.grid[currentRoom.x, currentRoom.y + 1].room = Instantiate(downRooms[Random.Range(0, 3)],
        //        currentRoom.room.transform.position + Vector3.up * 3,
        //        Quaternion.identity);
        //    currentRoom.dir -= OpenDir.U;
        //    if (currentRoom.dir == OpenDir.None)
        //    {
        //        openQueue.RemoveAt(0);
        //    }
        //    //currentRoom = grid.grid[currentRoom.x, currentRoom.y + 1];

        //    grid.grid[currentRoom.x, currentRoom.y + 1].dir = grid.grid[currentRoom.x, currentRoom.y + 1].room.GetComponent<Room>().openDirections;
        //    grid.grid[currentRoom.x, currentRoom.y + 1].dir -= OpenDir.D;
        //    openQueue.Add(grid.grid[currentRoom.x, currentRoom.y + 1]);
        //    Node n = openQueue[openQueue.Count - 1];
        //    SpawnRoomsRec(ref n);

        //}



    }


    public GameObject GetSpawnedRoom(GameObject[] roomArray, Node current, OpenDir parentDir)
    {
        GameObject room = roomArray[Random.Range(0, 7)];

        OpenDir required = grid.CheckNeighbors(current, parentDir);

        if (!room.GetComponent<Room>().openDirections.HasFlag(required))
        {
            for(int i = 0;i < roomArray.Length; i++)
            {
                if(roomArray[i].GetComponent<Room>().openDirections.HasFlag(required))
                {
                    room = roomArray[i];

                }

            }
        }



        //OpenDir restricted = grid.CheckBounds(current, parentDir);
        //if (required != OpenDir.None || restricted != OpenDir.None)
        //{
        //    if (required != OpenDir.None && restricted != OpenDir.None)
        //    {
        //        while (room.GetComponent<Room>().openDirections.HasFlag(restricted) && !room.GetComponent<Room>().openDirections.HasFlag(required))
        //        {
        //            room = roomArray[Random.Range(0, 7)];
        //        }
        //    }

        //    else if (required != OpenDir.None)
        //    {
        //        while (!room.GetComponent<Room>().openDirections.HasFlag(required))
        //        {
        //            room = roomArray[Random.Range(0, 7)];
        //        }
        //    }

        //    else if (restricted != OpenDir.None)
        //    {
        //        while (room.GetComponent<Room>().openDirections.HasFlag(restricted))
        //        {
        //            room = roomArray[Random.Range(0, 7)];
        //        }
        //    }

        //}

        return room;
    }

    



}
