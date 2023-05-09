using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool mouse_over = false;

    private InventorySlot slot;

    [SerializeField]
    private Player player;

    public delegate void ClickEvent();
    public ClickEvent OnClick;


    private void Start()
    {
        slot = GetComponent<InventorySlot>();
    }

    void Update()
    {
        if (mouse_over)
        {

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Clicked");
        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        slot.highlight.SetActive(true);
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        slot.highlight.SetActive(false);

        Debug.Log("Mouse exit");
    }
}
