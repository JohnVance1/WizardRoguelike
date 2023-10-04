using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;


public class InventoryUIController : SerializedMonoBehaviour
{
    [SerializeField]
    public List<InventorySlot_UI> InventoryItems = new List<InventorySlot_UI>();
    private VisualElement m_Root;
    private VisualElement m_SlotContainer;
    public Inventory inv;
    private VisualElement m_GhostIcon;
    private bool m_IsDragging;
    private InventorySlot_UI m_OriginalSlot;
    private Sprite m_OriginalSprite;

    public OpenState state;
    public Interactable_Base interactable;
    public InventorySlot_UI selected;


    private void Awake()
    {
        //Store the root from the UI Document component
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");

        //Search the root for the SlotContainer Visual Element
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");
        //Create InventorySlots and add them as children to the SlotContainer
        List<VisualElement> items = m_SlotContainer.hierarchy.Children().ToList();

        foreach (InventorySlot_UI item in items)
        {            
            InventoryItems.Add(item);
            m_SlotContainer.Add(item);
            item.onMouseDown += ButtonCallback;

        }


        inv.onInventoryAdd += OnInventoryAdd;
        inv.onInventoryRemove += OnInventoryRemove;

        m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        //Only take action if the player is dragging an item around the screen
        if (!m_IsDragging)
        {
            return;
        }
        //Set the new position
        m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;
    }
    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!m_IsDragging)
        {
            return;
        }
        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<InventorySlot_UI> slots = InventoryItems.Where(x =>
               x.worldBound.Overlaps(m_GhostIcon.worldBound));
        //Found at least one
        if (slots.Count() != 0)
        {
            InventorySlot_UI closestSlot = slots.OrderBy(x => Vector2.Distance
               (x.worldBound.position, m_GhostIcon.worldBound.position)).First();

            //Set the new inventory slot with the data
            closestSlot.Set(m_OriginalSlot.storedItem);
            //closestSlot.HoldItem(GameController.GetItemByGuid(m_OriginalSlot.ItemGuid));

            //Clear the original slot
            m_OriginalSlot.Reset();
            //m_OriginalSlot.DropItem();

        }
        //Didn't find any (dragged off the window)
        else
        {
            //m_OriginalSlot.Icon.image =
            //      GameController.GetItemByGuid(m_OriginalSlot.ItemGuid).Icon.texture;
            m_OriginalSlot.Icon.sprite = m_OriginalSprite;
        }
        //Clear dragging related visuals and data
        m_IsDragging = false;
        m_OriginalSlot = null;
        m_GhostIcon.style.visibility = Visibility.Hidden;
    }

    public void StartDrag(Vector2 position, InventorySlot_UI originalSlot)
    {
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;
        m_OriginalSprite = originalSlot.Icon.sprite;
        //Set the new position
        m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;
        //Set the image
        //m_GhostIcon.style.backgroundImage = GameController.GetItemByGuid(originalSlot.ItemGuid).Icon.texture;
        m_GhostIcon.style.backgroundImage = originalSlot.Icon.sprite.texture;
        originalSlot.Icon.sprite = null;
        //Flip the visibility on
        m_GhostIcon.style.visibility = Visibility.Visible;
    }
  
    public void ButtonCallback(Vector2 position, InventorySlot_UI slot)
    {
        selected = slot;
        if (selected != null)
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
                        ((Cauldron)interactable).SelectCauldron((Herb)selected.storedItem.item);

                        if (((Herb)selected.storedItem.item).IsResearched)
                        {
                            ((Cauldron)interactable).SelectCauldron((Herb)selected.storedItem.item);
                        }
                        else
                        {
                            Debug.Log("This Herb needs to be researched!!");
                        }
                    }
                    break;
                case OpenState.General:
                    StartDrag(position, slot);
                    break;
                case OpenState.None:
                    Debug.Log("Not open in a valid state!");
                    break;
            }

        }
    }

    private void OnInventoryAdd(InventoryItem item)
    {
        var emptySlot = InventoryItems.FirstOrDefault(x => x.storedItem == item);

        if (emptySlot != null)
        {
            emptySlot.Set(item);

        }
        else
        {
            emptySlot = InventoryItems.FirstOrDefault(x => x.storedItem == null);
            if (emptySlot != null)
            {
                emptySlot.Set(item);
            }
        }

        


    }

    private void OnInventoryRemove(InventoryItem item)
    {
        var emptySlot = InventoryItems.FirstOrDefault(x => x.storedItem == item);

        if(item.count == 1)
        {
            emptySlot.Count.style.visibility = Visibility.Hidden;
        }
        else if(item.count <= 0)
        {
            if (emptySlot != null)
            {
                emptySlot.Reset();
            }
        }
        else
        {
            emptySlot.Count.text = item.count.ToString();
        }

        


    }

    
}
