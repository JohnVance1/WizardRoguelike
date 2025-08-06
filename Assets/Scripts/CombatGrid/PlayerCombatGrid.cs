using UnityEngine;

public class PlayerCombatGrid : MonoBehaviour
{
    public CombatGridSpawner Spawner;

    private int x, y;

    void Start()
    {
        x = 0; 
        y = 0;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (x + 1 >= Spawner.width)
            {
                x = Spawner.width - 1;
            }
            else
            {
                x++;
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (y - 1 < 0)
            {
                y = 0;
            }
            else
            {
                y--;
            }

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (x - 1 < 0)
            {
                x = 0;
            }
            else
            {
                x--;
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (y + 1 >= Spawner.height)
            {
                y = Spawner.height - 1;
            }
            else
            {
                y++;
            }
                

        }

        transform.position = Spawner.grid[x, y].transform.position;
    }
}
