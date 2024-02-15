using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using static Player;

public class InventoryUIController : SerializedMonoBehaviour
{
    [SerializeField]
    //public List<InventorySlot_UI> InventoryItems = new List<InventorySlot_UI>();
    public List<List<InventorySlot_UI>> InventoryItems;
    public List<InventorySlot_UI> ItemSlots;
    //public List<InventorySlot_UI> AllSlots;

    private int xCount;
    private int yCount;

    //private VisualElement m_Root;
    //private VisualElement m_SlotContainer;
    public Inventory inv;
    //private VisualElement m_GhostIcon;
    private bool m_IsDragging;
    private Slot_UI m_OriginalSlot;
    private Sprite m_OriginalSprite;

    public OpenState state;
    public Interactable_Base interactable;
    public Slot_UI selected;
    public Slot_UI current;

    public Player player;

    private PlayerControls input;
    private InputAction nav;

    private int currentX;
    private int currentY;


    private void Awake()
    {
        //ItemSlots = AllSlots;
        currentX = 0;
        currentY = 0;
                
        //Store the root from the UI Document component
        //m_Root = GetComponent<UIDocument>().rootVisualElement;
        //m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");
        InventoryItems = new List<List<InventorySlot_UI>>();
        //Search the root for the SlotContainer Visual Element
        //m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");
        //Create InventorySlots and add them as children to the SlotContainer
        //List<VisualElement> items = m_SlotContainer.hierarchy.Children().ToList();
        int index = 1;
        for(int i = 0; i < 5; i++)
        {
            //List<VisualElement> buttons = InventoryItems;
            int yCount = 0;
            List<InventorySlot_UI> temp = new List<InventorySlot_UI>();
            InventoryItems.Add(temp);
            foreach (InventorySlot_UI item in ItemSlots)
            {
                //item.RegisterCallback<FocusInEvent>(OnFocusInSlot);
                InventoryItems[i].Add(item);
                //m_SlotContainer.Add(item);
                //item.tabIndex = index++;
                //item.RegisterCallback<NavigationMoveEvent, Vector2>(Navigate, new Vector2(i, yCount));
                yCount++;
            }
        }

        
    }
    private void OnNavSubmitEvent(NavigationSubmitEvent evt)
    {
        Debug.Log($"OnNavSubmitEvent {evt.propagationPhase}");
    }

    private void OnNavMoveEvent(NavigationMoveEvent evt)
    {
        Debug.Log($"OnNavMoveEvent {evt.propagationPhase} - move {evt.move} - direction {evt.direction}");
        
        /* If there is an item that is selected */
        //if (selected != null && !m_IsDragging) 
        //{
        //    /* Then set the item to the GhostIcon and move it with the movement of the controller */
        //    StartDrag(selected.layout.position, selected);
        //}

        
        //Set the new position
        


    }

    public void OnFocusInSlot(FocusInEvent evt)
    {
        current = (Slot_UI)evt.currentTarget;

        if (m_IsDragging)
        {
            //m_GhostIcon.style.top = current.worldBound.position.y;// - m_GhostIcon.layout.height / 2;
            //m_GhostIcon.style.left = current.worldBound.position.x;// - m_GhostIcon.layout.width / 2;
        }
    }

    //public void DirectionToPosition(NavigationMoveEvent e)
    //{
    //    switch (e.direction)
    //    {
    //        case NavigationMoveEvent.Direction.Up: return ; break;
    //        case NavigationMoveEvent.Direction.Down: D.Focus(); break;
    //        case NavigationMoveEvent.Direction.Left: L.Focus(); break;
    //        case NavigationMoveEvent.Direction.Right: R.Focus(); break;
    //    }
    //    e.PreventDefault();
    //}

    private void OnNavCancelEvent(NavigationCancelEvent evt)
    {
        Debug.Log($"OnNavCancelEvent {evt.propagationPhase}");
        current = null;
        m_IsDragging = false;
        m_OriginalSlot = null;
        //m_GhostIcon.style.visibility = Visibility.Hidden;

    }

