using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class FountainSlotUI : Slot_UI
{
    public delegate void OnMouseDown(InventoryItem slot);
    public OnMouseDown onMouseDown;

    public delegate void OnNoStoredHerb(Slot_UI slot);
    public OnNoStoredHerb onNoStoredPotion;

    public FountainSlotUI()
    {
        Icon = new Image();
        Add(Icon);
        Icon.AddToClassList("slotIcon");

        clicked += OnPointerDown;

    }


    private void OnPointerDown()
    {
        //Not the left mouse button
        
        if (storedItem == null)
        {
            onNoStoredPotion(this);
        }
        else
        {
            onMouseDown(storedItem);
            Reset();

        }
       

    }

    public override void Set(InventoryItem item, ProcessType type = ProcessType.Raw)
    {
        if (storedItem == null)
        {
            storedItem = item;
            Icon.sprite = item.item.sprite;
        }
    }

    public override void Reset()
    {
        Icon.sprite = null;
        storedItem = null;
    }

    

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<FountainSlotUI, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
