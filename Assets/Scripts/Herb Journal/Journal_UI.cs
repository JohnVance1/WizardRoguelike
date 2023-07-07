using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Journal_UI : SerializedMonoBehaviour
{
    public Player player;
    //public List<JournalSlot> slots;
    [SerializeField]
    public Dictionary<Herb, JournalSlot> slotsDict;

    private void OnEnable()
    {
        foreach (Herb h in player.journal.foundHerbs)
        {
            slotsDict[h].IsFound = true;
            slotsDict[h].Set();

        }
    }

    public void UpdateHerbs(Herb herb)
    {
        slotsDict[herb].IsFound = true;
    }

}
