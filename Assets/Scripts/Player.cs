using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;

    private float prevHorizontal;
    private float prevVertical;

    [SerializeField]
    private GameObject weapon1;
    [SerializeField]
    private Transform weapon1Spawn;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Name = gameObject.name;

    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    public void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0)
        {
            prevHorizontal = horizontal;
            prevVertical = vertical;

        }
        else if(horizontal != 0)
        {
            prevHorizontal = horizontal;
            prevVertical = 0;

        }
        else if (vertical != 0)
        {
            prevVertical = vertical;
            prevHorizontal = 0;

        }
        
        rb.velocity = new Vector3(horizontal, vertical, 0).normalized * speed;

    }

    public override void Attack()
    {
        base.Attack();
        Vector3 wepPos = new Vector3(transform.position.x + (transform.localScale.x * prevHorizontal), transform.position.y + (transform.localScale.y * prevVertical), 0);
        rb.velocity = Vector3.zero;
        GameObject wep = Instantiate(weapon1, wepPos, Quaternion.identity, transform);        

    }
    



}
