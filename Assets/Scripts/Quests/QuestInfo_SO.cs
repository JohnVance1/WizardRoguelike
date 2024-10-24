using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo_SO", menuName = "ScriptableObjects/QuestInfo_SO", order = 1)]
public class QuestInfo_SO : ScriptableObject
{
    [SerializeField] public string id { get; set; }  // The ID of the quest

    [Header("General")]
    public string displayName;  // The name displayed to the player as to which quest this is

    [Header("Requirements")]
    public int levelRequirement;    // The level requirement (set to 0 if none)

    public QuestInfo_SO[] questPrereqs; // Any quests that need to be completed first before this one is available

    [Header("Steps")]
    public GameObject[] questStepPrefabs;   // The steps in the quest that are needed to complete it (There needs to be at least 1)
    public string questProgressionText;   // What you need to do for the quest


    [Header("Rewards")]
    public int goldReward;  // The reward in gold for completing the quest
    public int expReward;   // The reward in experience for completing the quest
    public string rewardText;   // What the reward is




    // Makes sure that the id is always the name of the ScriptableObject asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}