    public static Focusable getFocusedElement()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            return null;
        }

        GameObject selectedGameObject = eventSystem.currentSelectedGameObject;
        if (selectedGameObject == null)
        {
            return null;
        }

        PanelEventHandler panelEventHandler = selectedGameObject.GetComponent<PanelEventHandler>();
        if (panelEventHandler != null)
        {
            return panelEventHandler.panel.focusController.focusedElement;
        }

        return null;
    }

    //private void OnPointerMove(PointerMoveEvent evt)
    //{
    //    //Only take action if the player is dragging an item around the screen
    //    if (!m_IsDragging)
    //    {
    //        return;
    //    }
    //    //Set the new position
    //    m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
    //    m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;
    //}
    //private void OnPointerUp(PointerUpEvent evt)
    //{
    //    if (!m_IsDragging)
    //    {
    //        return;
    //    }
    //    //Check to see if they are dropping the ghost icon over any inventory slots.
    //    IEnumerable<Slot_UI> slots = AllSlots.Where(x =>
    //           x.worldBound.Overlaps(m_GhostIcon.worldBound));
    //    //Found at least one
    //    if (slots.Count() != 0)
    //    {
    //        Slot_UI closestSlot = slots.OrderBy(x => Vector2.Distance
    //           (x.worldBound.position, m_GhostIcon.worldBound.position)).First();

    //        //Set the new inventory slot with the data
    //        closestSlot.Set(m_OriginalSlot.storedItem);
    //        //closestSlot.HoldItem(GameController.GetItemByGuid(m_OriginalSlot.ItemGuid));

    //        //Clear the original slot
    //        m_OriginalSlot.Reset();
    //        //m_OriginalSlot.DropItem();

    //    }
    //    //Didn't find any (dragged off the window)
    //    else
    //    {
    //        //m_OriginalSlot.Icon.image =
    //        //      GameController.GetItemByGuid(m_OriginalSlot.ItemGuid).Icon.texture;
    //        m_OriginalSlot.Icon.sprite = m_OriginalSprite;
    //    }
    //    //Clear dragging related visuals and data
    //    m_IsDragging = false;
    //    m_OriginalSlot = null;
    //    m_GhostIcon.style.visibility = Visibility.Hidden;
    //}

    //public void StartDrag(Vector2 position, Slot_UI originalSlot)
    //{
    //    //Set tracking variables
    //    m_IsDragging = true;
    //    m_OriginalSlot = originalSlot;
    //    m_OriginalSprite = originalSlot.Icon.sprite;
    //    //Set the new position
    //    m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
    //    m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;
    //    //Set the image
    //    //m_GhostIcon.style.backgroundImage = GameController.GetItemByGuid(originalSlot.ItemGuid).Icon.texture;
    //    m_GhostIcon.style.backgroundImage = originalSlot.Icon.sprite.texture;
    //    originalSlot.Icon.sprite = null;
    //    //Flip the visibility on
    //    m_GhostIcon.style.visibility = Visibility.Visible;
    //}

    private void OnUpdateInventory()
    {
        xCount = 0;
        yCount = 0;

        DrawInventory();
    }

    public void DrawInventory()
    {
        //InventorySlot[] slotList = transform.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot_UI slot in ItemSlots)
        {
            slot.Clear();
        }

        //foreach (InventoryItem item in inv.inventory)
        //{
        //    AddInventorySlot(item);
        //}
        for(int i = 0; i < inv.inventory.Count; i++)
        {
            ItemSlots[i].GetComponent<InventorySlot_UI>().Set(inv.inventory[i]);

        }


    }

    public void AddInventorySlot(InventoryItem item)
    {
        //inventorySlots[yCount, xCount].GetComponent<InventorySlot>().Set(item);
        //ItemSlots.GetComponent<InventorySlot>().Set(item);

        //xCount++;
        //if (xCount >= height)
        //{
        //    xCount = 0;
        //    yCount++;
        //    if (yCount >= width)
        //    {
        //        yCount = 0;
        //    }
        //}

    }

    private void OnEnable()
    {
        input = player.GetComponent<Player_Interact>().input;

        inv.onInventoryAdd += OnInventoryAdd;
        inv.onInventoryRemove += OnInventoryRemove;

        List<InventorySlot_UI> items = ItemSlots;
        foreach (InventorySlot_UI item in items)
        {
            item.onMouseDown += ButtonCallback;
        }

        input.UI.Cancel.performed += Cancel;
        input.UI.Navigate.Enable();
        input.UI.Cancel.Enable();
        EventSystem.current.SetSelectedGameObject(ItemSlots[0].gameObject);
        OnUpdateInventory();

    }

    private void OnDisable()
    {
        inv.onInventoryAdd -= OnInventoryAdd;
        inv.onInventoryRemove -= OnInventoryRemove;
        List<InventorySlot_UI> items = ItemSlots;
        foreach (InventorySlot_UI item in items)
        {
            item.onMouseDown -= ButtonCallback;

        }
        input.UI.Cancel.performed -= Cancel;
        input.UI.Cancel.Disable();
        input.UI.Navigate.Disable();
    }

    private void Update()
    {
        //Debug.Log("X: " + currentX);
        //Debug.Log("Y: " + currentY);

    }

    public void OnOpen()
    {
        //ItemList[0].Focus();

    }

    public void Navigate(InputAction.CallbackContext context)
    {
        Debug.Log("Hello!!");

        Vector2 nav = context.ReadValue<Vector2>();


        if(nav.x == 1)
        {
            currentX = (currentX + 1 >= InventoryItems[currentY].Count) ? 0 : (currentX + 1);
            
        }
        else if (nav.x == -1)
        {
            currentX = (currentX - 1 < 0) ? (InventoryItems[currentY].Count - 1) : (currentX - 1);
                        
        }
        else if (nav.y == -1)
        {
            currentY = (currentY + 1 >= InventoryItems.Count) ? 0 : (currentY + 1);
            
        }
        else if (nav.y == 1)
        {
            currentY = (currentY - 1 < 0) ? (InventoryItems.Count - 1) : (currentY - 1);
            
        }

        //InventoryItems[currentY][currentX].Focus();


    }

    public void Cancel(InputAction.CallbackContext context)
    {
        if(state == OpenState.Cauldron)
        {
            GameObject cauldron = GameObject.FindGameObjectWithTag("Cauldron");
            cauldron.GetComponent<Cauldron>().CloseCauldronUI();
        }
        if (state == OpenState.Research)
        {
            GameObject research = GameObject.FindGameObjectWithTag("ResearchStation");
            research.GetComponent<ResearchStation>().CloseResearchGame();
        }
        player.GetComponent<Player_Interact>().CloseOpenUI();
    }

    //private void Navi(Vector2 vec)
    //{
    //    //Vector2 navigate = nav.ReadValue<Vector2>();

    //    int x = (int)vec.x;
    //    int y = (int)vec.y;

    //    if (evt.direction == NavigationMoveEvent.Direction.Right)
    //    {
    //        if (x + 1 >= InventoryItems.Count)
    //        {
    //            InventoryItems[0][y].Focus();

    //        }
    //        else
    //        {
    //            InventoryItems[x + 1][y].Focus();

    //        }
    //    }
    //    else if (evt.direction == NavigationMoveEvent.Direction.Down)
    //    {
    //        if (y - 1 <= 0)
    //        {
    //            InventoryItems[x][InventoryItems[x].Count - 1].Focus();

    //        }
    //        else
    //        {
    //            InventoryItems[x][y - 1].Focus();

    //        }

    //    }
    //    else if (evt.direction == NavigationMoveEvent.Direction.Left)
    //    {
    //        if (x - 1 <= 0)
    //        {
    //            InventoryItems[InventoryItems.Count - 1][y].Focus();

    //        }
    //        else
    //        {
    //            InventoryItems[x - 1][y].Focus();

    //        }

    //    }
    //    else if (evt.direction == NavigationMoveEvent.Direction.Up)
    //    {
    //        if (y + 1 >= InventoryItems[x].Count)
    //        {
    //            InventoryItems[x][0].Focus();

    //        }
    //        else
    //        {
    //            InventoryItems[x][y + 1].Focus();

    //        }
    //    }
    //}

        
  
    public void ButtonCallback(/*Vector2 position,*/ Slot_UI slot)
    {
        selected = slot;
        if (selected != null && selected.storedItem != null)
        {
            switch (state)
            {
                case OpenState.Farm:
                    if (selected.storedItem.item is Herb)
                    {
                        ((FarmPlot)interactable).SetHerb((Herb)selected.storedItem.item);
                    }
                    break;
                case OpenState.Research:
                    if (selected.storedItem.item is Herb)
                    {
                        if (!((Herb)selected.storedItem.item).IsResearched)
                        {
                            ((ResearchStation)interactable).OpenResearchGame((Herb)selected.storedItem.item);
                        }
                        else
                        {
                            Debug.Log("This Herb has been researched already!");
                        }
                    }
                    break;
                case OpenState.Cauldron:
                    if (selected.storedItem.item is Herb)
                    {
                        //((Cauldron)interactable).SelectCauldron((Herb)selected.storedItem.item);

                        if (((Herb)selected.storedItem.item).IsResearched)
                        {
                            ((Cauldron)interactable).SelectCauldron((Herb)selected.storedItem.item);
                            //List<CauldronSlot> slots = ((Cauldron)interactable).GetCauldronSlots();
                            //foreach(CauldronSlot calSlot in slots)
                            //{
                            //    AllSlots.Add(calSlot);
                            //    calSlot.onMouseDown += ButtonCallback;
                            //}
                            //StartDrag(position, slot);

                        }
                        else
                        {
                            Debug.Log("This Herb needs to be researched!!");
                        }
                    }
                    break;
                case OpenState.Fountain:
                    if (selected.storedItem.item is PotionInfo_SO)
                    {
                        ((Fountain)interactable).SelectFountain((PotionInfo_SO)selected.storedItem.item);
                                                
                    }
                    break;
                case OpenState.General:
                    //StartDrag(position, slot);
                    break;
                case OpenState.None:
                    Debug.Log("Not open in a valid state!");
                    break;
            }
            OnUpdateInventory();

        }

    }

    private void OnInventoryAdd(InventoryItem item)
    {

        OnUpdateInventory();
        //var emptySlot = ItemSlots.FirstOrDefault(x => x.storedItem == item);

        //if (emptySlot != null)
        //{
        //    emptySlot.Set(item);

        //}
        //else
        //{
        //    emptySlot = ItemSlots.FirstOrDefault(x => x.storedItem == null);
        //    if (emptySlot != null)
        //    {
        //        emptySlot.Set(item);
        //    }
        //}




    }

    private void OnInventoryRemove(InventoryItem item)
    {
        OnUpdateInventory();

        //var emptySlot = ItemSlots.FirstOrDefault(x => x.storedItem == item);

        //if(item.count == 1)
        //{
        //    emptySlot.countObj.SetActive(false);
        //}
        //else if(item.count <= 0)
        //{
        //    if (emptySlot != null)
        //    {
        //        emptySlot.Clear();
        //    }
        //}
        //else
        //{
        //    emptySlot.countObj.SetActive(true);
        //    emptySlot.countLabel.text = item.count.ToString();
        //}

        


    }

    
}
