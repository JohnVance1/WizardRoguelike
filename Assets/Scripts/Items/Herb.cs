using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProcessType
{
    Raw,
    Crush,
    Smoker,
    Distilled,
}

[Flags]
public enum Element
{
    None = 0,
    Light = 1,
    Shadow = 2,
    Fire = 4,
    Ice = 8,
    Earth = 16,
    Air = 32,
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Herb", order = 1)]
public class Herb : Item_Base
{
    [SerializeField]
    public Sprite[] herbStages;

    public Sprite defaultSprite;

    public float timeToGrow;
    public float timeToProcess;

    public int researchNum;

    public bool IsResearched = false;

    public ProcessType processType = ProcessType.Raw;

    [SerializeField]
    public Dictionary<Element, int> elements = new Dictionary<Element, int>();


}
