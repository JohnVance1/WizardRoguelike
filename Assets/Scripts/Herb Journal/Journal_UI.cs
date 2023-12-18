using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using System;
using UnityEngine.U2D;

public class Journal_UI : SerializedMonoBehaviour
{
    public Journal journalOBJ;
    public Herb currentRightHerb;
    public Herb currentLeftHerb;
    public int herbIndex;

    private VisualElement m_Root;

    private VisualElement pageLeft;
    private VisualElement herbImage_L;
    private VisualElement herbElements_L;
    private Label herbName_L;
    private Label whereToFind_L;
    private VisualElement prevBtn_L;


    private VisualElement pageRight;
    private VisualElement herbImage_R;
    private VisualElement herbElements_R;
    private Label herbName_R;
    private Label whereToFind_R;
    private VisualElement nextBtn_R;


    void Awake()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        pageLeft = m_Root.Q<VisualElement>("PageLeft");
        pageRight = m_Root.Q<VisualElement>("PageRight");

        herbImage_L = pageLeft.Q<VisualElement>("HerbImage");
        herbElements_L = pageLeft.Q<VisualElement>("HerbElements");
        herbName_L = pageLeft.Q<Label>("HerbName");
        whereToFind_L = pageLeft.Q<Label>("TextBoxWhereToFind");
        prevBtn_L = pageLeft.Q<VisualElement>("PreviousButton");

        herbImage_R = pageRight.Q<VisualElement>("HerbImage");
        herbElements_R = pageRight.Q<VisualElement>("HerbElements");
        herbName_R = pageRight.Q<Label>("HerbName");
        whereToFind_R = pageRight.Q<Label>("TextBoxWhereToFind");
        nextBtn_R = pageRight.Q<VisualElement>("NextButton");


        prevBtn_L.RegisterCallback<ClickEvent>(TurnLeftPage);
        nextBtn_R.RegisterCallback<ClickEvent>(TurnRightPage);
        herbIndex = 0;
        SetPageInfo(journalOBJ.herbOrder[0], herbImage_L, herbElements_L, herbName_L, whereToFind_L);
        SetPageInfo(journalOBJ.herbOrder[1], herbImage_R, herbElements_R, herbName_R, whereToFind_R);

        //SetPageLeftInfo(journalOBJ.herbOrder[0]);
        //SetPageRightInfo(journalOBJ.herbOrder[1]);

    }

    private void OnEnable()
    {
        GameEventsManager.instance.journalEvents.onFirstherbCollected += HerbCollected;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.journalEvents.onFirstherbCollected -= HerbCollected;

    }

    /// <summary>
    /// Adds up the Herbs collected until the amount needed is reached
    /// </summary>
    private void HerbCollected(Herb herb)
    {
        int index = journalOBJ.herbOrder.IndexOf(herb);

        if(index == 0 || index % 2 == 0)
        {
            SetPageInfo(herb, herbImage_L, herbElements_L, herbName_L, whereToFind_L);

        }
        else
        {
            SetPageInfo(herb, herbImage_R, herbElements_R, herbName_R, whereToFind_R);

        }


    }

    private void TurnLeftPage(ClickEvent evt)
    {
        herbIndex -= 2;

        if (evt.button != 0 || herbIndex < 0 || herbIndex >= journalOBJ.herbOrder.Count)
        { 
            herbIndex= 0;
            return;
        }
        TurnPage(herbIndex);        

    }

    private void TurnRightPage(ClickEvent evt)
    {
        herbIndex += 2;
        if (evt.button != 0 || herbIndex < 0 || herbIndex >= journalOBJ.herbOrder.Count)
        {
            herbIndex = journalOBJ.herbOrder.Count - 2;
            return;
        }
        TurnPage(herbIndex);

    }

    private void TurnPage(int index)
    {
        //SetPageLeftInfo(journalOBJ.herbOrder[index]);
        //index++;
        //SetPageRightInfo(journalOBJ.herbOrder[index]);

        SetPageInfo(journalOBJ.herbOrder[index], herbImage_L, herbElements_L, herbName_L, whereToFind_L);
        index++;
        SetPageInfo(journalOBJ.herbOrder[index], herbImage_R, herbElements_R, herbName_R, whereToFind_R);

    }

    
    private void SetPageInfo(Herb currentHerb, VisualElement herbImage, 
        VisualElement herbElements, Label herbName, Label whereToFind)
    {
        herbImage.style.backgroundImage = new StyleBackground(currentHerb.sprite);

        if (currentHerb.IsFound)
        {
            herbImage.style.unityBackgroundImageTintColor = Color.white;
            herbName.text = currentHerb.name;

        }
        else
        {
            herbImage.style.unityBackgroundImageTintColor = Color.black;
            herbName.text = "???";

        }

        if (currentHerb.IsResearched)
        {
            herbElements.style.backgroundColor = Color.white;
            whereToFind.text = currentHerb.name;    // Needs to be changed to whatever the decriptive text about this herb is
        }
        else
        {
            herbElements.style.backgroundColor = Color.black;
            whereToFind.text = "???";

        }


        //herbElements.sprite = currentHerb.sprite;
    }

    //private void SetPageLeftInfo(Herb currentHerb)
    //{
    //    //herbImage_L.sprite = currentHerb.sprite;
    //    herbImage_L.style.backgroundImage = new StyleBackground(currentHerb.sprite);

    //    if (currentHerb.IsFound)
    //    {
    //        herbImage_L.style.unityBackgroundImageTintColor = Color.white;
    //    }
    //    else
    //    {
    //        herbImage_L.style.unityBackgroundImageTintColor = Color.black;

    //    }


    //    herbImage_L.style.backgroundImage = new StyleBackground(currentHerb.sprite);
    //    //herbElements_L.sprite = currentHerb.sprite;
    //    whereToFind_L.text = currentHerb.name;
    //}

    //private void SetPageRightInfo(Herb currentHerb)
    //{
    //    //herbImage_R.sprite = currentHerb.sprite;
    //    herbImage_R.style.backgroundImage = new StyleBackground(currentHerb.sprite);

    //    if (currentHerb.IsFound)
    //    {
    //        herbImage_R.style.unityBackgroundImageTintColor = Color.white;
    //    }
    //    else
    //    {
    //        herbImage_R.style.unityBackgroundImageTintColor = Color.black;

    //    }
    //    //herbElements_R
    //    whereToFind_R.text = currentHerb.name;

    //}



}
