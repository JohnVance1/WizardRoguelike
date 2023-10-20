using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item_Base", order = 1)]
public class Item_Base : SerializedScriptableObject
{
    public Sprite sprite;
    //public string GUID;
    public string Name;
}
