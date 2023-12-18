using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The manager for all of the game events(delegates) in the game
/// </summary>
public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public JournalEvents journalEvents;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 Game Events Manager in Scene");

        }
        instance = this;

        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        journalEvents = new JournalEvents();
    }
}
