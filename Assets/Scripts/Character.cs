using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public int health;
    [SerializeField]
    public int maxHealth;

    protected string Name { get; set; }

    [SerializeField]
    public float speed;
    private float defaultSpeed;

    protected void Start()
    {
        health = maxHealth;
        Name = gameObject.name;
        defaultSpeed = 5;
    }

    virtual public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(Name + " took Damage!");
        if (health <= 0)
        {
            Dead();
        }
    }

    virtual public void Heal(int num)
    {
        health += num;
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    virtual public void Dead()
    {
        Debug.Log(Name + " Died!");

    }

    public void SetSpeed(float val)
    {
        if (val <= 0)
        {
            speed = val;
        }
        else
        {
            speed = defaultSpeed;
        }

    }

    

    virtual public void Attack() { }


}
