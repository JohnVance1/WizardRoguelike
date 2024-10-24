using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    public Button button { get; private set; }

    public TextMeshProUGUI buttonText;
    private UnityAction onSelectQuestButton;

    public void Initilize(string displayName, UnityAction selectQuestButton)
    {
        button = GetComponent<Button>();
        buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        buttonText.text = displayName;
        onSelectQuestButton = selectQuestButton;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectQuestButton();
    }

    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
            case QuestState.CAN_START:
                buttonText.color = Color.red;
                break;
            case QuestState.IN_PROGRESS:
            case QuestState.CAN_FINISH:
                buttonText.color = Color.yellow;
                break;
            case QuestState.FINISHED:
                buttonText.color = Color.green;
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement: " + state);
                break;
        }
    }
}