using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Users;


public enum InputType
{
    None,
    KBM,
    XBox,
    PS
}

public class Player_Interact : MonoBehaviour
{

    public GameObject InventoryCanvas;
    private bool IsInventoryOpen;

    public GameObject HerbJournalCanvas;
    private bool IsHerbJournalOpen;

    public GameObject PotionJournalCanvas;
    private bool IsPotionJournalOpen;

    public GameObject QuestLogUI;
    private bool IsQuestLogOpen;

    public GameObject DefaultUI;

    private bool IsCauldronOpen;

    public PlayerControls input;
    private InputAction move;
    private InputAction interact;
    private InventoryUIController inventoryController;
    public bool IsInteractButtonDown { get; private set; }
    public Player player;
    public bool IsCrouched;

    public InputType inputType;


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
        IsCrouched = false;
        PlayerInput inputContoller = FindObjectOfType<PlayerInput>();
        updateButtonImage(inputContoller.currentControlScheme);
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

        input.Player.OpenPotionJournal.performed += PotionJournalInput;
        input.Player.OpenPotionJournal.Enable();

        input.Player.OpenQuestLog.performed += QuestLogInput;
        input.Player.OpenQuestLog.Enable();

        input.Player.Sneak.performed += Crouch;
        input.Player.Sneak.Enable();

        InputUser.onChange += onInputDeviceChange;

    }

    private void OnDisable()
    {
        move.Disable();
        input.Player.Interact.canceled -= RemovePress;
        input.Player.Interact.performed -= Interact;

        input.Player.OpenInventory.performed -= InventoryInput;
        input.Player.OpenJournal.performed -= JournalInput;
        input.Player.OpenPotionJournal.performed -= PotionJournalInput;
        input.Player.OpenQuestLog.performed -= QuestLogInput;
        input.Player.Sneak.performed -= Crouch;

        input.Player.Sneak.Disable();
        input.Player.Interact.Disable();
        input.Player.OpenJournal.Disable();
        input.Player.OpenPotionJournal.Disable();
        input.Player.OpenQuestLog.Disable();

        InputUser.onChange -= onInputDeviceChange;


    }



    void Start()
    {
        IsInventoryOpen = false;
        IsHerbJournalOpen = false;
        IsPotionJournalOpen = false;

        IsQuestLogOpen = false;
        input.Player.Enable();
        input.UI.Disable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 5f;
        inventoryController = InventoryCanvas.GetComponent<InventoryUIController>();

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

        float newSpeed = speed;

        if (IsCrouched)
        {
            newSpeed *= 0.7f;
        }

        rb.velocity = movement.normalized * newSpeed;

        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }  

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            updateButtonImage(user.controlScheme.Value.name);
        }
    }

    void updateButtonImage(string schemeName)
    {
        if (schemeName.Equals("Gamepad"))
        {
            inputType = InputType.XBox;
            print("Xbox gamepad");
        }
        else
        {
            inputType = InputType.KBM;
            print("Mouse and Keyboard");
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        IsCrouched = !IsCrouched;
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
        if (IsHerbJournalOpen)
        {
            CloseHerbJournal();
        }
        else
        {
            OpenHerbJournal();
        }
    }

    public void PotionJournalInput(InputAction.CallbackContext context)
    {
        if (IsPotionJournalOpen)
        {
            ClosePotionJournal();
        }
        else
        {
            OpenPotionJournal();
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
        if(IsHerbJournalOpen)
        {
            CloseHerbJournal();
        }
        if (IsPotionJournalOpen)
        {
            ClosePotionJournal();
        }
        if (IsQuestLogOpen)
        {
            CloseQuestLog();
        }
        
    }

    public void EnablePlayer()
    {
        DefaultUI.SetActive(true);
        input.UI.Disable();
        input.Player.Enable();

    }

    public void EnableUI()
    {
        DefaultUI.SetActive(false);
        input.Player.Disable();
        input.UI.Enable();
        IsInteractButtonDown = false;

    }

    public void OpenInventory()
    {
        //InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.SetActive(true);
        inventoryController.state = OpenState.General;
        IsInventoryOpen = true;
        inventoryController.OnOpen();
        EnableUI();
    }
    public void CloseInventory()
    {
        //InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        InventoryCanvas.SetActive(false);
        IsInventoryOpen = false;
        EnablePlayer();
    }

    public void OpenHerbJournal()
    {
        HerbJournalCanvas.gameObject.SetActive(true);
        //HerbJournalCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        HerbJournalCanvas.GetComponent<HerbJournal_UI>().OpenUI();
        IsHerbJournalOpen = true;
        EnableUI();
    }
    public void CloseHerbJournal()
    {
        HerbJournalCanvas.gameObject.SetActive(false);
        //HerbJournalCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;

        IsHerbJournalOpen = false;
        EnablePlayer();
    }

    public void OpenPotionJournal()
    {
        PotionJournalCanvas.gameObject.SetActive(true);
        //HerbJournalCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        PotionJournalCanvas.GetComponent<PotionJournal_UI>().OpenUI();
        IsPotionJournalOpen = true;
        EnableUI();
    }
    public void ClosePotionJournal()
    {
        PotionJournalCanvas.gameObject.SetActive(false);
        //HerbJournalCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;

        IsPotionJournalOpen = false;
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
        InventoryCanvas.SetActive(true);
        inventoryController.state = OpenState.Farm;
        inventoryController.interactable = plot;
        inventoryController.OnOpen();
        IsInventoryOpen = true;
        EnableUI();
    }

    public void OpenResearchInventory(ResearchStation research)
    {
        InventoryCanvas.SetActive(true);
        inventoryController.state = OpenState.Research;
        inventoryController.interactable = research;
        inventoryController.OnOpen();
        IsInventoryOpen = true;
        EnableUI();
    }

    public void OpenCauldronInventory(Cauldron cauldron)
    {
        //InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.SetActive(true);
        inventoryController.state = OpenState.Cauldron;
        inventoryController.interactable = cauldron;
        IsInventoryOpen = true;
        //InventoryCanvas.transform.GetComponent<InventoryUIController>().OnOpen();
        EnableUI();
    }

    public void OpenFountainInventory(Fountain fountain)
    {
        InventoryCanvas.SetActive(true);
        inventoryController.state = OpenState.Fountain;
        inventoryController.interactable = fountain;
        IsInventoryOpen = true;
        inventoryController.OnOpen();
        EnableUI();
    }
}
