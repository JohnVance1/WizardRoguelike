using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fountain_UI : MonoBehaviour
{
    public List<FountainSlotUI> storageSlots;

    private VisualElement m_Root;
    private FountainSlotUI Slot1;
    private FountainSlotUI Slot2;
    private FountainSlotUI Slot3;
    private FountainSlotUI Slot4;

    public Fountain fountain;
    public Player player;
    public PotionInfo_SO currentPotion;


    private void Awake()
    {
        storageSlots = new List<FountainSlotUI>();
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        Slot1 = m_Root.Q<FountainSlotUI>("Slot1");
        Slot2 = m_Root.Q<FountainSlotUI>("Slot2");
        Slot3 = m_Root.Q<FountainSlotUI>("Slot3");
        Slot4 = m_Root.Q<FountainSlotUI>("Slot4");
        storageSlots.Add(Slot1);
        storageSlots.Add(Slot2);
        storageSlots.Add(Slot3);
        storageSlots.Add(Slot4);


    }

    private void OnEnable()
    {
        foreach (FountainSlotUI slot in storageSlots)
        {
            slot.onMouseDown += ButtonCallback;
            slot.onNoStoredPotion += SetSlot;
        }
        
    }

    private void OnDisable()
    {
        foreach (FountainSlotUI slot in storageSlots)
        {
            slot.onMouseDown -= ButtonCallback;
            slot.onNoStoredPotion -= SetSlot;
        }
    }

    private void Start()
    {
        this.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        this.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    public void SetPotion(PotionInfo_SO potion, Player p)
    {
        currentPotion = potion;
        player = p;
    }

    public void ButtonCallback(InventoryItem item)
    {
        player.AddItemToInventory(fountain.AddBackPotion((PotionInfo_SO)item.item));
        fountain.RemovePotion((PotionInfo_SO)item.item);

    }
    
    public void SetSlot(Slot_UI slot)
    {
        if (fountain.storageCurrent.Count >= 4)
        {
            Debug.Log("Cauldron Storage Reached!!!");
            return;
        }
        if (currentPotion == null)
        {
            return;
        }
        player.RemoveItemFromInventory(currentPotion);
        fountain.storageCurrent.Add(currentPotion);

        InventoryItem newItem = new InventoryItem(currentPotion);

        slot.Set(newItem);
    }



}
