using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    virtual public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
