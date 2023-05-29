using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Herb", order = 1)]
public class Herb : Item_Base
{
    [SerializeField]
    public Sprite[] herbStages;

    public float timeToGrow;
    public int researchNum;


}
