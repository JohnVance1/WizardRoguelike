using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public Player player;
    public Journal journalOBJ;


    void Awake()
    {
        journalOBJ.herbMap = CreateHerbMap();
        journalOBJ.herbOrder = PutHerbsInOrder();


    }

   

    /// <summary>
    /// Loads all of the quests into the Map from the Assets/Resources/Quests folder
    /// All Quests need to be QuestInfo_SO ScriptableObjects
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, Herb> CreateHerbMap()
    {
        // Loads all QuestInfo_SO objects under the Assets/Resoucres/Quests folder
        Herb[] allHerbs = Resources.LoadAll<Herb>("Herbs");

        Dictionary<string, Herb> idToHerbMap = new Dictionary<string, Herb>();
        foreach (Herb herbInfo in allHerbs)
        {
            if (idToHerbMap.ContainsKey(herbInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when making herbMap: " + herbInfo.id);
            }
            // Loads any herbs that were saved in PlayerPerfs
            idToHerbMap.Add(herbInfo.id, herbInfo);
        }
        return idToHerbMap;
    }

    /// <summary>
    /// Loads each quest in PlayerPrefs from the QuestInfo_SO.id
    /// </summary>
    /// <param name="questInfo"></param>
    /// <returns></returns>
    private Herb LoadPotion(Herb herbInfo)
    {
        Herb herb = null;
        try
        {
            return herbInfo;

        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load Quest: " + herbInfo.displayName + ": " + e);
        }
        return herb;
    }

    private List<Herb> PutHerbsInOrder()
    {
        List<Herb> newList = new List<Herb>();
        foreach (var herb in journalOBJ.herbMap)
        {
            newList.Add(herb.Value);

        }
        return newList;
    }
}
