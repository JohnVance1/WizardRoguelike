using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.Interactions;

public class PotionUIController : SerializedMonoBehaviour
{
    public GameObject selected;

    public Cauldron cauldron;

    //public List<Herb> storedHerbs;
    public Herb currentHerb;
    public List<Device> devices;
    public Potion potion;


    public Player player;
    //private VisualElement m_Root;

    //private Button m_Distiller;
    //private ProgressBar m_DistilBar;

    //private Button m_Crusher;
    //private ProgressBar m_CrushBar;

    //private Button m_Smoker;
    //private ProgressBar m_SmokeBar;

    public Button m_Exit;
    public Button m_Make;

    //private List<VisualElement> m_CauldronSlots;
    [SerializeField]
    public List<CauldronSlot> CauldronSlots = new List<CauldronSlot>();
    //private VisualElement m_GhostIcon;

    private Slot_UI m_OriginalSlot;
    private Sprite m_OriginalSprite;



    public bool IsActive;
    public bool IsFinished;
    public Herb storedHerb;
    //public Herb selectedHerb;
    public ProcessType type;
    public PotionManager potionManager;


    public delegate void OnFinishedDevice(Herb herb, ProcessType type);
    public OnFinishedDevice onFinishedDevice;

    public delegate void OnStartedDevice();
    public OnStartedDevice onStartedDevice;

    public Slider slider;
    public float processTime = 2f;
    public float elapsedTime = 0f;
    private bool m_IsDragging;



    void Awake()
    {
        //m_Root = GetComponent<UIDocument>().rootVisualElement;
        //m_Distiller = m_Root.Q<Button>("Distiller");
        //m_DistilBar = m_Root.Q<ProgressBar>("Distil_Bar");

        //m_Crusher = m_Root.Q<Button>("Crusher");
        //m_CrushBar = m_Root.Q<ProgressBar>("Crush_Bar");

        //m_Smoker = m_Root.Q<Button>("Smoker");
        //m_SmokeBar = m_Root.Q<ProgressBar>("Smoke_Bar");

        //m_Exit = m_Root.Q<Button>("ExitButton");
        //m_Make = m_Root.Q<Button>("MakePotion");

        ////m_CauldronSlots = m_Root.Q<VisualElement>("Cauldron").hierarchy.Children().ToList();
        //m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");


        // m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        //m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);

        //m_Exit.RegisterCallback<ClickEvent>(CloseUI);
        //m_Exit += CloseUI;
        //m_Make.clicked += MakePotion;
        //m_Distiller.clicked += ActivateDistil;
        //m_Crusher.clicked += ActivateCrush;
        //m_Smoker.clicked += ActivateSmoke;

       
        IsFinished = true;

    }

    private void OnEnable()
    {
        foreach (CauldronSlot slot in CauldronSlots)
        {
            slot.onMouseDown += ButtonCallback;
            slot.onNoStoredHerb += SetSlotHerb;
        }
    }

    private void OnDisable()
    {
        foreach (CauldronSlot slot in CauldronSlots)
        {
            slot.onMouseDown -= ButtonCallback;
            slot.onNoStoredHerb -= SetSlotHerb;
        }
    }




    public void ButtonCallback(InventoryItem item)
    {
        player.AddItemToInventory(cauldron.AddBackHerb((Herb)item.item));
        cauldron.RemoveHerb((Herb)item.item);

    }

