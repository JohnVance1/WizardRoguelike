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

    public GameObject JournalCanvas;
    private bool IsJournalOpen;

    private Animator animator;
    //public List<Item_Base> inventory;

    public Inventory inventory;
    public Journal journal;

    public Sprite[] directionSprites;

    public bool IsInteractButtonDown { get; private set; }

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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Name = gameObject.name;
        IsInventoryOpen = false;
        IsJournalOpen = false;
        speed = 5f;
    }


    private void FixedUpdate()
    {
        Movement();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Attack();
        }

        IsInteractButtonDown = Input.GetMouseButtonDown(0) ? true : false;

        if(Input.GetKeyDown(KeyCode.V))
        {
            if(IsInventoryOpen)
            {                
                CloseInventory();
            }
            else
            {
                OpenInventory();                
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsJournalOpen)
            {
                CloseJournal();
            }
            else
            {
                OpenJournal();
            }
        }

        ChangeAnimator();

    }

    public void ChangeAnimator()
    {

    }

    public void OpenInventory()
    {
        InventoryCanvas.gameObject.SetActive(true);
        IsInventoryOpen = true;
    }
    public void CloseInventory()
    {
        InventoryCanvas.gameObject.SetActive(false);
        IsInventoryOpen = false;

    }

    public void OpenJournal()
    {
        JournalCanvas.gameObject.SetActive(true);
        IsJournalOpen = true;
    }
    public void CloseJournal()
    {
        JournalCanvas.gameObject.SetActive(false);
        IsJournalOpen = false;

    }

    public void OpenFarmInventory(FarmPlot plot)
    {
        InventoryCanvas.gameObject.SetActive(true);
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().state = OpenState.Farm;
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().interactable = plot;
        IsInventoryOpen = true;
    }

    public void OpenResearchInventory(ResearchStation research)
    {
        InventoryCanvas.gameObject.SetActive(true);
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().state = OpenState.Research;
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().interactable = research;
        IsInventoryOpen = true;
    }

    public void OpenCauldronInventory(Cauldron cauldron)
    {
        InventoryCanvas.gameObject.SetActive(true);
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().state = OpenState.Cauldron;
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().interactable = cauldron;
        IsInventoryOpen = true;
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

        if(horizontal < 0 && vertical < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (horizontal > 0 && vertical < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (horizontal < 0 && vertical > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackLeft];
        }

        if (horizontal > 0 && vertical > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }

        if(horizontal > 0 && vertical == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (horizontal < 0 && vertical == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (horizontal == 0 && vertical > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }
        

        rb.velocity = new Vector3(horizontal, vertical, 0).normalized * speed;

        if(rb.velocity != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

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
