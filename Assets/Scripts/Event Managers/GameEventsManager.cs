using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;
     

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
    }
}
