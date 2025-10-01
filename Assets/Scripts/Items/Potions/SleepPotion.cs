using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPotion : PotionInfo_SO
{
    Player player;
    List<GameObject> enemies;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        enemies = new List<GameObject>();
    }

    protected override void UsePotion()
    {
        Collider2D[] allCol = Physics2D.OverlapCircleAll(player.transform.position, radius);

        foreach (Collider2D col in allCol)
        {
            if(col.tag.Equals("Enemy"))
            {
                enemies.Add(col.gameObject);
                col.GetComponent<BaseEnemy>().SetStatus(EnemyStatus.Asleep);
            }
        }
               


    }


}
