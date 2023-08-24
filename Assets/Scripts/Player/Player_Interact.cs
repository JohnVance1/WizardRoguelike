using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        InventoryCanvas.gameObject.SetActive(true);
        IsInventoryOpen = true;
    }
    public void CloseInventory()
    {
        InventoryCanvas.gameObject.SetActive(false);
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
        InventoryCanvas.gameObject.SetActive(true);
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().state = OpenState.Farm;
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().interactable = plot;
        IsInventoryOpen = true;
    }

    public void OpenResearchInventory(ResearchStation research)
    {
        InventoryCanvas.gameObject.SetActive(true);
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().state = OpenState.Research;
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().interactable = research;
        IsInventoryOpen = true;
    }

    public void OpenCauldronInventory(Cauldron cauldron)
    {
        InventoryCanvas.gameObject.SetActive(true);
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().state = OpenState.Cauldron;
        InventoryCanvas.transform.GetChild(0).GetComponent<Inventory_UI>().interactable = cauldron;
        IsInventoryOpen = true;
    }
}
