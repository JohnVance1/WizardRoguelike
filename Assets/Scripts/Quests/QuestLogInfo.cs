using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class QuestLogInfo : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private QuestLogScrollView scrollingList;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;
    [SerializeField] private TextMeshProUGUI goldRewardsText;
    [SerializeField] private TextMeshProUGUI experienceRewardsText;
    //[SerializeField] private TextMeshProUGUI levelRequirementsText;
    //[SerializeField] private TextMeshProUGUI questRequirementsText;

    private Button firstSelectedButton;

    public GameObject player;
    private PlayerControls input;
    public Player_Interact playerInteract;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        input = GetComponentInParent<Player_Interact>().input;
        input.UI.Cancel.performed += Cancel;
        input.UI.Cancel.Enable();
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        input.UI.Cancel.performed -= Cancel;
        input.UI.Cancel.Disable();
    }

    private void Start()
    {
        player = transform.parent.gameObject;
        playerInteract = player.GetComponent<Player>().interact;
    }


    private void QuestStateChange(Quest quest)
    {
        // add the button to the scrolling list if not already added
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => {
            SetQuestLogInfo(quest);
        });

        if(firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
            firstSelectedButton.Select();
        }

        questLogButton.SetState(quest.state);
        
    }

    private void SetQuestLogInfo(Quest quest)
    {
        // quest name
        questDisplayNameText.text = quest.info.displayName;

        questStatusText.text = quest.GetFullStatusText();
  

    }

    public void Cancel(InputAction.CallbackContext context)
    {
        player.GetComponent<Player_Interact>().CloseOpenUI();
    }



}
