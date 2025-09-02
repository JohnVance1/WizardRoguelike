using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PotionInfo_SO", menuName = "ScriptableObjects/PotionInfo_SO", order = 1)]
public class PotionInfo_SO : Item_Base
{
    [field: SerializeField] public string id { get; private set; }  // The ID of the potion

    [Header("General")]
    public string displayName;  // The name displayed to the player as to which potion this is

    public string info = "I am a Potion!";

    public PotionEffect potionEffect;
    public Sprite graph;

    public bool IsFound = false;

    public Dictionary<Element, int> elementsNeeded= new Dictionary<Element, int>();

    public float usageTime;

    public int radius;

    public int effectArea_XPos = 3;

    [TableMatrix(IsReadOnly = true)]
    public bool[,] potionEffectArea = new bool[3,3];

    private void Awake()
    {
    }

    // Makes sure that the id is always the name of the ScriptableObject asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    protected virtual void UsePotion(){ }

}
