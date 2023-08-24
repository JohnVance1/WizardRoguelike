using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Sprites
{
    FrontLeft = 0,
    FrontRight = 1,
    BackLeft = 2,
    BackRight = 3,
}



public class Player : Character
{
    [SerializeField]
    private GameObject weapon1;
    [SerializeField]
    private Transform weapon1Spawn;


    public Player_Interact interact;
    public Player_Movement move;

    
    //public List<Item_Base> inventory;

    public Inventory inventory;
    public Journal journal;



    public int gateNum { get; set; }

    public static Player Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    private new void Start()
    {
        DontDestroyOnLoad(gameObject);
        base.Start();
        interact = GetComponent<Player_Interact>();
        move = GetComponent<Player_Movement>();
        //Name = gameObject.name;


    }


    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Attack();
        }

        

        ChangeAnimator();

    }

    public void ChangeAnimator()
    {

    }

    

    
    //public override void Attack()
    //{
    //    base.Attack();
    //    Vector3 wepPos = new Vector3(transform.position.x + (transform.localScale.x * prevHorizontal), transform.position.y + (transform.localScale.y * prevVertical), 0);
    //    rb.velocity = Vector3.zero;
    //    GameObject wep = Instantiate(weapon1, wepPos, Quaternion.identity, transform);        

    //}
    
    public void AddItemToInventory(Item_Base item)
    {
        inventory.Add(item);
        if (item is Herb)
        {
            if (!journal.Contains((Herb)item))
            {
                journal.AddHerb((Herb)item);
            }
        }
    }

    public void RemoveItemFromInventory(Item_Base item)
    {
        inventory.Remove(item);
    }


}
