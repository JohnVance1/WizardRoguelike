using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyWeapon : BaseWeapon
{
    private bool IsDamaged;

    private void Start()
    {
        IsDamaged = false;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsDamaged)
        {
            collision.GetComponent<Character>().TakeDamage(damage);

            IsDamaged = true;
        }

    }
}
