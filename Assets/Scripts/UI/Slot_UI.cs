using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Slot_UI : VisualElement
{
    public InventoryItem storedItem;
    protected Sprite defaultIcon;
    public Image Icon;

    public abstract void Set(InventoryItem item);

    public abstract void Reset();
}
