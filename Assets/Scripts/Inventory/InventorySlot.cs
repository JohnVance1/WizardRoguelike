using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultIcon;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI countLabel;
    [SerializeField]
    private GameObject countObj;

    [SerializeField]
    public GameObject highlight;

    public MouseOver mouseOverComp;

    public OpenState openState;
    public InventoryItem storedItem;

    private void Start()
    {
        mouseOverComp = GetComponent<MouseOver>();

    }

    private void Update()
    {
        
    }

    public void ReturnSelf()
    {
        
    }


    public void Set(InventoryItem item)
    {
        storedItem = item;
        icon.sprite = item.item.sprite;
        if(item.count <= 1)
        {
            countObj.SetActive(false);
            return;
        }
        else
        {
            countObj.SetActive(true);
            countLabel.text = item.count.ToString();

        }
    }

    public void Clear()
    {
        icon.sprite = defaultIcon;
        countObj.SetActive(false);
        countLabel.text = null;
    }


}
