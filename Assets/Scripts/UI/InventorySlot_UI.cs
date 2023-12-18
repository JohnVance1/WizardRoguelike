using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot_UI : Slot_UI
{
    public Label Count;

    public string ItemGuid = "";


    //[SerializeField]
    //private Image icon;
    [SerializeField]
    private TextMeshProUGUI countLabel;
    [SerializeField]
    private GameObject countObj;

    [SerializeField]
    public GameObject highlight;

    public MouseOver mouseOverComp;

    public OpenState openState;

    public delegate void OnMouseDown(/*Vector2 pos,*/ Slot_UI slot);
    public OnMouseDown onMouseDown;


    public InventorySlot_UI()
    {
        //Create a new Image element and add it to the root
        Icon = new Image();
        Count = new Label();
        //Count.visible = false;
        Add(Count);
        Add(Icon);
        //Add USS style properties to the elements
        Count.AddToClassList("slotCount");
        Count.text = "0";
        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
        //RegisterCallback<PointerDownEvent>(OnPointerDown);
        clicked += OnPointerDown;
        //this.focusable = true;
        //RegisterCallback<FocusInEvent>(OnFocusInSlot);
        //RegisterCallback<FocusOutEvent>(OnFocusOutSlot);
        //RegisterCallback<NavigationMoveEvent>(e =>
        //{
        //    switch (e.direction)
        //    {
        //        case NavigationMoveEvent.Direction.Left: myElementOnTheLeft.Focus(); break;
        //        case NavigationMoveEvent.Direction.Top: myElementOnTheTop.Focus(); break;
        //            // etc.
        //    }
        //    e.PreventDefault(); // Stop navigation from selecting the default elements it normally would
        //});

    }

    




    private void OnPointerDown()
    {
        //Not the left mouse button
        //if (evt.button != 0 || storedItem == null)
        //{
        //    return;
        //}
        //Clear the image
        //Icon.image = null;
        //Start the drag
        //InventoryUIController.StartDrag(evt.position, this);
        onMouseDown(/*evt.position,*/ this);
        //InventoryUIController.ButtonCallback(evt.position, this);

    }



    public override void Set(InventoryItem item, ProcessType type = ProcessType.Raw)
    {        
        if(storedItem == null)
        {
            storedItem = item;
            Icon.sprite = item.item.sprite;
        }

        if (item.count <= 1)
        {
            //countObj.SetActive(false);
            
            //Count.visible = false;
            Count.style.visibility = Visibility.Hidden;

            return;
        }
        else
        {
            //countObj.SetActive(true);
            //countLabel.text = item.count.ToString();
            //Count.visible = true;
            Count.style.visibility = Visibility.Visible;
            Count.text = item.count.ToString();


        }
    }

    public override void Reset()
    {
        Icon.sprite = null;
        storedItem = null;
        Count.style.visibility = Visibility.Hidden;
        Count.text = "0";
        //icon.sprite = defaultIcon;
        //countObj.SetActive(false);
        //countLabel.text = null;
    }

    //public void DropItem()
    //{
    //    ItemGuid = "";
    //}
    //public void HoldItem(ItemDetails item)
    //{
    //    Icon.image = item.Icon.texture;
    //    ItemGuid = item.GUID;
    //}

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot_UI, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
