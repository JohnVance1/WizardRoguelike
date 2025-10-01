using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<BaseEnemy> enemysInLevel;
    public bool PathFound;
    public CombatGridSpawner spawner;

    private void Awake()
    {
        //enemysInLevel = new List<BaseEnemy>();
    }

    private void Start()
    {
        spawner.SetCurrentGridNode(2, 1, enemysInLevel[0]);
        CheckCompletePath(enemysInLevel[0].currentSpace, PlayerCombatGrid.Instance.currentSpace);
    }

    public void CheckCompletePath(GridSpace start, GridSpace end)
    {
        Dictionary<GridSpace, bool> visited = new Dictionary<GridSpace, bool>();
        Dictionary<GridSpace, GridSpace> path = new Dictionary<GridSpace, GridSpace>();

        Queue<GridSpace> worklist = new Queue<GridSpace>();

        visited.Add(start, false);

        worklist.Enqueue(start);

        while (worklist.Count != 0)
        {
            GridSpace node = worklist.Dequeue();

            foreach (GridSpace neighbor in node.neighbors)
            {
                if (!visited.ContainsKey(neighbor))
                {
                    visited.Add(neighbor, false);
                    path.Add(neighbor, node);
                    worklist.Enqueue(neighbor);
                }
            }
        }

        if (path.ContainsKey(end))
        {
            GridSpace startEnd = end;
            while (end != start)
            {
                //Debug.Log(startEnd + ": " + end);
                Debug.Log("Path Found!!!");

                end = path[end];
            }
            PathFound = true;
        }
    }


}
