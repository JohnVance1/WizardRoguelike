using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class CauldronSlot : Slot_UI
{
    public delegate void OnMouseDown(InventoryItem slot);
    public OnMouseDown onMouseDown;

    public delegate void OnNoStoredHerb(Slot_UI slot);
    public OnNoStoredHerb onNoStoredHerb;



    //public CauldronSlot()
    //{
    //    Icon = new Image();
    //    //Add(Icon);
    //    Icon.AddToClassList("slotIcon");

    //    //clicked += OnPointerDown;

    //}    

    public void OnPointerDown()
    {
        //Not the left mouse button
        
        if(storedItem == null)
        {
            onNoStoredHerb(this);
        }
        else
        {
            onMouseDown(storedItem);
            Clear();

        }
        //Clear the image
        //Icon.image = null;
        //Start the drag
        //InventoryUIController.StartDrag(evt.position, this);

        //InventoryUIController.ButtonCallback(evt.position, this);

    }

    public override void Set(InventoryItem item, ProcessType type = ProcessType.Raw)
    {
        if (storedItem == null)
        {
            storedItem = item;
            if(type != ProcessType.Raw)
            {
                icon.sprite = ((Herb)item.item).processSprites[type];
            }
            else
            {
                icon.sprite = item.item.sprite;
            }
            icon.gameObject.SetActive(true);

        }
    }

    public override void Clear()
    {
        defaultIcon = null;
        storedItem = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        //Icon.sprite = null;
        //icon.sprite = defaultIcon;
        //countObj.SetActive(false);
        //countLabel.text = null;
    }

    //    #region UXML
    //    [Preserve]
    //    public new class UxmlFactory : UxmlFactory<CauldronSlot, UxmlTraits> { }
    //    [Preserve]
    //    public new class UxmlTraits : VisualElement.UxmlTraits { }
    //    #endregion
}