    public void SetSlotHerb(Slot_UI slot)
    {
        if (cauldron.storedHerbs.Count >= 4)
        {
            Debug.Log("Cauldron Storage Reached!!!");
            return;
        }
        if(currentHerb == null)
        {
            return;
        }
        player.RemoveItemFromInventory(currentHerb);
        cauldron.storedHerbs.Add(currentHerb);

        InventoryItem newItem = new InventoryItem(currentHerb);

        slot.Set(newItem);
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

    ////private void OnPointerUp(PointerUpEvent evt)
    ////{
    ////    if (!m_IsDragging)
    ////    {
    ////        return;
    ////    }
    ////    //Check to see if they are dropping the ghost icon over any inventory slots.
    ////    IEnumerable<Slot_UI> slots = CauldronSlots.Where(x =>
    ////           x.worldBound.Overlaps(m_GhostIcon.worldBound));
    ////    //Found at least one
    ////    if (slots.Count() != 0)
    ////    {
    ////        Slot_UI closestSlot = slots.OrderBy(x => Vector2.Distance
    ////           (x.worldBound.position, m_GhostIcon.worldBound.position)).First();

    ////        //Set the new inventory slot with the data
    ////        closestSlot.Set(m_OriginalSlot.storedItem);
    ////        //closestSlot.HoldItem(GameController.GetItemByGuid(m_OriginalSlot.ItemGuid));

    ////        //Clear the original slot
    ////        m_OriginalSlot.Reset();
    ////        //m_OriginalSlot.DropItem();

    ////    }
    ////    //Didn't find any (dragged off the window)
    ////    else
    ////    {
    ////        //m_OriginalSlot.Icon.image =
    ////        //      GameController.GetItemByGuid(m_OriginalSlot.ItemGuid).Icon.texture;
    ////        m_OriginalSlot.Icon.sprite = m_OriginalSprite;
    ////    }
    ////    //Clear dragging related visuals and data
    ////    m_IsDragging = false;
    ////    m_OriginalSlot = null;
    ////    m_GhostIcon.style.visibility = Visibility.Hidden;
    ////}

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

    public void CloseUI()
    {
        //if (evt.propagationPhase != PropagationPhase.AtTarget)
        //    return;
        // Assign a new color
        //var targetBox = evt.target as VisualElement;
        //targetBox.style.backgroundColor = Color.green;
        CancelPotion();
        cauldron.CloseCauldronUI();
    }

    //private void ActivateDistil()
    //{
    //    type = ProcessType.Distilled;
    //    ActivateDevice(m_DistilBar);
    //}
    //private void ActivateCrush()
    //{
    //    type = ProcessType.Crush;
    //    ActivateDevice(m_CrushBar);

    //}
    //private void ActivateSmoke()
    //{
    //    type = ProcessType.Smoker;
    //    ActivateDevice(m_SmokeBar);

    //}

    #region Device Logic
    //private void ActivateDevice(ProgressBar bar)
    //{
    //    if(cauldron.storedHerbs.Count >= 4)
    //    {
    //        Debug.Log("Cauldron Storage Reached!!!");
    //        return;
    //    }

    //    if (currentHerb != null && IsFinished)
    //    {
    //        bar.style.visibility = Visibility.Visible;
    //        IsFinished = false;
    //        storedHerb = Object.Instantiate(currentHerb);
    //        //currentHerb = null;
    //        if (storedHerb != null && !IsActive)
    //        {
    //            IsActive = true;
    //            //onStartedDevice();
    //            DeviceSelected();
    //            StartCoroutine(UpdateDevice(bar));
    //        }
    //    }
    //}

    //public IEnumerator UpdateDevice(ProgressBar bar)
    //{
    //    while(elapsedTime <= processTime)
    //    {
    //        ProgressSlider(bar);
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //    yield return null;
    //    //yield return new WaitForSeconds(processTime);
    //    IsActive = false;
    //    bar.style.visibility = Visibility.Hidden;
    //    bar.value = 0;
    //    elapsedTime = 0f;
    //    IsFinished = true;
    //    Herb temp = storedHerb;
    //    storedHerb = null;
    //    //onFinishedDevice(temp, type);
    //    SetStoredHerb(temp, type);
    //    //RemoveOnClick();

    //}
    //public void ProgressSlider(ProgressBar bar)
    //{
    //    elapsedTime += Time.deltaTime;
    //    float percentComplete = elapsedTime / processTime;
    //    bar.value = elapsedTime;
    //}

    #endregion

    public void SetRawHerb()
    {
        if (currentHerb != null)
        {
            Instantiate(currentHerb);
            cauldron.storedHerbs.Add(currentHerb);
            cauldron.storedHerbs[cauldron.storedHerbs.Count - 1].processType = ProcessType.Raw;
            currentHerb = null;

        }
    }

    public void SetStoredHerb(Herb herb, ProcessType type = ProcessType.Raw)
    {
        if (herb != null)
        {
            cauldron.storedHerbs.Add(herb);
            cauldron.storedHerbs[cauldron.storedHerbs.Count - 1].processType = type;
            AddToSlots(herb, type);
            //currentHerb = null;

        }
    }

    public void AddToSlots(Herb herb, ProcessType type = ProcessType.Raw)
    {
        foreach(CauldronSlot slot in CauldronSlots)
        {
            if(slot.storedItem == null)
            {
                InventoryItem newItem = new InventoryItem(herb);

                slot.Set(newItem, type);
                break;
            }
        }
    }

    public void MakePotion()
    {
        if (cauldron.storedHerbs.Count > 0)
        {
            Potion p = potionManager.CalculatePotion(cauldron.storedHerbs);
            player.AddItemToInventory(p.info);
            //player.AddItemToInventory(potion.info);

            cauldron.storedHerbs.Clear();
            currentHerb = null;
        }
    }

    public void CancelPotion()
    {
        foreach (Herb h in cauldron.storedHerbs)
        {

            player.AddItemToInventory(cauldron.AddBackHerb(h));
        }
        cauldron.storedHerbs.Clear();

        foreach (CauldronSlot slot in CauldronSlots)
        {
            slot.Clear();

        }
        currentHerb = null;
    }

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public void UseDevice(Herb herb, Player p)
    {
        currentHerb = herb;
        player = p;
        storedHerb = Object.Instantiate(currentHerb);
        DeviceSelected();
        SetStoredHerb(storedHerb);
        storedHerb = null;

    }

    public void DeviceSelected()
    {
        if (currentHerb != null)
        {
            player.RemoveItemFromInventory(currentHerb);
            currentHerb = null;
            
        }

    }
}
