using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public abstract class Slot_UI : MonoBehaviour
{
    public InventoryItem storedItem;
    protected Sprite defaultIcon;
    [SerializeField]
    protected Image icon;
    private Color c;

    //public Slot_UI()
    //{
    //    //RegisterCallback<FocusInEvent>(OnFocusInSlot);
    //    //RegisterCallback<FocusOutEvent>(OnFocusOutSlot);


    //    c = new Color(0.3f, 0.4f, 0.6f, 0.3f);

    //}


  

    //public abstract void Set(InventoryItem item);
    public abstract void Set(InventoryItem item, ProcessType type = ProcessType.Raw);


    public abstract void Clear();
        
}
