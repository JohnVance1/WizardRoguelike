using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Interact : MonoBehaviour
{

    public GameObject InventoryCanvas;
    private bool IsInventoryOpen;

    public GameObject JournalCanvas;
    private bool IsJournalOpen;

    public bool IsInteractButtonDown { get; private set; }
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        IsInventoryOpen = false;
        IsJournalOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsInteractButtonDown = Input.GetMouseButtonDown(0) ? true : false;
        //Debug.Log(IsInteractButtonDown);

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (IsInventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsJournalOpen)
            {
                CloseJournal();
            }
            else
            {
                OpenJournal();
            }
        }
    }

    public void OpenInventory()
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.General;
        IsInventoryOpen = true;
    }
    public void CloseInventory()
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        IsInventoryOpen = false;

    }

    public void OpenJournal()
    {
        JournalCanvas.gameObject.SetActive(true);
        IsJournalOpen = true;
    }
    public void CloseJournal()
    {
        JournalCanvas.gameObject.SetActive(false);
        IsJournalOpen = false;

    }

    public void OpenFarmInventory(FarmPlot plot)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Farm;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = plot;
        IsInventoryOpen = true;
    }

    public void OpenResearchInventory(ResearchStation research)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Research;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = research;
        IsInventoryOpen = true;
    }

    public void OpenCauldronInventory(Cauldron cauldron)
    {
        InventoryCanvas.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().state = OpenState.Cauldron;
        InventoryCanvas.transform.GetComponent<InventoryUIController>().interactable = cauldron;
        IsInventoryOpen = true;
    }
}
