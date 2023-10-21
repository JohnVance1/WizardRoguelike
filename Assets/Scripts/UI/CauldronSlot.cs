using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class CauldronSlot : Slot_UI
{
    public delegate void OnMouseDown(Vector2 pos, InventoryItem slot);
    public OnMouseDown onMouseDown;

    public delegate void OnNoStoredHerb(Slot_UI slot);
    public OnNoStoredHerb onNoStoredHerb;



    public CauldronSlot()
    {
        Icon = new Image();
        Add(Icon);
        Icon.AddToClassList("slotIcon");


        RegisterCallback<PointerDownEvent>(OnPointerDown);

    }    

    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button
        if (evt.button != 0)
        {
            return;
        }
        if(storedItem == null)
        {
            onNoStoredHerb(this);
        }
        else
        {
            onMouseDown(evt.position, storedItem);
            Reset();

        }
        //Clear the image
        //Icon.image = null;
        //Start the drag
        //InventoryUIController.StartDrag(evt.position, this);

        //InventoryUIController.ButtonCallback(evt.position, this);

    }

    public override void Set(InventoryItem item)
    {
        if (storedItem == null)
        {
            storedItem = item;
            Icon.sprite = item.item.sprite;
        }
    }

    public override void Reset()
    {
        defaultIcon = null;
        storedItem = null;
        Icon.sprite = null;
        //icon.sprite = defaultIcon;
        //countObj.SetActive(false);
        //countLabel.text = null;
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<CauldronSlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
