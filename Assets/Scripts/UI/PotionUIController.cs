using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PotionUIController : SerializedMonoBehaviour
{
    public GameObject selected;

    public Cauldron cauldron;

    //public List<Herb> storedHerbs;
    public Herb currentHerb;
    public List<Device> devices;
    public Potion potion;


    public Player player;
    private VisualElement m_Root;

    private Button m_Distiller;
    private ProgressBar m_DistilBar;

    private Button m_Crusher;
    private ProgressBar m_CrushBar;

    private Button m_Smoker;
    private ProgressBar m_SmokeBar;

    private Button m_Exit;


    public bool IsActive;
    public bool IsFinished;
    public Herb storedHerb;
    //public Herb selectedHerb;
    public ProcessType type;

    public delegate void OnFinishedDevice(Herb herb, ProcessType type);
    public OnFinishedDevice onFinishedDevice;

    public delegate void OnStartedDevice();
    public OnStartedDevice onStartedDevice;

    public Slider slider;
    public float processTime = 2f;
    public float elapsedTime = 0f;



    void Awake()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_Distiller = m_Root.Q<Button>("Distiller");
        m_DistilBar = m_Root.Q<ProgressBar>("Distil_Bar");

        m_Crusher = m_Root.Q<Button>("Crusher");
        m_CrushBar = m_Root.Q<ProgressBar>("Crush_Bar");

        m_Smoker = m_Root.Q<Button>("Smoker");
        m_SmokeBar = m_Root.Q<ProgressBar>("Smoke_Bar");

        m_Exit = m_Root.Q<Button>("ExitButton");

        m_Exit.RegisterCallback<ClickEvent>(CloseUI);

        m_Distiller.RegisterCallback<ClickEvent, ProgressBar>(ActivateDevice, m_DistilBar);
        m_Crusher.RegisterCallback<ClickEvent, ProgressBar>(ActivateDevice, m_CrushBar);
        m_Smoker.RegisterCallback<ClickEvent, ProgressBar>(ActivateDevice, m_SmokeBar);
        IsFinished = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CloseUI(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        // Assign a random new color
        var targetBox = evt.target as VisualElement;
        targetBox.style.backgroundColor = Color.green;
    }

    private void ActivateDevice(ClickEvent evt, ProgressBar bar)
    {
        if (currentHerb != null && IsFinished)
        {
            bar.style.visibility = Visibility.Visible;
            IsFinished = false;
            storedHerb = Object.Instantiate(currentHerb);
            //currentHerb = null;
            if (storedHerb != null && !IsActive)
            {
                IsActive = true;
                //onStartedDevice();
                DeviceSelected();
                StartCoroutine(UpdateDevice(bar));
            }
        }
    }

    public IEnumerator UpdateDevice(ProgressBar bar)
    {
        while(elapsedTime <= processTime)
        {
            ProgressSlider(bar);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
        //yield return new WaitForSeconds(processTime);
        IsActive = false;
        bar.style.visibility = Visibility.Hidden;
        bar.value = 0;
        elapsedTime = 0f;
        IsFinished = true;
        Herb temp = storedHerb;
        storedHerb = null;
        //onFinishedDevice(temp, type);
        SetStoredHerb(temp, type);
        //RemoveOnClick();

    }
    public void ProgressSlider(ProgressBar bar)
    {
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / processTime;
        bar.value = elapsedTime;
    }

    public void SetRawHerb()
    {
        if (currentHerb != null)
        {
            Instantiate(currentHerb);
            cauldron.storedHerbs.Add(currentHerb);
            cauldron.storedHerbs[cauldron.storedHerbs.Count - 1].processType = ProcessType.Raw;
            currentHerb = null;

        }
    }

    public void SetStoredHerb(Herb herb, ProcessType type = ProcessType.Raw)
    {
        if (herb != null)
        {
            cauldron.storedHerbs.Add(herb);
            cauldron.storedHerbs[cauldron.storedHerbs.Count - 1].processType = type;
            //currentHerb = null;

        }
    }

    public void MakePotion()
    {
        if (cauldron.storedHerbs.Count > 0)
        {
            player.AddItemToInventory(potion);

            cauldron.storedHerbs.Clear();
            currentHerb = null;
        }
    }

    public void CancelPotion()
    {
        foreach (Herb h in cauldron.storedHerbs)
        {

            player.AddItemToInventory(cauldron.AddBackHerb(h));
        }
        cauldron.storedHerbs.Clear();
        currentHerb = null;
    }

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public void UseDevice(Herb herb)
    {
        currentHerb = herb;
        //if (currentHerb != null)
        //{
        //    foreach (Device d in devices)
        //    {
        //        //d.ChangeOnClick(herb);
        //        d.selectedHerb = herb;
        //        //d.GetComponent<Button>().onClick.AddListener(() => DeviceSelected(d));
        //        if (d.onFinishedDevice == null)
        //        {
        //            d.onFinishedDevice += SetStoredHerb;
        //        }
        //        if (d.onStartedDevice == null)
        //        {
        //            d.onStartedDevice += DeviceSelected;
        //        }
        //    }
        //}
    }

    public void DeviceSelected()
    {
        if (currentHerb != null)
        {
            player.RemoveItemFromInventory(currentHerb);
            currentHerb = null;
            
        }

    }
}
