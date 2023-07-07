using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JournalSlot : MonoBehaviour
{
    public bool IsFound;
    public Herb herbSlot;
    public Image icon;
    public Sprite sp;
    public GameObject herbPage;

    private void OnEnable()
    {
        //icon.sprite = sp;
    }

    public void Set()
    {
        icon.sprite = herbSlot.defaultSprite;
    }

    public void OpenHerbPage()
    {
        if(IsFound) 
        {
            herbPage.SetActive(true);
        }
    }
    public void CloseHerbPage()
    {
        herbPage.SetActive(false);
    }

}
