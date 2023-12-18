using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Slot_UI : Button
{
    public InventoryItem storedItem;
    protected Sprite defaultIcon;
    public Image Icon;
    private Color c;

    public Slot_UI()
    {
        RegisterCallback<FocusInEvent>(OnFocusInSlot);
        RegisterCallback<FocusOutEvent>(OnFocusOutSlot);
        c = this.style.backgroundColor.value;
    }

  

    //public abstract void Set(InventoryItem item);
    public abstract void Set(InventoryItem item, ProcessType type = ProcessType.Raw);


    public abstract void Reset();

    public void OnFocusInSlot(FocusInEvent evt)
    {
        this.style.backgroundColor = Color.white;
    }

    public void OnFocusOutSlot(FocusOutEvent evt)
    {
        this.style.backgroundColor = c;

    }
}
