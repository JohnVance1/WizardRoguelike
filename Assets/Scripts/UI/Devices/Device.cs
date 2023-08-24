using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Device;

public class Device : MonoBehaviour
{
    public bool IsActive;
    public bool IsFinished;
    public Herb storedHerb;
    public Herb selectedHerb;
    public ProcessType type;

    public delegate void OnFinishedDevice(Herb herb, ProcessType type);
    public OnFinishedDevice onFinishedDevice;

    public delegate void OnStartedDevice();
    public OnStartedDevice onStartedDevice;

    public Slider slider;
    public float processTime = 2f;
    public float elapsedTime = 0f;

    private void Start()
    {
        //ChangeOnClick();
        IsFinished = true;
    }

    private void Update()
    {
        if (IsActive)
        {
            slider.gameObject.SetActive(true);
            ProgressSlider();
        }

    }

    public void Activate()
    {
        if (selectedHerb != null && IsFinished)
        {
            IsFinished = false;
            storedHerb = Object.Instantiate(selectedHerb);
            selectedHerb = null;
            if (storedHerb != null && !IsActive)
            {
                IsActive = true;
                onStartedDevice();
                StartCoroutine(UpdateDevice());
            }
        }
        
    }

    public IEnumerator UpdateDevice()
    {
        yield return new WaitForSeconds(processTime);
        IsActive = false;
        slider.gameObject.SetActive(false);
        elapsedTime = 0f;
        IsFinished = true;
        Herb temp = storedHerb;
        storedHerb = null;
        onFinishedDevice(temp, type);
        //RemoveOnClick();

    }

    //public void ChangeOnClick()
    //{
    //    GetComponent<Button>().onClick.RemoveAllListeners();
    //    GetComponent<Button>().onClick.AddListener(() => Activate());

    //}

    //public void RemoveOnClick()
    //{
    //    storedHerb = null;
    //    GetComponent<Button>().onClick.RemoveAllListeners();
    //    IsFinished = false;

    //}

    public void ProgressSlider()
    {
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / processTime;
        slider.value = percentComplete;
    }


}
