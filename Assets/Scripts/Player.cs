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

    public GameObject InventoryCanvas;
    private bool IsInventoryOpen;

    //public List<Item_Base> inventory;

    public Inventory inventory;

    public bool IsInteractButtonDown { get; private set; }

    private new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Name = gameObject.name;
        IsInventoryOpen = false;
        speed = 5f;
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

        IsInteractButtonDown = Input.GetMouseButtonDown(0) ? true : false;

        if(Input.GetKeyDown(KeyCode.V))
        {
            if(IsInventoryOpen)
            {
                InventoryCanvas.gameObject.SetActive(false);
                IsInventoryOpen = false;
            }
            else
            {
                InventoryCanvas.gameObject.SetActive(true);
                IsInventoryOpen = true;
            }
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
    
    public void AddItemToInventory(Item_Base item)
    {
        inventory.Add(item);
    }

    public void RemoveItemFromInventory(Item_Base item)
    {
        inventory.Remove(item);
    }


}
