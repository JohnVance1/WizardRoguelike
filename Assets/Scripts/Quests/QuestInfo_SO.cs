using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo_SO", menuName = "ScriptableObjects/QuestInfo_SO", order = 1)]
public class QuestInfo_SO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;

    public QuestInfo_SO[] questPrereqs;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;


    [Header("Rewards")]
    public int goldReward;
    public int expReward;





    // Makes sure that the id is always the name of the ScriptableObject asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}
