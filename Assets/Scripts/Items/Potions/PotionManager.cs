using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : SerializedMonoBehaviour
{
    // The Map of all of the quests in the game
    [SerializeField]
    private Dictionary<string, Potion> potionMap;



    private void Awake()
    {
        potionMap = CreatePotionMap();



    }

    /// <summary>
    /// Loads all of the potions into the Map from the Assets/Resources/Quests folder
    /// All Quests need to be QuestInfo_SO ScriptableObjects
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, Potion> CreatePotionMap()
    {
        // Loads all PotionInfo_SO objects under the Assets/Resoucres/Potions folder
        PotionInfo_SO[] allPotions = Resources.LoadAll<PotionInfo_SO>("Potions");

        Dictionary<string, Potion> idToPotionMap = new Dictionary<string, Potion>();
        foreach (PotionInfo_SO potionInfo in allPotions)
        {
            if (idToPotionMap.ContainsKey(potionInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when making potionMap: " + potionInfo.id);
            }
            // Loads any potions that were saved in PlayerPerfs
            idToPotionMap.Add(potionInfo.id, LoadPotion(potionInfo));
        }
        return idToPotionMap;
    }

    /// <summary>
    /// Helper method for getting the quest by it's ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private Potion GetPotionByID(string id)
    {
        Potion potion = potionMap[id];
        if (potion == null)
        {
            Debug.LogError("ID not found in Quest Map: " + id);
        }
        return potion;
    }

    private Potion GetPotionByElement(Dictionary<Element, int> herbSum)
    {
        foreach(var pot in potionMap)
        {
            bool val = true;
            foreach(var pair in herbSum)
            {
                if (pot.Value.info.elementsNeeded[pair.Key] !=
                    pair.Value)
                {
                    val = false;
                }
            }
            if(val)
            {
                return pot.Value;
            }
        }

        return null;
        
    }

    /// <summary>
    /// Loads each quest in PlayerPrefs from the QuestInfo_SO.id
    /// </summary>
    /// <param name="questInfo"></param>
    /// <returns></returns>
    private Potion LoadPotion(PotionInfo_SO potionInfo)
    {
        Potion potion = null;
        try
        {
            //if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            //{
            //    string serializedData = PlayerPrefs.GetString(questInfo.id);
            //    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
            //    potion = new Potion(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            //}
            //else
            //{
            //}
            potion = new Potion(potionInfo);

        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load Quest: " + potionInfo.displayName + ": " + e);
        }
        return potion;
    }


    public Potion CalculatePotion(List<Herb> herbs)
    {
        //Dictionary<Element, int> herbSum = new Dictionary<Element, int>();
        //var v = Enum.GetValues(typeof(Element));
        //foreach (Element h in v)
        //{
        //    herbSum[h] = 0;
        //}

        //foreach (Herb h in herbs)
        //{
        //    foreach(var p in h.elements)
        //    {
        //        herbSum[p.Key] += p.Value;
        //    }
        //}

        //Potion potion = GetPotionByElement(herbSum);

        //if(potion == null)
        //{
        //    Debug.LogWarning("No potion found!!!");
        //}

        //return potion;


        return potionMap["SleepPotion"];




    }



}
