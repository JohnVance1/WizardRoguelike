using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class QuestLogScrollView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Rect Transforms")]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform contentRectTransform;

    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;




    private Dictionary<string, QuestLogButton> _questLogButtons = new Dictionary<string, QuestLogButton>();

    private void Start()
    {
        //for (int i = 0; i < 20; i++)
        //{
        //    QuestInfo_SO questInfoTest = ScriptableObject.CreateInstance<QuestInfo_SO>();
        //    questInfoTest.id = "test_" + i;
        //    questInfoTest.displayName = "Test " + i;
        //    questInfoTest.questStepPrefabs = new GameObject[0];
        //    questInfoTest.rewardText = "This is a reward:" + questInfoTest.displayName;
        //    Quest quest = new Quest(questInfoTest);

        //    QuestLogButton questLogButton = CreateButtonIfNotExists(quest, () =>
        //    {
        //        Debug.Log("SELECTED: " + questInfoTest.displayName);
        //    });

        //    if (i == 0)
        //    {
        //        questLogButton.button.Select();
        //    }
        //}
    }

    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = null;
        // only create the button if we haven't seen this quest id before
        if (!_questLogButtons.ContainsKey(quest.info.id))
        {
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        }
        else
        {
            questLogButton = _questLogButtons[quest.info.id];
        }
        return questLogButton;
    }

    private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        // create the button
        QuestLogButton questLogButton = Instantiate(
            questLogButtonPrefab,
            contentParent.transform).GetComponent<QuestLogButton>();
        // game object name in the scene
        questLogButton.gameObject.name = quest.info.id + "_button";
        // initialize and set up function for when the button is selected
        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
        questLogButton.Initilize(quest.info.displayName, () => {
            selectAction();
            UpdateScrolling(buttonRectTransform);
        });
        // add to map to keep track of the new button
        _questLogButtons[quest.info.id] = questLogButton;
        return questLogButton;
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        // calculate the min and max for the selected button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        // calculate the min and max for the content area
        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;

        // handle scrolling down
        if (buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(
                contentRectTransform.anchoredPosition.x,
                buttonYMax - scrollRectTransform.rect.height
            );
        }
        // handle scrolling up
        else if (buttonYMin < contentYMin)
        {
            contentRectTransform.anchoredPosition = new Vector2(
                contentRectTransform.anchoredPosition.x,
                buttonYMin
            );
        }
    }



    

}
