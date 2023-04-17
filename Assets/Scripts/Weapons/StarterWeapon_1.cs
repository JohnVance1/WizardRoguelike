using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeapon_1 : BaseWeapon
{
    private bool IsDamaged;

    private void Start()
    {
        IsDamaged = false;
        StartCoroutine(DestroyWeapon(gameObject));
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsDamaged)
        {
            collision.GetComponent<Character>().TakeDamage(damage);
            
            IsDamaged = true;
        }

    }

    IEnumerator DestroyWeapon(GameObject wep)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(wep);
    }

}
