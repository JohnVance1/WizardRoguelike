using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot_UI : VisualElement
{
    public Image Icon;
    public string ItemGuid = "";
    public InventorySlot_UI()
    {
        //Create a new Image element and add it to the root
        Icon = new Image();
        Add(Icon);
        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
    }
}
