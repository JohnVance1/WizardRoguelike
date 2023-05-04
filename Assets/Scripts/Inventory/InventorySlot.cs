using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    public void Set(InventoryItem item)
    {
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
