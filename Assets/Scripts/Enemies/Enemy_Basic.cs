using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : BaseEnemy
{
    public GameObject weaponPrefab;
    private bool spawned;

    override public void Start()
    {
        spawned = false;
        Name = gameObject.name;
    }

    public override void Update()
    {
        MoveEnemy();
        Attack();
    }

    public void MoveEnemy()
    {
        if (InRange(transform.position, player.transform.position, 5f))
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public override void Attack()
    {
        if (InRange(transform.position, player.transform.position, 2f) && !spawned)
        {
            agent.isStopped = true;
            Vector3 wepPos = new Vector3(transform.position.x + (transform.localScale.x * agent.velocity.normalized.x), transform.position.y + (transform.localScale.y * agent.velocity.normalized.y), 0);
            GameObject wep = Instantiate(weaponPrefab, wepPos, Quaternion.identity, transform);
            spawned = true;
            StartCoroutine(DestroyWeapon(wep));
        }
        else
        {
            agent.isStopped = false;
        }
    }
    IEnumerator DestroyWeapon(GameObject wep)
    {
        yield return new WaitForSeconds(1f);
        spawned = false;
        Destroy(wep);
    }
}
