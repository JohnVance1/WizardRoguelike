using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player_Interact : MonoBehaviour
{

    public GameObject InventoryCanvas;
    private bool IsInventoryOpen;

    public GameObject JournalCanvas;
    private bool IsJournalOpen;

    public GameObject QuestLogUI;
    private bool IsQuestLogOpen;

    private bool IsCauldronOpen;

    public PlayerControls input;
    private InputAction move;
    private InputAction interact;

    public bool IsInteractButtonDown { get; private set; }
    public Player player;

    #region Movement Variables
    [SerializeField]

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    private float prevHorizontal;
    private float prevVertical;
    public Sprite[] directionSprites;
    private Animator animator;
    public float speed;

    #endregion

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        move = input.Player.Move;
        move.Enable();

        input.Player.Interact.performed += Interact;
        input.Player.Interact.canceled += RemovePress;
        input.Player.Interact.Enable();

        input.Player.OpenInventory.performed += InventoryInput;
        input.Player.OpenInventory.Enable();

        input.Player.OpenJournal.performed += JournalInput;
        input.Player.OpenJournal.Enable();

        input.Player.OpenQuestLog.performed += QuestLogInput;
        input.Player.OpenQuestLog.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        input.Player.Interact.canceled -= RemovePress;
        input.Player.Interact.performed -= Interact;

        input.Player.OpenInventory.performed -= InventoryInput;
        input.Player.OpenJournal.performed -= JournalInput;
        input.Player.OpenQuestLog.performed -= QuestLogInput;

        input.Player.Interact.Disable();
        input.Player.OpenJournal.Disable();
        input.Player.OpenQuestLog.Disable();

    }



    void Start()
    {
        IsInventoryOpen = false;
        IsJournalOpen = false;
        IsQuestLogOpen = false;
        input.Player.Enable();
        input.UI.Disable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 5f;

    }



    // Update is called once per frame
    void Update()
    {
        //IsInteractButtonDown = Input.GetMouseButtonDown(0) ? true : false;

       
        //Debug.Log(IsInteractButtonDown);

    }


    private void FixedUpdate()
    {
        Vector2 movement = move.ReadValue<Vector2>();
        //horizontal = Input.GetAxisRaw("Horizontal");

        //vertical = Input.GetAxisRaw("Vertical");
        if (movement.x != 0 && movement.y != 0)
        {
            prevHorizontal = movement.x;
            prevVertical = movement.y;

        }
        else if (movement.x != 0)
        {
            prevHorizontal = movement.y;
            prevVertical = 0;

        }
        else if (movement.y != 0)
        {
            prevVertical = movement.y;
            prevHorizontal = 0;

        }

        if (movement.x < 0 && movement.y < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (movement.x > 0 && movement.y < 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (movement.x < 0 && movement.y > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackLeft];
        }

        if (movement.x > 0 && movement.y > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }

        if (movement.x > 0 && movement.y == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontRight];
        }

        if (movement.x < 0 && movement.y == 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.FrontLeft];
        }

        if (movement.x == 0 && movement.y > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = directionSprites[(int)Sprites.BackRight];
        }
        rb.velocity = movement.normalized * speed;

        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }


    public void Interact(InputAction.CallbackContext context)
    {
        IsInteractButtonDown = true;
        GameEventsManager.instance.inputEvents.SubmitPressed();

       
    }

    public void RemovePress(InputAction.CallbackContext context)
    {
        IsInteractButtonDown = false;
    }

    public void JournalInput(InputAction.CallbackContext context)
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

    public void InventoryInput(InputAction.CallbackContext context)
    {
        if (IsInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void QuestLogInput(InputAction.CallbackContext context)
    {
        if (IsQuestLogOpen)
        {
            CloseQuestLog();
        }
        else
        {
            OpenQuestLog();
        }
    }

    public void CloseOpenUI()
    {
        if(IsInventoryOpen)
        {
            CloseInventory();
        }
        if(IsJournalOpen)
        {
            CloseJournal();
        }
        if(IsQuestLogOpen)
        {
            CloseQuestLog();
        }
        
    }

    public void EnablePlayer()
    {
        input.UI.Disable();
        input.Player.Enable();
    }

    public void EnableUI()
    {
        input.Player.Disable();
        input.UI.Enable();
        IsInteractButtonDown = false;

    }

    public void OpenInventory()
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.General;
        IsInventoryOpen = true;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().OnOpen();
        EnableUI();
    }
    public void CloseInventory()
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        IsInventoryOpen = false;
        EnablePlayer();
    }

    public void OpenJournal()
    {
        //JournalCanvas.gameObject.SetActive(true);
        JournalCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        JournalCanvas.GetComponent<Journal_UI>().OpenUI();
        IsJournalOpen = true;
        EnableUI();
    }
    public void CloseJournal()
    {
        //JournalCanvas.gameObject.SetActive(false);
        JournalCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;

        IsJournalOpen = false;
        EnablePlayer();


    }

    public void OpenQuestLog()
    {
        QuestLogUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

        IsQuestLogOpen = true;
        EnableUI();
    }

    public void CloseQuestLog()
    {
        QuestLogUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;

        IsQuestLogOpen = false;
        EnablePlayer();

    }

    public void OpenFarmInventory(FarmPlot plot)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Farm;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = plot;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().OnOpen();
        IsInventoryOpen = true;
        EnableUI();
    }

    public void OpenResearchInventory(ResearchStation research)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Research;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = research;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().OnOpen();
        IsInventoryOpen = true;
        EnableUI();
    }

    public void OpenCauldronInventory(Cauldron cauldron)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Cauldron;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = cauldron;
        IsInventoryOpen = true;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().OnOpen();
        EnableUI();
    }

    public void OpenFountainInventory(Fountain fountain)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Fountain;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = fountain;
        IsInventoryOpen = true;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().OnOpen();
        EnableUI();
    }
}
