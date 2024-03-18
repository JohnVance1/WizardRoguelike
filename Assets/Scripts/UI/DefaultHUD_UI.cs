using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultHUD_UI : MonoBehaviour
{
    public List<Sprite> herbJournalButtons;
    public List<Sprite> potionJournalButtons;
    public List<Sprite> inventoryButtons;

    public GameObject herbJournalSprite;
    public GameObject potionJournalSprite;
    public GameObject inventorySprite;

    public InputType storedType;
    public Player_Interact playerInteract;

    public GameObject startMsg;
    public GameObject endMsg;

    private void Awake()
    {
        startMsg.SetActive(true);

    }

    private void Start()
    {
        storedType = playerInteract.inputType;
        DisplayInteractButtons(storedType, herbJournalButtons, herbJournalSprite);
        DisplayInteractButtons(storedType, potionJournalButtons, potionJournalSprite);
        DisplayInteractButtons(storedType, inventoryButtons, inventorySprite);
        OpenGame();
    }

    public void OpenGame()
    {
        StartCoroutine(StartGame());
    }


    private void Update()
    {
        if(storedType != playerInteract.inputType)
        {
            storedType = playerInteract.inputType;
            DisplayInteractButtons(storedType, herbJournalButtons, herbJournalSprite);
            DisplayInteractButtons(storedType, potionJournalButtons, potionJournalSprite);
            DisplayInteractButtons(storedType, inventoryButtons, inventorySprite);

        }
    }

    public void DisplayInteractButtons(InputType type, List<Sprite> buttons, GameObject sp)
    {
        if (type == InputType.KBM)
        {
            sp.GetComponent<Image>().sprite = buttons[0];
        }
        else if (type == InputType.XBox)
        {
            sp.GetComponent<Image>().sprite = buttons[1];

        }
        else if (type == InputType.PS)
        {
            sp.GetComponent<Image>().sprite = buttons[2];

        }

        sp.SetActive(true);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2.5f);
        startMsg.SetActive(false);
    }

    public void EndGame()
    {
        endMsg.SetActive(true);
    }
}
