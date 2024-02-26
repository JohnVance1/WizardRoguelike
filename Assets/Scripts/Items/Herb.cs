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
    [field: SerializeField] public string id { get; private set; }  // The ID of the herb

    [Header("General")]
    public string displayName;  // The name displayed to the player as to which herb this is


    public float timeToGrow;
    public float timeToProcess;

    public int researchNum;

    public bool IsResearched = false;
    public bool IsFound = false;

    public Sprite graph;
    public string info;

    public ProcessType processType = ProcessType.Raw;

    [SerializeField]
    public Dictionary<Element, int> elements = new Dictionary<Element, int>();

    [Header("Sprites")]
    public Sprite defaultSprite;

    [SerializeField]
    public Sprite[] herbStages;

    [SerializeField]
    public Dictionary<ProcessType, Sprite> processSprites = new Dictionary<ProcessType, Sprite>();

    // Makes sure that the id is always the name of the ScriptableObject asset
    private void OnValidate()
    {
    #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
    #endif
    }


}
