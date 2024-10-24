using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private bool loadQuestState = true;

    // The Map of all of the quests in the game
    public Dictionary<string, Quest> questMap { get; private set; }

    public static QuestManager instance { get; private set; }



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 Game Events Manager in Scene");

        }
        instance = this;

        questMap = CreateQuestMap();

        
        
    }

    /// <summary>
    /// Loads all of the quests into the Map from the Assets/Resources/Quests folder
    /// All Quests need to be QuestInfo_SO ScriptableObjects
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, Quest> CreateQuestMap()
    {
        // Loads all QuestInfo_SO objects under the Assets/Resoucres/Quests folder
        QuestInfo_SO[] allQuests = Resources.LoadAll<QuestInfo_SO>("Quests");

        Dictionary<string, Quest> idToQuestMap= new Dictionary<string, Quest>();
        foreach(QuestInfo_SO questInfo in allQuests)
        {
            if(idToQuestMap.ContainsKey(questInfo.id)) 
            {
                Debug.LogWarning("Duplicate ID found when making questMap: " + questInfo.id);
            }
            // Loads any quests that were saved in PlayerPerfs
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    /// <summary>
    /// Helper method for getting the quest by it's ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if(quest== null)
        {
            Debug.LogError("ID not found in Quest Map: " + id);
        }
        return quest;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            // Respawns the Quest if it was IN_PROGRESS when the game closed
            if(quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    /// <summary>
    /// Calls the QuestStateChange method whenever the 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    /// <summary>
    /// Stores the quest for saving when the game Exits
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stepIndex"></param>
    /// <param name="questStepState"></param>
    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);

    }

    /// <summary>
    /// Check to see if the requirements of the quest are met to start the quest and if so return 'true'
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        foreach(QuestInfo_SO prerequisiteQuestInfo in quest.info.questPrereqs)
        {
            if(GetQuestByID(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        // Loop through each quest and check to see if it is able to be started
        foreach(Quest quest in questMap.Values)
        {
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    /// <summary>
    /// Starts the first quest in a questline
    /// </summary>
    /// <param name="id"></param>
    private void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    /// <summary>
    /// Moves the quest from one step to the next and creates a GameObject based on that
    /// </summary>
    /// <param name="id"></param>
    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByID(id);

        quest.MoveToNextStep();

        if(quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }

    }

    /// <summary>
    /// Finishs the quest and sends the rewards to the Player
    /// </summary>
    /// <param name="id"></param>
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    /// <summary>
    /// Method for sending the rewards to the Player
    /// Currently just throws a Debug.Log statement
    /// </summary>
    /// <param name="quest"></param>
    private void ClaimRewards(Quest quest)
    {
        Debug.Log("Rewards Gained from Quest: " + quest.info.displayName);
        // Gold
        // Items
        // EP (Environmental Progress)
    }

    private void OnApplicationQuit()
    {
        // Saves the status of each quest in the game
        foreach(Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    /// <summary>
    /// Saves the quest passed in
    /// </summary>
    /// <param name="quest">The quest to be saved on Exiting</param>
    private void SaveQuest(Quest quest)
    {
        try
        {
            // Serializes the quest and saves it to PlayerPerfs
            QuestData questData = quest.GetQuestData();
            string serializeData = JsonUtility.ToJson(questData);

            // Should change and is just a quick fix (not sure why but it was recommended from the video to not use PlayerPrefs)
            PlayerPrefs.SetString(quest.info.id, serializeData); 
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
        }
    }

    /// <summary>
    /// Loads each quest in PlayerPrefs from the QuestInfo_SO.id
    /// </summary>
    /// <param name="questInfo"></param>
    /// <returns></returns>
    private Quest LoadQuest(QuestInfo_SO questInfo)
    {
        Quest quest = null;
        try
        {
            if(PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to load Quest: " + questInfo.displayName+ ": " + e);
        }
        return quest;
    }

}
