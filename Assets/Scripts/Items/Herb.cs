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


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Herb", order = 1)]
public class Herb : Item_Base
{
    [SerializeField]
    public Sprite[] herbStages;

    public Sprite defaultSprite;

    public float timeToGrow;
    public float timeToProcess;

    public int researchNum;

    public ProcessType processType = ProcessType.Raw;


}
