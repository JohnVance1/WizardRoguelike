using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : BaseEnemy
{
    public GameObject weaponPrefab;

    public int moveDist;

    public int attackOrder;

    public Vector2Int gridPos;


    //public Weapon activeWeapon;

    public new void Start()
    {
        base.Start();
        Name = gameObject.name;
    }

    public void Update()
    {
        //MoveEnemy();
        Attack();
    }

   

    public override void Attack()
    {
        
    }

}
