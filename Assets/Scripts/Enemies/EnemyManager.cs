using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<BaseEnemy> enemiesInLevel;
    public bool PathFound;
    public CombatGridSpawner spawner;

    private void Awake()
    {
        //enemysInLevel = new List<BaseEnemy>();
    }

    private void Start()
    {
        spawner.SetCurrentGridNode(2, 1, enemiesInLevel[0]);
        CheckCompletePath(enemiesInLevel[0].currentSpace, PlayerCombatGrid.Instance.currentSpace);
    }

    public Dictionary<GridSpace, GridSpace> CheckCompletePath(GridSpace start, GridSpace end)
    {
        Dictionary<GridSpace, bool> visited = new Dictionary<GridSpace, bool>();
        Dictionary<GridSpace, GridSpace> path = new Dictionary<GridSpace, GridSpace>();
        PathFound = false;
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
                    visited.Add(neighbor, true);
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

            return path;
        }
        return null;
    }

    //public void MoveEnemies()
    //{
    //    Dictionary<GridSpace, GridSpace> path;
    //    GridSpace end;
    //    GridSpace start;
    //    foreach(BaseEnemy enemy in enemiesInLevel)
    //    {
    //        path = CheckCompletePath(enemy.currentSpace, PlayerCombatGrid.Instance.currentSpace);
    //        end = PlayerCombatGrid.Instance.currentSpace;
    //        while (end != enemy.currentSpace)
    //        {

    //            end = path[end];
    //        }
    //        StartCoroutine(MoveEnemy(enemy, path));

    //    }

    //}

    //public IEnumerator MoveEnemy(BaseEnemy enemy, Dictionary<GridSpace, GridSpace> path)
    //{

    //    yield return new WaitForSeconds(0.5f);
    //    spawner.SetCurrentGridNode( , , enemy);

    //}


}
